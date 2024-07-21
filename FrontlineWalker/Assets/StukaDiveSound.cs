using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(AudioManager))]
public class StukaDiveSound : TestMessageReceiver
{

    public TestMessageReceiver messageReceiver;
    
    private void Start()
    {
        GetComponent<AudioManager>().Play("dive");
    }

    public override void ReceiveTerminationMessage()
    {
        GetComponent<AudioManager>().Play("crush");
        messageReceiver.ReceiveMessage();
    }
}
