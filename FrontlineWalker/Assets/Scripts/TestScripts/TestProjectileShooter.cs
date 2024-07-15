using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProjectileShooter : MonoBehaviour
{
    [Header("Links")]
    
    [SerializeField] private Projectile _usedProjectile;
    [SerializeField] private bool _useInitialVelocity;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Sound")] public GameObject soundEffect;
    
    public void Shoot()
    {
        GameObject sound = Instantiate(soundEffect, transform.position, Quaternion.identity);
        sound.transform.SetParent(gameObject.transform);
        
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
