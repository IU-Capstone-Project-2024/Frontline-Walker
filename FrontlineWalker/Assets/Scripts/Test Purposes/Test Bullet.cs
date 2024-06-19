using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBullet : Projectile
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D _rigidbody;
    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody.velocity += new Vector2(Mathf.Cos(Mathf.Deg2Rad * _rigidbody.rotation), Mathf.Sin(Mathf.Deg2Rad *  _rigidbody.rotation)) * speed;
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Pow");
        Destroy(gameObject);
    }
}
