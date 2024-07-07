using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestProjectileShooter))]
[RequireComponent(typeof(TestRotatonController))]
public class TestCannon : MonoBehaviour
{
    public float reloadTime;
    [Range(0, 100)]
    public int maxShells;
    public float recoilForce;
    public TestForceReceiver forceReceiver;
    
    private TestRotatonController _rotatonController;
    private TestProjectileShooter _projectileShooter;

    private bool _readyToFire;
    private int _remainingShells;
    
    void Start()
    {
        _projectileShooter = GetComponent<TestProjectileShooter>();
        _rotatonController = GetComponent<TestRotatonController>();

        _remainingShells = maxShells;
        _readyToFire = true;
    }
    void Update()
    {
        
    }

    public void Up()
    {
        _rotatonController.Up();
    }

    public void Down()
    {
        _rotatonController.Down();
    }

    public void Fire()
    {
        if (_readyToFire)
        {
           _projectileShooter.Shoot();
           forceReceiver.ReceiveForce(recoilForce, Vector2.left);
           _remainingShells--;
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
}
