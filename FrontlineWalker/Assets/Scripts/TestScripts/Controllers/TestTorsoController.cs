using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTorsoController : TestMessageReceiver
{
    [Header("Settings")]
    public float maxY = 0.2f;
    public float minY = -0.4f;
    public float speed = 0.5f;
    public float dropSpeed = 0.7f;
    public float walkerTorsoBottomDropHeight;

    [Header("Parts observer")] 
    public TestWalkerPartsObserver partsObserver;
    
    private float _currentY;
    private float _initialY;
    private float _initialSpeed;

    [Header("State flags")]
    public  bool _distabilazed;
    public bool _stabilazing;
    private bool _moving_to_initial_height;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentY = 0;
        _initialY = transform.localPosition.y;

        _initialSpeed = speed;
        
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
        if (_moving_to_initial_height && !_distabilazed)
        {
            if (_currentY > 0.02f)
            { 
                Down();      
            } else if (_currentY < -0.02f)
            {
                Up();        
            }
            else
            {
                _currentY = 0;
                _moving_to_initial_height = false;
            }
        }

        if (_distabilazed)
        {
            if (_currentY > -walkerTorsoBottomDropHeight + 0.02f)
            {
                _currentY -= dropSpeed * Time.deltaTime;
            }
            else
            {
                _currentY = -walkerTorsoBottomDropHeight;
            }
            setTorsoY();
        }

        if (_stabilazing && !_distabilazed)
        {
            if (_currentY < -0.02f)
            {
                _currentY += speed * Time.deltaTime;
            }
            else
            {
                _currentY = 0;
                _stabilazing = false;
            }
            setTorsoY();
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
        if (isStabilized())
        {
            _currentY += speed * Time.deltaTime;
            Clamp();
            setTorsoY();
        }  
    }

    public void Down()
    {
        if (isStabilized())
        {
            _currentY -= speed * Time.deltaTime;
            Clamp();
            setTorsoY();
        }
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

    public float getCurrentY()
    {
        return _currentY;
    }

    public float GetCurrentYRatio()
    {
        if (isStabilized())
        {
           return ( Math.Abs(_currentY - minY) / Math.Abs(maxY - minY) ) * 0.7f + 0.3f; 
        } else
        {
            return 0.3f;
        }
        
    }

    public bool isDistabilazed()
    {
        return _distabilazed;
    }

    public bool isStabilized()
    {
        return !_stabilazing && !_distabilazed;
    }

    public void Distabilaze()
    {
        _distabilazed = true;
    }

    public void Stabilize()
    {
        _distabilazed = false;
        _stabilazing = true;
    }
    
    public override void ReceiveMessage()
    {
        Debug.Log("Torso Controller received message");
        var torsoMovementPenalty = partsObserver.GetCurrentTorsoMovementPenalty();
        speed = _initialSpeed * (1 - torsoMovementPenalty);
    }
}
