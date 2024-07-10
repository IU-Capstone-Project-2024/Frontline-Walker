using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacterPart : MonoBehaviour
{
    [SerializeField] private String name;
    [Range(0, 1000)]
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private bool isCritical;

    private int _health;
    private bool _isWorking;
    
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

    // Update is called once per frame
    void Update()
    {
        
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
            Debug.Log(name + " took critical damage");
            if (isCritical)
            {
                Debug.Log(name + " was critical part");
            }
        }
    }
     
    public int GetHealth()
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
    
    public void TakeDamage(int _damageAmount)
    {
        if (_damageAmount < 0)
        {
            throw new Exception("Amount of damage can not be negative!");
        }
        _health -= _damageAmount;
        
        ClampHealth();
        Debug.Log("Damage taken by " + name +  ", health left - " + _health);
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
        Debug.Log("Collision detected");
        var appliedDamage = other.gameObject.GetComponent<TestAppliesDamage>();
        if (appliedDamage != null)
        {
            Debug.Log("Damage component received");
            TakeDamage(appliedDamage.damage);
        }
    }
}
