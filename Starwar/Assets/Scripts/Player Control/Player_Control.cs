using UnityEngine;

public class Player_Control : MonoBehaviour
{
    [SerializeField]
    private float
        Forward_Force = 3.0f,
        TorqueX_Force = 0.1f,
        TorqueY_Force = 0.1f,
        TorqueZ_Force = 0.01f;

    [SerializeField]
    private MachineGun machineGun;

    [SerializeField]
    private MissileLauncherManager missileLauncherManager;

    private Rigidbody _rigidbody;
    private SoundController soundController;
    private Vector3 lastSpot;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        soundController = GetComponent<SoundController>();
        lastSpot = transform.position;

    }

    private void FixedUpdate()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            _rigidbody.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * Forward_Force, ForceMode.Force);
        }
        if (!Mathf.Approximately(Input.GetAxis("TorqueX"), 0f))
        {
            _rigidbody.AddRelativeTorque(Vector3.left * Input.GetAxis("TorqueX") * TorqueX_Force, ForceMode.Force);
        }
        if (!Mathf.Approximately(Input.GetAxis("TorqueY"), 0f))
        {
            // Turns like in space
            //_rigidbody.AddRelativeTorque(Vector3.up * Input.GetAxis("TorqueY") * TorqueY_Force, ForceMode.Force);

            // Turns like in air
            _rigidbody.AddRelativeTorque(Vector3.left * Input.GetAxis("TorqueY") * TorqueX_Force, ForceMode.Force);
            _rigidbody.AddRelativeTorque(Vector3.forward * -Input.GetAxis("TorqueY") * TorqueZ_Force, ForceMode.Force);
        }
        if (!Mathf.Approximately(Input.GetAxis("TorqueZ"), 0f))
        {
            _rigidbody.AddRelativeTorque(Vector3.forward * Input.GetAxis("TorqueZ") * TorqueZ_Force, ForceMode.Force);
        }

        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire bullet");
            soundController.playLaser();
            machineGun.Fire();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Fire Missile");  
            soundController.playMissile(); 
            missileLauncherManager.LockOn();
            missileLauncherManager.Fire();
        }

        
        soundController.playEngine(lastSpot - transform.position);
        lastSpot = transform.position;


    }
}
