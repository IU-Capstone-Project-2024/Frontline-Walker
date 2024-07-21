using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(AudioManager))]
public class StukaDiveEffects : TestMessageReceiver
{

    public TestMessageReceiver messageReceiver;
    public GameObject fireEffect;
    public Transform fireEffectTransform;
    
    private void Start()
    {
        GetComponent<AudioManager>().Play("dive");
    }

    public override void ReceiveTerminationMessage()
    {
        var effect = Instantiate(fireEffect, fireEffectTransform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        effect.transform.parent = gameObject.transform;
        GetComponent<AudioManager>().Play("crush");
        messageReceiver.ReceiveMessage();
    }
}
