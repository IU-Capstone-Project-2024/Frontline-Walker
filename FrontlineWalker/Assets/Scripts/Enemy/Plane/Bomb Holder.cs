using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    [SerializeField] private TestBomb bomb;
    private Vector2 _prevPos;
    private Vector2 _delayPos;

    private void Start()
    {
        bomb.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private Vector2 GetVelocity()
    {
        Debug.Log((Vector2)transform.position + " " + _prevPos + " " + _delayPos);
        return ((Vector2)transform.position - _delayPos) / 0.02f;
    }

    public void Release()
    {
        if (bomb == null)
            return;
        bomb.GetComponent<Rigidbody2D>().velocity = GetVelocity();
        bomb.transform.parent = null;
        bomb.GetComponent<Rigidbody2D>().gravityScale = 1;
        bomb = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) )
        {
            Release();
        }
    }

    private void FixedUpdate()
    {
        _delayPos = _prevPos;
        _prevPos = transform.position;
    }
}
