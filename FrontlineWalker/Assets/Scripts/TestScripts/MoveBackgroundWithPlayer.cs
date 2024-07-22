using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackgroundWithPlayer : MonoBehaviour
{

    public float movementMultiplier;
    
    private Transform _player;
    private float _playerInitialX;
    private float _initialX;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerInitialX = _player.position.x;

        _initialX = transform.position.x;
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(_initialX + (_playerInitialX - _player.position.x) * movementMultiplier, transform.position.y, transform.position.z);
    }
}
