using System;
using System.Collections.Generic;
using Audio;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestController : TestMessageReceiver
{
    [Header("Controllers&Observers")]
    public TestProceduralWalkerAnimation proceduralWalkerAnimation;
    public TestTorsoController torsoController;
    public TestCannon mainCannonController;
    public TestCannon AAMachineGunController;
    public TestWalkerPartsObserver partsObserver;
    
    [Header("Speed")]
    public float forward_speed = 2f;
    public float backward_speed = 1f;
    public float forward_force = 50f;
    public float backward_force = 50f;
    public float distabilization_speed = 3f;
    public float initialFriction = 1;
    [Header("Normals")]
    public LayerMask groundLayers;

    private Vector2 _direction;
    private Vector2 _normal;
    [Header("Fuel")] 
    public bool fuelIsLimited = true;
    public float tankCapacity = 100;
    public float idleFuelLoss = 0.1f;
    public float fuelLossPerForceUnit = 0.1f;
    private float fuelLossMultiplier = 1f;
    
    private float _currentFuelLevel;
    
    private float _current_y;
    
    private Rigidbody2D _rb;
    private PhysicsMaterial2D _material;
    
    private float _frictionPenalty;
    private float _movementPenalty;
    
    [Header("Sound")] 
    
    public AudioManager audioManager;

    private bool motorSoundPlay;

    private List<Collision2D> _collisions;
    
    void Start()
    {
        _collisions = new List<Collision2D>();
        _direction = Vector2.right;
        
        _rb = GetComponent<Rigidbody2D>();
        _material = GetComponent<Collider2D>().sharedMaterial;

        _currentFuelLevel = tankCapacity;

        _frictionPenalty = 0;
        _movementPenalty = 0;

        motorSoundPlay = false;
        audioManager.Play("idle");
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

        if (!torsoController.isStabilized())
        {
            PlayIdleMotorSound();
        }
        if (_currentFuelLevel == 0)
        {
            audioManager.StopAll();
        }

        mainCannonController.SetAbleToReceiveCommands(partsObserver.mainCannon.IsWorking());
        AAMachineGunController.SetAbleToReceiveCommands(partsObserver.AAmachineGun.IsWorking());
    }

    public void Right()
    {
        if (AbleToMove())
        {
            PlayActionMotorSound();
            
            var _forward_speed = torsoController.GetCurrentYRatio() * forward_speed;
            _forward_speed *= 1 - _movementPenalty;
            if (Mathf.Abs(_rb.velocity.x) < _forward_speed)
            {
                _rb.AddForce(_direction * forward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _forward_speed;
            }
            FuelLossPerForceUnit(forward_force);
        }
    }

    public void Left()
    {
        if (AbleToMove())
        {
            PlayActionMotorSound();
            
            var _backward_speed = torsoController.GetCurrentYRatio() * backward_speed;
            _backward_speed *= 1 - _movementPenalty;
            if (Mathf.Abs(_rb.velocity.x) < _backward_speed)
            {
                _rb.AddForce(new Vector2(-_direction.x, _direction.y) * backward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _backward_speed;
            }
            FuelLossPerForceUnit(backward_force);
        }
    }

    private void IdleFuelLoss()
    {
        _currentFuelLevel -= idleFuelLoss * fuelLossMultiplier * Time.deltaTime;
        ClampFuel();
    }

    private void FuelLossPerForceUnit(float _force)
    {
        _currentFuelLevel -= fuelLossPerForceUnit * _force * fuelLossMultiplier * Time.deltaTime;
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
        
        PlayIdleMotorSound();
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

    public override void ReceiveMessage()
    {
        Debug.Log("Controller received message");
        _movementPenalty = partsObserver.GetCurrentMovementPenalty();
        _frictionPenalty = partsObserver.GetCurrentFrictionPenalty();
        fuelLossMultiplier = partsObserver.GetFuelLossMultiplier();

        _material.friction = initialFriction - _frictionPenalty;
    }
    
    public void PlayActionMotorSound()
    {
        if (!motorSoundPlay && tankCapacity > 0)
        {
            audioManager.Stop("idle");
            audioManager.Play("action");
            
            motorSoundPlay = true;
        }
    }
    
    public void PlayIdleMotorSound()
    {
        if (motorSoundPlay && tankCapacity > 0)
        {
            audioManager.Stop("action");
            audioManager.Play("idle");
            
            motorSoundPlay = false;
        }
    }

    public void ReceiveResupply()
    {
        Refuel(tankCapacity / 3f);
        AAMachineGunController.ResupplyShells(AAMachineGunController.maxShells / 4);
        mainCannonController.ResupplyShells(mainCannonController.maxShells / 4);
        
        partsObserver.ResupplyFix();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((groundLayers & (1 << other.gameObject.layer)) != 0)
        {
            _collisions.Add(other);
            //Debug.Log(other.gameObject.name + " Added");
        }
        CalculateNormal();
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if ((groundLayers & (1 << other.gameObject.layer)) != 0)
        {
            _collisions.Remove(other);
            //Debug.Log(other.gameObject.name + " Deleted");
        }
        CalculateNormal();
    }

    private void CalculateNormal()
    {
        _normal = Vector2.up;
        var point = Vector2.zero;

        if (_collisions.Count > 0)
        {
            _normal = Vector2.zero;

            if (_rb.velocity.x > 0)
            {
                var max = float.MinValue;
                foreach (var collision in _collisions)
                {
                    if (collision.contacts.Length > 0)
                    {
                        if (collision.contacts[0].point.x > max)
                        {
                            max = collision.contacts[0].point.x;
                            _normal = collision.contacts[0].normal;
                            point = collision.contacts[0].point;
                        }
                    }
                }
            }
            else
            {
                var min = float.MaxValue;
                foreach (var collision in _collisions)
                {
                    if (collision.contacts.Length > 0)
                    {
                        if (collision.contacts[0].point.x < min)
                        {
                            min = collision.contacts[0].point.x;
                            _normal = collision.contacts[0].normal;
                            point = collision.contacts[0].point;
                        }
                    }
                }
            }

            if (_normal == Vector2.zero) _normal = Vector2.up;
            _normal = _normal.normalized;
        }
        Debug.DrawRay(point, point + _normal, Color.blue);
        CalculateDirection();
    }

    private void CalculateDirection()
    {
        Debug.Log("Direction: " + _normal.y + " " + -_normal.x + " " + _collisions.Count);
        _direction = new Vector2(_normal.y, -_normal.x).normalized;
    }

    private void OnDrawGizmos()
    {
        foreach (var collision in _collisions)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(collision.contacts[0].point, 0.05f);
        }
        
    }
}
