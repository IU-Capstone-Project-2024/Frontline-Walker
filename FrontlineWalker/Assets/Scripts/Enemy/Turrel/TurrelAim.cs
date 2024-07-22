using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelAim : TestMessageReceiver
{
    
    public float rotationSpeed = 200.0f;
    public float angleOfStartingFire = 5f;
    
    private Transform _target;
    private Quaternion _targetRotation;
    
    private bool _ableToRotate;

    private void Start()
    {
        _ableToRotate = true;
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
        if (_target != null && _ableToRotate)
        {
            Vector3 direction = _target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        }
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
    
    public override void ReceiveMessage()
    {
        _ableToRotate = false;
    }
}
