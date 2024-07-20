using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBombHolder : MonoBehaviour
{
    [SerializeField] private TestBomb bomb;
    private Vector2 _prevPos;
    private Rigidbody2D _rigidbody2D;
    private Rigidbody2D _parentrb;

    private void Start()
    {
        _rigidbody2D = bomb.GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
        _parentrb = bomb.transform.parent.GetComponent<Rigidbody2D>();
    }
    

    public void Release()
    {
        if (bomb == null)
            return;
        bomb.transform.parent = null;
        bomb.GetComponent<Rigidbody2D>().gravityScale = 1;
        bomb = null;
    }

    private void Update()
    {
        _rigidbody2D.velocity = _parentrb.velocity;
        if (Input.GetKeyDown(KeyCode.E) )
        {
            Release();
        }
    }
    
}