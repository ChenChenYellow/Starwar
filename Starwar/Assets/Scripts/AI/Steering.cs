using UnityEngine;
public class Steering
{
    public float TorqueX { get; set; }
    public float TorqueY { get; set; }
    public float TorqueZ { get; set; }
    public float ForwardLinear { get; set; }
    public Steering(float torqueX, float torqueY, float torqueZ, float forwardLinear)
    {
        TorqueX = torqueX;
        TorqueY = torqueY;
        TorqueZ = torqueZ;
        ForwardLinear = forwardLinear;
    }
    public void Add(Steering steering)
    {
        TorqueX += steering.TorqueX;
        TorqueY += steering.TorqueY;
        TorqueZ += steering.TorqueZ;
        ForwardLinear += steering.ForwardLinear;
    }
}
