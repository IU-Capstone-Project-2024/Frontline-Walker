using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPiston : MonoBehaviour
{

    [SerializeField] private GameObject _base;
    [SerializeField] private GameObject _piston;
    [SerializeField] private float _max_force;
    [SerializeField] private float _min_force;
    [SerializeField] private float _force_to_add;
    private float _force;

    private Rigidbody2D _piston_rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _piston_rb = _piston.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Debug.Log(_force);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ClampForce();
        _piston_rb.AddForce(new Vector2(0, _force));
    }

    public void Up()
    {
        _force += _force_to_add;
    }
    
    public void Down()
    {
        _force -= _force_to_add;
    }

    private void ClampForce()
    {
        if (_force < _min_force) _force = _min_force;
        if (_force > _max_force) _force = _max_force;
    }
}
