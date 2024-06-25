using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TestBomb : Projectile
{
    [SerializeField] private int explosionForce = 10;

    [SerializeField] private int explosionRadius = 10;

    [SerializeField] private int damage = 10;
    
    [SerializeField] private LayerMask hit_layer;
    
    [SerializeField] private LayerMask wall_layer;

    [SerializeField] private float presicionAngle = 3;
    private void Start()
    {
        if (presicionAngle <= 0)
        {
            throw new Exception();
        }

        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode2();
        Debug.Log("Boom");
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, hit_layer);

        foreach (var collider in colliders)
        {
            var hitPoints = GetAllPossiblePoints(transform.position, collider);
            foreach (var point in hitPoints)
            {
                var distance = ((Vector2)transform.position - point).magnitude;
                var rb = collider.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float forceValue = explosionForce * (1 - distance / explosionRadius);
                    rb.AddForceAtPosition((point - (Vector2)transform.position).normalized * forceValue,point, ForceMode2D.Impulse);
                }
            }
        }
    }

    private List<Vector2> GetAllPossiblePoints(Vector2 origin, Collider2D target)
    {
        var result = new List<Vector2>();
        Vector2 initialDirection = ((Vector2)target.transform.position - origin);
        for (float angle = 0; angle < 360; angle += presicionAngle)
        {
            Vector2 direction = (Quaternion.AngleAxis(angle, Vector3.forward) * initialDirection).normalized;
            var hits = Physics2D.RaycastAll(origin, direction, hit_layer);
            foreach (var hit in hits)
            {
                if (hit.collider != target){
                    continue;
                }

                var distance = (hit.point - origin).magnitude;
                if (!Physics2D.Raycast(origin, hit.point - origin, distance, wall_layer))
                {
                    result.Add(hit.point);
                }
            }
        }
        return result;
    }

    private void Explode2()
    {
        var AllHits = new List<RaycastHit2D>();
        var origin = (Vector2)transform.position;
        for (float angle = 0; angle < 360; angle += presicionAngle)
        {
            Vector2 direction = (Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.right).normalized;
            var hits = Physics2D.RaycastAll(origin, direction, explosionRadius, hit_layer);
            foreach (var hit in hits)
            {
                var distance = (hit.point - origin).magnitude;
                if (!Physics2D.Raycast(origin, (hit.point - origin).normalized, distance, wall_layer))
                    AllHits.Add(hit);
            }
        }

        foreach (var hit in AllHits)
        {
            var distance = (hit.point - origin).magnitude;
            var forceValue = explosionForce * (1 - distance / explosionRadius);
            var force = (hit.point - origin).normalized * forceValue;
            if (hit.rigidbody)
            {
                hit.rigidbody.AddForceAtPosition(force, hit.point, ForceMode2D.Impulse);
            }
        }

    }
}
