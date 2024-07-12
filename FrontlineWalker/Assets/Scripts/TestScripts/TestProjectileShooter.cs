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
    [SerializeField] private Rigidbody2D _rigidbody;
    
    public void Shoot()
    {
        Projectile newProjectile = Instantiate(_usedProjectile, _shootingPoint.position, _shootingPoint.rotation);
        if (_rigidbody != null)
        {
            if (newProjectile.GetComponent<Rigidbody2D>() && _useInitialVelocity)
            {
                newProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity;
            }
        }
    }
}
