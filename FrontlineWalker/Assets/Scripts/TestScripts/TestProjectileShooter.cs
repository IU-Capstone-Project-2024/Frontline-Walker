using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProjectileShooter : MonoBehaviour
{
    [SerializeField] private Projectile _usedProjectile;
    [SerializeField] private bool _useInitialVelocity;
    [SerializeField] private Transform _shootingPoint;
    private Rigidbody2D _rigidbody;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        _rigidbody.centerOfMass = new Vector2(0, -1f);
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(_usedProjectile, _shootingPoint.position, _shootingPoint.rotation);
        if (newProjectile.GetComponent<Rigidbody2D>() && _useInitialVelocity)
        {
            newProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity;
        }
    }
}
