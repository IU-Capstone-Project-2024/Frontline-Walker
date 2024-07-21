using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TestTurretCannon : TestMessageReceiver
{

    public TurrelAim turrelAim;
    public TestProjectileShooter projectileShooter;
    public float reloadTime = 1f;

    [Header("OnDeathEffects")]
    public GameObject cannon;
    public Transform turningPoint;
    public GameObject visualEffect;
    private bool alive = true;
    
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
        if (alive)
        {
            alive = false;
            _working = false;
            
            cannon.transform.parent = null;
            cannon.AddComponent(typeof(Rigidbody2D));
            cannon.GetComponent<Rigidbody2D>().AddForce(Vector2.up + Random.Range(-0.5f, 0.5f) * Vector2.right, ForceMode2D.Impulse);
            
            Instantiate(visualEffect, turningPoint.position + new Vector3(0,0,1), Quaternion.Euler(new Vector3(-90, 0, 0)));
        }
    }
}
