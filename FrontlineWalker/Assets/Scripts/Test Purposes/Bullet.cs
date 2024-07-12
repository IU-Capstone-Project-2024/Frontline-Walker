using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        var direction = new Vector2(transform.right.x, transform.right.y);
        Debug.Log(transform.right);
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody.velocity += direction * speed;
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Pow");
        Destroy(gameObject);
    }
}
