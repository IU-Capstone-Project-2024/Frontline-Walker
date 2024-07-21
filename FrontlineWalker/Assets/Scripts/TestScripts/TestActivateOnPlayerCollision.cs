using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActivateOnPlayerCollision : MonoBehaviour
{

    public GameObject objectToActivate;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
        }
    }
}
