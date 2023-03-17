using UnityEngine;
public class Missile : MonoBehaviour
{
    public float InitialLauchForce;
    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set
        {
            target = value;
            seek.Target = Target;
            lookAtTarget.Target = Target;
        }
    }

    private Seek seek;
    private LookAtTarget lookAtTarget;
    private void Awake()
    {
        seek = GetComponent<Seek>();
        lookAtTarget = GetComponent<LookAtTarget>();
    }
}
