using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TESTInoyBobrProjectileShooter : MonoBehaviour
{
    [SerializeField] private Projectile usedProjectile;
    [SerializeField] private bool useInitialVelocity;
    [SerializeField] private Vector3 shift;
    private Rigidbody2D _rigidbody;
    private float _timer = 0;

    void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Shoot()
    {
        Projectile newProjectile = Instantiate(usedProjectile, transform.position + shift, transform.rotation);
        if (newProjectile.GetComponent<Rigidbody2D>() && useInitialVelocity)
        {
            newProjectile.GetComponent<Rigidbody2D>().velocity = _rigidbody.velocity;
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > 1)
        {
            _timer = 0;
            Shoot();
        }
    }
}
