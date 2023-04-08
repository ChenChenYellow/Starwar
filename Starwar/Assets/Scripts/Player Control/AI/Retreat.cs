﻿using UnityEngine;

public class Retreat : SteeringMovement
{
    [SerializeField]
    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set
        {
            arrive.Target = value;
            lookAtTarget.Target = value;
            target = value;
        }
    }
    public float ArriveAngle, ArriveStopDistance, ArriveSlowDistance, ArriveSpeedLimit;
    public Vector3 Kp, Ki, Kd;


    private Arrive arrive;
    private LookAtTarget lookAtTarget;

    private void Start()
    {
        arrive = new Arrive()
        {
            MaximumAngle = ArriveAngle,
            StopDistance = ArriveStopDistance,
            SlowDistance = ArriveSlowDistance,
            SlowZoneSpeedLimit = ArriveSpeedLimit,
        };
        lookAtTarget = new LookAtTarget()
        {
            Kp = Kp,
            Ki = Ki,
            Kd = Kd
        };
    }

    public override Steering GetSteering(SteeringAgent agent)
    {
        Steering ret = base.GetSteering(agent);
        if (Target == null) { return ret; }
        ret.Add(arrive.GetSteering(agent));
        ret.Add(lookAtTarget.GetSteering(agent));
        return ret;
    }
}
