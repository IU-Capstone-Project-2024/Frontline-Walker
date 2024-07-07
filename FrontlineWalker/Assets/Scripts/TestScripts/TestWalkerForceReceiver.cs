using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkerForceReceiver : TestForceReceiver
{
    public TestTorsoController torsoController;
    
    public override void ReceiveForce(float force, Vector2 direction)
    {
        force *= torsoController.GetCurrentYRatio();
        forceReceiver.AddForce(direction * force);
    }
}
