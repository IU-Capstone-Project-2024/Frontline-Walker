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

    void Start()
    {
        _readyToFire = true;
    }

    void FixedUpdate()
    {
        if (turrelAim.AimOnEnemy() && _readyToFire)
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
        Destroy(gameObject);
    }
}
