using UnityEngine;
public class MissileLauncher : MonoBehaviour
{

    [SerializeField] float MaxLockOnAngle, MaxLockOnDistance, InitialLauchForce;
    [SerializeField]
    private GameObject MissilePrefab,
         ParentObject;
    public bool IsLoaded = true;
    public GameObject Target;
    public void LockOn()
    {
        Collider[] colliders = Physics.OverlapSphere(ParentObject.transform.position, MaxLockOnDistance);
        Collider targetCollider = null;
        float targetAngle = 0;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == ParentObject) { continue; }
            Vector3 colliderDirection = collider.transform.position - ParentObject.transform.position;
            float colliderAngle = Vector3.Angle(ParentObject.transform.forward, colliderDirection);
            if (colliderAngle <= MaxLockOnAngle)
            {
                if (targetCollider == null)
                {
                    targetAngle = colliderAngle;
                    targetCollider = collider;
                    continue;
                }
                else
                {
                    if (targetAngle < colliderAngle) { continue; }
                    else
                    {
                        targetAngle = colliderAngle;
                        targetCollider = collider;
                        continue;
                    }
                }
            }
        }
        Target = targetCollider.gameObject;
    }
    public void Launch()
    {
        if (Target != null && IsLoaded)
        {
            GameObject missleObject = Instantiate(MissilePrefab, transform.position - 5 * transform.up, transform.rotation);
            Missile missle = missleObject.GetComponent<Missile>();
            missle.InitialLauchForce = InitialLauchForce;
            missle.Target = Target; 
            IsLoaded = false;
        }
    }
}
