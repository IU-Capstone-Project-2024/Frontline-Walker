using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceCollision : MonoBehaviour
{
    public Vector2 impactForce;
    public float impactForceMagnitude;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rb1 = GetComponent<Rigidbody2D>();
        Rigidbody2D rb2 = collision.rigidbody;

        if (rb1 != null && rb2 != null)
        {
            Vector2 relativeVelocity = rb1.velocity - rb2.velocity;
            float combinedMass = rb1.mass * rb2.mass / (rb1.mass + rb2.mass);
            impactForce = combinedMass * relativeVelocity / Time.fixedDeltaTime;
            impactForceMagnitude = impactForce.magnitude;
        }
    }
}
