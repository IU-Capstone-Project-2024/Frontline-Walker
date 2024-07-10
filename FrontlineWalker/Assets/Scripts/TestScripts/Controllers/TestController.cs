using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class TestController : MonoBehaviour
{
    [Header("Controllers&Observers")]
    public TestProceduralWalkerAnimation proceduralWalkerAnimation;
    public TestTorsoController torsoController;
    public TestCannon mainCannonController;
    public TestWalkerPartsObserver partsObserver;
    
    [Header("Speed")]
    public float forward_speed = 2f;
    public float backward_speed = 1f;
    public float forward_force = 50f;
    public float backward_force = 50f;
    public float distabilization_speed = 3f;
    [Header("Fuel")] 
    public bool fuelIsLimited = true;
    public float tankCapacity = 100;
    public float idleFuelLoss = 0.1f;
    public float fuelLossPerForceUnit = 0.1f;

    private float _currentFuelLevel;
    
    private float _current_y;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        _currentFuelLevel = tankCapacity;
    }

    private void FixedUpdate()
    {
        //Debug.Log(_rb.velocity.magnitude);
        if (_rb.velocity.magnitude > distabilization_speed)
        {
            torsoController.Distabilaze();
            proceduralWalkerAnimation.StopAnimation();
        }

        if (torsoController.isStabilized())
        {
            proceduralWalkerAnimation.ResumeAnimation();
        }
        IdleFuelLoss();
        //Debug.Log(_currentFuelLevel);

        mainCannonController.SetAbleToReceiveCommands(partsObserver.mainCannon.IsWorking());
    }

    public void Right()
    {
        if (AbleToMove())
        {
            var _forward_speed = torsoController.GetCurrentYRatio() * forward_speed;
            if (Mathf.Abs(_rb.velocity.x) < _forward_speed)
            {
                _rb.AddForce(Vector2.right * forward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _forward_speed;
            }
            FuelLossPerForceUnit(forward_force);
        }
    }

    public void Left()
    {
        if (AbleToMove())
        {
            var _backward_speed = torsoController.GetCurrentYRatio() * backward_speed;
            if (Mathf.Abs(_rb.velocity.x) < _backward_speed)
            {
                _rb.AddForce(-Vector2.right * backward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _backward_speed;
            }
            FuelLossPerForceUnit(backward_force);
        }
    }

    private void IdleFuelLoss()
    {
        _currentFuelLevel -= idleFuelLoss * Time.deltaTime;
        ClampFuel();
    }

    private void FuelLossPerForceUnit(float _force)
    {
        _currentFuelLevel -= fuelLossPerForceUnit * _force * Time.deltaTime;
        ClampFuel();
    }

    private void ClampFuel()
    {
        if (_currentFuelLevel < 0f) _currentFuelLevel = 0f;
        if (_currentFuelLevel > tankCapacity) _currentFuelLevel = tankCapacity;
    }

    public void Refuel(float _amountOfFuel)
    {
        _currentFuelLevel += _amountOfFuel;
        ClampFuel();
    }

    public float GetFuelLeft()
    {
        return _currentFuelLevel;
    }
    
    private bool AbleToMove()
    {
        if (fuelIsLimited && _currentFuelLevel == 0)
        {
            return false;
        }
        return torsoController.isStabilized();
    }
}
