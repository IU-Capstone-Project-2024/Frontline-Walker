using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestResupplyArea : MonoBehaviour
{

    public GameObject SoundEffect;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<TestController>().ReceiveResupply();
            Instantiate(SoundEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
