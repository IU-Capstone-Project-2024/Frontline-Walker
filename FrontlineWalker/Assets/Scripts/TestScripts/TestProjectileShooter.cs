using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProjectileShooter : MonoBehaviour
{
    [SerializeField] private GameObject _usedProjectile;
    [SerializeField] private bool _useInitialVelocity;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Rigidbody2D _rigidbody;

    void Start()
    {
        if (_rigidbody != null)
        {
            _rigidbody.centerOfMass = new Vector2(0, -1f);
        }
    }

    public void Shoot()
    {
        GameObject newProjectile = Instantiate(_usedProjectile, _shootingPoint.position, _shootingPoint.rotation);
        if (_rigidbody != null)
        {
            if (newProjectile.GetComponent<Rigidbody2D>() && _useInitialVelocity)
            {
                newProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity;
            }
        }
    }
}
