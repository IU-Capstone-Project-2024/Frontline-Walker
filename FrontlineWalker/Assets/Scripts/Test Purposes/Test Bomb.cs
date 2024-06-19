using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestBomb : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Boom");
        Destroy(gameObject);
    }
}
