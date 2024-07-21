using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SetControllerData : MonoBehaviour
{
    public static SetControllerData instance;

    public float initialVolume;
    public int masterVolume;
    public int musicVolume;
    public int soundVolume;
    public AudioSource audioSource;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        audioSource = GetComponent<AudioSource>();
    }
}
