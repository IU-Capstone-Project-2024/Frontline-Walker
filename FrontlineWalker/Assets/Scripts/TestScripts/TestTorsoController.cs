using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTorsoController : MonoBehaviour
{
    public float maxY = 0.5f;
    public float minY = -0.5f;
    public float speed = 0.05f;

    private float _currentY;
    private float _initialY;

    private bool _moving_to_initial_height;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentY = 0;
        _initialY = transform.localPosition.y;

        _moving_to_initial_height = false;
    }

    void Clamp()
    {
        if (_currentY > maxY)
        {
            _currentY = maxY;
        }
        if (_currentY < minY)
        {
            _currentY = minY;
        }
    }

    void setTorsoY()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _initialY + _currentY, transform.localPosition.z);
    }

    private void FixedUpdate()
    {
        if (_moving_to_initial_height)
        {
            if (_currentY > 0.1f)
            { 
                Down();      
            } else if (_currentY < -0.1f)
            {
                Up();        
            }
            else
            {
                _currentY = 0;
                _moving_to_initial_height = false;
            }
        }
    }

    public void SetCurrentY(float _new_currentY)
    {
        _currentY = _new_currentY;
        Clamp();
        setTorsoY();
    }

    public void AddToCurrentY(float _addY)
    {
        _currentY += _addY;
        Clamp();
        setTorsoY();
    }

    public void Up()
    {
        _currentY += speed * Time.deltaTime;
        Clamp();
        setTorsoY();    
    }

    public void Down()
    {
        _currentY -= speed * Time.deltaTime;
        Clamp();
        setTorsoY();
    }

    public void StartMovingToInitialHeight()
    {
        _moving_to_initial_height = true;
    }

    public void StopMovingToInitialHeight()
    {
        _moving_to_initial_height = false;
    }

    public bool isMovingToInitialHeight()
    {
        return _moving_to_initial_height;
    }
}
