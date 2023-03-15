﻿using UnityEngine;
public class LookAtTarget : SteeringMovement
{
    public GameObject target;
    public Vector3 Kp, Ki, Kd, PreviousError;
    private Vector3 P, I, D;
    private void Start()
    {
    }
    private void Update()
    {
    }
    public override Steering GetSteering(SteeringAgent agent)
    {
        Steering ret = base.GetSteering(agent);
        Vector3 targetDirection = target.transform.position - transform.position;
        float angleFromUpToTargetDirection = Vector3.Angle(transform.up, targetDirection);
        float angleFromDownToTargetDirection = Vector3.Angle(-transform.up, targetDirection);

        float angleFromLeftToTargetDirection = Vector3.Angle(-transform.right, targetDirection);
        float angleFromRightToTargetDirection = Vector3.Angle(transform.right, targetDirection);

        // With PID
        float currentError = (angleFromDownToTargetDirection - angleFromUpToTargetDirection) / 180;
        //float currentError = (angleFromDownToTargetDirection - angleFromUpToTargetDirection);
        P.x = currentError;
        I.x += P.x * Time.deltaTime;
        D.x = (P.x - PreviousError.x) / Time.deltaTime;
        PreviousError.x = currentError;
        float torqueX = P.x * Kp.x + I.x * Ki.x + D.x * Kd.x;
        torqueX = Mathf.Clamp(torqueX, -1.0f, 1.0f);

        currentError = (angleFromLeftToTargetDirection - angleFromRightToTargetDirection) / 180;
        //currentError = (angleFromLeftToTargetDirection - angleFromRightToTargetDirection);
        P.y = currentError;
        I.y += P.y * Time.deltaTime;
        D.y = (P.y - PreviousError.y) / Time.deltaTime;
        PreviousError.y = currentError;
        float torqueY = P.y * Kp.y + I.y * Ki.y + D.y * Kd.y;
        torqueY = Mathf.Clamp(torqueY, -1.0f, 1.0f);

        currentError = -currentError;
        P.z = currentError;
        I.z += P.z * Time.deltaTime;
        D.z = (P.z - PreviousError.z) / Time.deltaTime;
        PreviousError.z = currentError;
        float torqueZ = P.z * Kp.z + I.z * Ki.z + D.z * Kd.z;
        torqueZ = Mathf.Clamp(torqueZ, -1.0f, 1.0f);


        // Without PID

        //float torqueX = (angleFromDownToTargetDirection - angleFromUpToTargetDirection) / 30;
        //torqueX = Mathf.Clamp(torqueX, -1, 1);
        //if (angleFromDownToTargetDirection < angleFromUpToTargetDirection) { ret.TorqueX = -1; }
        //else { ret.TorqueX = 1; }

        //float torqueY = (angleFromLeftToTargetDirection - angleFromRightToTargetDirection) / 30;
        //torqueY = Mathf.Clamp(torqueY, -1, 1);

        //float torqueZ = -torqueY;

        //if (angleFromLeftToTargetDirection < angleFromRightToTargetDirection) { ret.TorqueY = -1; ret.TorqueZ = 1; }
        //else { ret.TorqueY = 1; ret.TorqueZ = -1; }




        ret.TorqueX = torqueX;
        ret.TorqueY = torqueY;
        ret.TorqueZ = torqueZ;

        //Debug.Log(agent.Rigidbody.angularVelocity + " " + agent.LocalAngularVelocity);
        return ret;
    }
}
