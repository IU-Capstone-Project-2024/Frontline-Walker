using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProjectileShooter : MonoBehaviour
{
    [Header("Links")]
    
    [SerializeField] private Projectile _usedProjectile;
    [SerializeField] private bool _useInitialVelocity;
    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Rigidbody2D _rigidbody;

    [Header("Sound")] public AudioManager audioManager;
    
    public void Shoot()
    {
        if (audioManager != null)
        {
            audioManager.Play("fire");
        }
        
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
