using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProjectileShooter : MonoBehaviour
{
    [SerializeField] private Projectile _usedProjectile;
    [SerializeField] private bool _useInitialVelocity;
    [SerializeField] private Vector2 _shift;
    private Rigidbody2D _rigidbody;
    private float _timer = 0;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody.centerOfMass = new Vector2(0, -1f);
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(_usedProjectile, transform.position + transform.rotation * _shift, transform.rotation);
        if (newProjectile.GetComponent<Rigidbody2D>() && _useInitialVelocity)
        {
            newProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity;
        }
    }

    private void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > 0.3)
        {
            _timer = 0;
            Shoot();
        }
    }
}
