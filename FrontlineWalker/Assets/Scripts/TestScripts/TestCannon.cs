using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

[RequireComponent(typeof(TestProjectileShooter))]
[RequireComponent(typeof(TestRotationController))]
[RequireComponent(typeof(TestBlowbackMechanism))]
public class TestCannon : MonoBehaviour
{
    public float reloadTime;
    [Range(0, 100)]
    public int maxShells;
    public float recoilForce;
    public TestForceReceiver forceReceiver;

    public float maxAimingDelta = 0;
    
    private TestRotationController _rotatonController;
    private TestProjectileShooter _projectileShooter;
    private TestBlowbackMechanism _blowbackMechanism;

    private bool _isAbleToReceiveCommands;
    private bool _readyToFire;
    private int _remainingShells;
    private float _targetAngle;
    
    [Header("Sound")] 
    
    public AudioManager audioManager;

    public bool PerRoundReload = false;
    public float PerRoundReloasDelay = 0;

    private bool _aimingSoundPlay;
    
    void Start()
    {
        _projectileShooter = GetComponent<TestProjectileShooter>();
        _rotatonController = GetComponent<TestRotationController>();
        _blowbackMechanism = GetComponent<TestBlowbackMechanism>();

        _remainingShells = maxShells;
        _readyToFire = true;
        _isAbleToReceiveCommands = true;
        _aimingSoundPlay = false;
    }
    void FixedUpdate()
    {
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) < _targetAngle - maxAimingDelta) {
            Up();
        } else 
        if (Mathf.RoundToInt(_rotatonController.GetCurrentAngle()) > _targetAngle + maxAimingDelta)
        {
            Down();
        } else if (_rotatonController.GetCurrentAngle() == _rotatonController.maxAngle ||
                   _rotatonController.GetCurrentAngle() == _rotatonController.minAngle)
        {
            PauseAimingSound();
        }
        else
        {
            _rotatonController.SetCurrentAngle(_targetAngle);
            PauseAimingSound();
        }
    }

    public void Up()
    {
        if (_isAbleToReceiveCommands)
        {
            _rotatonController.Up();
            PlayAimingSound();
        }
        
    }

    public void Down()
    {
        if (_isAbleToReceiveCommands)
        {
            _rotatonController.Down();
            PlayAimingSound();
        }
    }

    public void Fire()
    {
        if (_readyToFire && _isAbleToReceiveCommands)
        {
           _projectileShooter.Shoot();
           forceReceiver.ReceiveForce(recoilForce, Vector2.left);
           _remainingShells--;
           _blowbackMechanism.Blowback();
           _readyToFire = false;
           Invoke("Reload", reloadTime);
           if (PerRoundReload)
           {
               Invoke("PlayReloadSound", PerRoundReloasDelay);
           }
        }
    }

    public bool ReadyToFire()
    {
        return _readyToFire;
    }

    public void Reload()
    {
        if (_remainingShells > 0)
        {
            _readyToFire = true;
        }

        if (PerRoundReload && _remainingShells > 0)
        {
            audioManager.Stop("reload");
        }
    }

    public void PlayReloadSound()
    {
        audioManager.Play("reload");
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
