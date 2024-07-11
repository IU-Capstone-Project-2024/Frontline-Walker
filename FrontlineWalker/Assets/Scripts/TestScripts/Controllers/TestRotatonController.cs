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
    public float maxAngle = 30;
    public float minAngle = -30;
    private float _currentAngle;

    private void Start()
    {
        _currentAngle = 0;
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
        if (_currentAngle > maxAngle)
        {
            _currentAngle = maxAngle;
        }

        if (_currentAngle < minAngle)
        {
            _currentAngle = minAngle;
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
