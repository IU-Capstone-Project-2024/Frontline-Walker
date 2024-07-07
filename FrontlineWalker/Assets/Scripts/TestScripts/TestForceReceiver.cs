using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestForceReceiver : MonoBehaviour
{

    public Rigidbody2D forceReceiver;

    public virtual void ReceiveForce(float force, Vector2 direction)
    {
        forceReceiver.AddForce(direction * force);
    }
}
