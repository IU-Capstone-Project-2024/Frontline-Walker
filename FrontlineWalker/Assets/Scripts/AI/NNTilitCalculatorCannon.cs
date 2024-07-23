using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class NNTiltCalculatorCannon : TestMessageReceiver
{

    public NNTiltCalculator brain;
    public TestProjectileShooter projectileShooter;
    public Projectile projectile;
    public TestMainCanonShell projectileShell;
    
    
    public float minAngle = -270;
    public float maxAngle = -170; 
    public float rotationSpeed = 200.0f;
    public float angleOfStartingFire = 5f;
    
    private Transform _target;
    private Quaternion _targetRotation;
    
    public float reloadTime = 1f;

    [Header("OnDeathEffects")]
    public GameObject cannon;
    public Transform turningPoint;
    public GameObject visualEffect;
    private bool alive = true;
    
    private bool _readyToFire;
    private bool _working;
    private bool _ableToRotate;

    public float a = -0.01f;
    public float b = 0;
    public float c = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        _ableToRotate = true;
        _readyToFire = true;
        _working = true;
        
        projectileShooter.usedProjectile = projectile;
        if (projectileShell == null)
            projectileShell = projectile.GetComponent<TestMainCanonShell>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (col.transform.childCount != 0)
            {
                _target = col.transform.GetChild(0);
            }
            else _target = col.transform;
        }
    }

    private void FixedUpdate()
    {
        SendInfoToBrain();
        
        if (_target != null && _ableToRotate)
        {
            if (AimOnEnemy() && _readyToFire && _working)
            {
                projectileShooter.Shoot();
                _readyToFire = false;
                Invoke("Reload", reloadTime);
            }

            var x = -(transform.position.x -_target.position.x) / 2;
            var y = a * x * x + b * x + c;
            Vector2 direction = new Vector2(x, -y);
            float angle = 180 - Vector2.SignedAngle(direction, Vector2.left);
            Debug.Log(direction + " " + angle + " " + x + " " + y);
            _targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
            
            ClampAngle();
        }
    }
    
    public void Reload()
    {
        _readyToFire = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _target = null;
        }
    }

    public bool SeeEnemy()
    {
        return _target != null;
    }

    public bool AimOnEnemy()
    {
        if (_target == null) return false;
        if (Math.Abs(Quaternion.Angle(_targetRotation, transform.rotation)) < angleOfStartingFire)
        {
            return true;
        }
        return false;
    }

    public void ClampAngle()
    {
        if (transform.rotation.eulerAngles.z > maxAngle)
        {
            transform.rotation = Quaternion.Euler(0, 0, maxAngle);
        }
        if (transform.rotation.eulerAngles.z < minAngle)
        {
            transform.rotation = Quaternion.Euler(0, 0, minAngle);
        }
    }

    void SendInfoToBrain()
    {
        brain.currentAngle = transform.rotation.eulerAngles.z;
        brain.shellMass = projectile.GetComponent<Rigidbody2D>().mass;
        brain.shellMass = projectileShell.initialForce;
    }
    
    public override void ReceiveMessage()
    {
        _ableToRotate = false;
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
            
            Instantiate(visualEffect, turningPoint.position + new Vector3(0,0,1), Quaternion.Euler(new Vector3(0, 0, 0)));
        }
    }
}
