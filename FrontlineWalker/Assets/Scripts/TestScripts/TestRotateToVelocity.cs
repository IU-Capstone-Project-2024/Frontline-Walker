using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestRotateToVelocity : MonoBehaviour
{

    public float rotation_shift = 90f;
    
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var direction = new Vector2(_rb.velocity.x, _rb.velocity.y).normalized;
        float rotation = Mathf.Atan2(transform.position.x + direction.x, transform.position.y + direction.y) * Mathf.Rad2Deg;
        rotation += rotation_shift;
        transform.eulerAngles = new Vector3(0, 0, rotation);
    }
}
