using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestProjectileShooter))]
[RequireComponent(typeof(TestRotatonController))]
[RequireComponent(typeof(TestBlowbackMechanism))]
public class TestCannon : MonoBehaviour
{
    public float reloadTime;
    [Range(0, 100)]
    public int maxShells;
    public float recoilForce;
    public TestForceReceiver forceReceiver;
    
    private TestRotatonController _rotatonController;
    private TestProjectileShooter _projectileShooter;
    private TestBlowbackMechanism _blowbackMechanism;

    private bool _isAbleToReceiveCommands;
    private bool _readyToFire;
    private int _remainingShells;
    private float _targetAngle;
    
    void Start()
    {
        _projectileShooter = GetComponent<TestProjectileShooter>();
        _rotatonController = GetComponent<TestRotatonController>();
        _blowbackMechanism = GetComponent<TestBlowbackMechanism>();

        _remainingShells = maxShells;
        _readyToFire = true;
        _isAbleToReceiveCommands = true;
    }
    void FixedUpdate()
    {
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) < _targetAngle) {
            Up();
        }
        
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) > _targetAngle)
        {
            Down();
        }
    }

    public void Up()
    {
        if (_isAbleToReceiveCommands)
        {
            _rotatonController.Up();
        }
        
    }

    public void Down()
    {
        Debug.Log("There");
        if (_isAbleToReceiveCommands)
        {
            Debug.Log("Here");
            _rotatonController.Down();
        }
    }

    public void Fire()
    {
        Debug.Log("Gun log");
        if (_readyToFire && _isAbleToReceiveCommands)
        {
           _projectileShooter.Shoot();
           forceReceiver.ReceiveForce(recoilForce, Vector2.left);
           _remainingShells--;
           _blowbackMechanism.Blowback();
           _readyToFire = false;
           Invoke("Reload", reloadTime);
        }
    }

    public void Reload()
    {
        if (_remainingShells > 0)
        {
            _readyToFire = true;
        }
    }

    public int GetNumberOfRemainingShells()
    {
        return _remainingShells;
    }

    public void ResupplyShells(int resupply)
    {
        if (resupply > 0)
        {
            _remainingShells += resupply;
        }

        if (_remainingShells > maxShells) _remainingShells = maxShells;
    }

    public void SetAbleToReceiveCommands(bool value)
    {
        _isAbleToReceiveCommands = value;
    }

    public bool IsAbleToReceiveCommands()
    {
        return _isAbleToReceiveCommands;
    }

    public void SetTargetAngle(float value)
    {
        _targetAngle = value;
    }

    public void setMaxAngle(float value)
    {
        _rotatonController.maxAngle = value;
    }
    
    public void setMinAngle(float value)
    {
        _rotatonController.minAngle = value;
    }
}
