using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
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
    public bool _cannonIsFired;
    
    [Header("Sound")] 
    
    public AudioManager audioManager;

    private bool _aimingSoundPlay;
    
    void Start()
    {
        _projectileShooter = GetComponent<TestProjectileShooter>();
        _rotatonController = GetComponent<TestRotatonController>();
        _blowbackMechanism = GetComponent<TestBlowbackMechanism>();

        _remainingShells = maxShells;
        _readyToFire = true;
        _isAbleToReceiveCommands = true;
        _aimingSoundPlay = false;

        _cannonIsFired = false;
    }
    void FixedUpdate()
    {
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) < _targetAngle) {
            Up();
            PlayAimingSound();
        } else 
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) > _targetAngle)
        {
            Down();
            PlayAimingSound();
        }
        else
        {
            PauseAimingSound();
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
        if (_isAbleToReceiveCommands)
        {
            _rotatonController.Down();
        }
    }

    public void Fire()
    {
        if (_readyToFire && _isAbleToReceiveCommands)
        {
           _cannonIsFired = true;
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

    public void PlayAimingSound()
    {
        if (!_aimingSoundPlay)
        {
            audioManager.Play("aiming");

            _aimingSoundPlay = true;
        }
    }

    public void PauseAimingSound()
    {
        if (_aimingSoundPlay)
        {
            audioManager.Pause("aiming");

            _aimingSoundPlay = false;
        }
    }
}
