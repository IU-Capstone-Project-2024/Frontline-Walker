using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTurretCannon : TestMessageReceiver
{

    public TurrelAim turrelAim;
    public TestProjectileShooter projectileShooter;
    public float reloadTime = 1f;

    private bool _readyToFire;
    private bool _working;

    void Start()
    {
        _readyToFire = true;
        _working = true;
    }

    void FixedUpdate()
    {
        if (turrelAim.AimOnEnemy() && _readyToFire && _working)
        {
            projectileShooter.Shoot();
            _readyToFire = false;
            Invoke("Reload", reloadTime);
        }
    }

    public void Reload()
    {
        _readyToFire = true;
    }

    public override void ReceiveMessage()
    {
        _working = false;
    }
    
    public override void ReceiveTerminationMessage()
    {
        Destroy(gameObject);
    }
}
