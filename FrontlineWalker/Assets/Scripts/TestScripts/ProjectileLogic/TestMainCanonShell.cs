using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestMainCanonShell : MonoBehaviour
{

    public float initialForce;
    public Direction direction;
    
    private Rigidbody2D _rb;

    public enum Direction
    {
        Up, Forward
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        
        if (direction == Direction.Forward)
        {
            _rb.AddForce(transform.forward * initialForce, ForceMode2D.Impulse);
        } else if (direction == Direction.Up)
        {
            _rb.AddForce(transform.up * initialForce, ForceMode2D.Impulse);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
