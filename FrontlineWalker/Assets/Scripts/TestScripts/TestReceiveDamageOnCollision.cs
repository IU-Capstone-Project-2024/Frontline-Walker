using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestCharacterPart))]
public class TestReceiveDamageOnCollision : MonoBehaviour
{

    public float appliedDamage = 1f;
    public LayerMask hitMask;

    private TestCharacterPart _characterPart;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterPart = GetComponent<TestCharacterPart>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("CollisionDetected");
        if ((hitMask.value & 1 << other.gameObject.layer) == 1 << other.gameObject.layer)
        {
            _characterPart.TakeDamage(appliedDamage);
        }
    }
}
