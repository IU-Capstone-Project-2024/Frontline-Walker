using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TestBomb : Projectile
{
    [SerializeField] private float explosionForce = 10;

    [SerializeField] private float explosionRadius = 10;

    [SerializeField] private int damage = 10;
    
    [SerializeField] private LayerMask hit_layer;
    
    [SerializeField] private LayerMask wall_layer;

    [Range(1, 100)]
    [SerializeField] private float presicionAngle = 3;

    [SerializeField] private float _timeBeforeDestruction = 20;
    private void Start()
    {
        Destroy(gameObject, _timeBeforeDestruction);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        Debug.Log("Boom");
        Destroy(gameObject);
    }

    private void Explode()
    {
        var allHits = new List<RaycastHit2D>();
        var origin = (Vector2)transform.position;
        for (float angle = 0; angle < 360; angle += presicionAngle)
        {
            Vector2 direction = (Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.right).normalized;
            var hits = Physics2D.RaycastAll(origin, direction, explosionRadius, hit_layer);
            foreach (var hit in hits)
            {
                var distance = (hit.point - origin).magnitude;
                if (!Physics2D.Raycast(origin, (hit.point - origin).normalized, distance, wall_layer))
                    allHits.Add(hit);
            }
        }

        foreach (var hit in allHits)
        {
            var distance = (hit.point - origin).magnitude;
            var forceValue = explosionForce * (1 - distance / explosionRadius);
            var force = (hit.point - origin).normalized * forceValue;
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForceAtPosition(force, hit.point, ForceMode2D.Impulse);
                Debug.DrawRay(origin, force.normalized * distance, Color.red, 5);
            }
        }

    }
}
