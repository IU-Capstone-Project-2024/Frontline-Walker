using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class TestRotatonController : MonoBehaviour
{
    public float rotationSpeed = 0.5f;
    public Slider rotationSlider;
    private float _currentAngle;

    private void Start()
    {
        _currentAngle = 0;
    }

    void FixedUpdate()
    {
        if (Mathf.RoundToInt(_currentAngle) < rotationSlider.value) {
            Up();
        }
        
        if (Mathf.RoundToInt(_currentAngle) > rotationSlider.value)
        {
            Down();
        }
    }

    public void Up()
    {
        _currentAngle += rotationSpeed * Time.deltaTime;
        Clamp();
        SetRotation();
    }

    public void Down()
    {
        _currentAngle -= rotationSpeed * Time.deltaTime;
        Clamp();
        SetRotation();
    }

    private void Clamp()
    {
        if (_currentAngle > rotationSlider.maxValue)
        {
            _currentAngle = rotationSlider.maxValue;
        }

        if (_currentAngle < rotationSlider.minValue)
        {
            _currentAngle = rotationSlider.minValue;
        }
    }

    private void SetRotation()
    {
        transform.localRotation = Quaternion.Euler(0,0,_currentAngle);
    }

    public float GetCurrentAngle()
    {
        return _currentAngle;
    }
}
