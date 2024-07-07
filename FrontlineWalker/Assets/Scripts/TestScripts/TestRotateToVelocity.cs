using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestRotateToVelocity : MonoBehaviour
{

    public float rotationShift = 90f;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var direction = new Vector2(_rb.velocity.x, _rb.velocity.y).normalized;
        var trajectory =
            new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z) -
            transform.position;
        float rotation = Mathf.Atan2(trajectory.y, trajectory.x) * Mathf.Rad2Deg;
        rotation += rotationShift;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        var direction = new Vector2(_rb.velocity.x, _rb.velocity.y).normalized;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector2(transform.position.x + direction.x, transform.position.y + direction.y), 0.1f);
    }
}
