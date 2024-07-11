using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterPart : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private String name;
    [Range(0, 1000)]
    [SerializeField] private float maxHealth = 10;
    [SerializeField] private bool isCritical;
    public TestMessangeSender messanger;
    [Header("Debug")] 
    public bool alwaysSendMessage = true;
    public bool showDebugLog = true;
    
    private float _health;
    public bool _isWorking;

    private void FixedUpdate()
    {
        if (alwaysSendMessage && !_isWorking)
        {
            messanger.SendMessage();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (name == null)
        {
            name = new string("Unnamed part");
        }

        _health = maxHealth;
        _isWorking = true;
    }

    private void ClampHealth()
    {
        if (_health > maxHealth) _health = maxHealth;
        if (_health < 0) _health = 0;
    }

    private void CheckHealth()
    {
        _isWorking = _health > 0;
        if (!_isWorking)
        {
            if (showDebugLog) Debug.Log(name + " took critical damage");

            if (messanger != null)
            {
                messanger.SendMessage();
            }
            
            if (isCritical)
            {
                if (showDebugLog) Debug.Log(name + " was critical part");
            }
        }
    }
     
    public float GetHealth()
    {
        return _health;
    }

    public void FixHealth(int _fixAmount)
    {
        if (_fixAmount < 0)
        {
            throw new Exception("Amount of fix can not be negative!");
        }
        _health += _fixAmount;
        ClampHealth();
        CheckHealth();
    }
    
    public void TakeDamage(float _damageAmount)
    {
        if (_damageAmount < 0)
        {
            throw new Exception("Amount of damage can not be negative!");
        }
        _health -= _damageAmount;
        
        ClampHealth();
        if (showDebugLog) Debug.Log("Damage taken by " + name +  ", health left - " + _health);
        CheckHealth();
    }

    public bool IsCritical()
    {
        return isCritical;
    }

    public String GetName()
    {
        return name;
    }

    public bool IsWorking()
    {
        return _isWorking;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision detected");
        var appliedDamage = other.gameObject.GetComponent<TestAppliesDamage>();
        if (appliedDamage != null)
        {
            //Debug.Log("Damage component received");
            TakeDamage(appliedDamage.damage);
        }
    }

    public void PrintConsoleHealth()
    {
        Debug.Log(name +  ", health left - " + _health);
    }
}
