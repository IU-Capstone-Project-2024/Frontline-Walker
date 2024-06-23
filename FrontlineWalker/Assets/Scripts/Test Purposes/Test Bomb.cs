using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBomb : Projectile
{
    [SerializeField] private int explosionForce = 10;

    [SerializeField] private int explosionRadius = 10;

    [SerializeField] private int damage = 10;
    
    [SerializeField] private LayerMask hit_layer;
    
    [SerializeField] private LayerMask wall_layer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explode();
        Debug.Log("Boom");
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, hit_layer);

        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if(!Physics2D.Raycast(transform.position, (hit.transform.position - transform.position).normalized, distance, wall_layer))
            {
                var rb = hit.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    float forceValue = explosionForce * (1 - distance / explosionRadius);
                    rb.AddForce((hit.transform.position - transform.position).normalized * forceValue, ForceMode2D.Impulse);
                }
            }
        }
    }
}
