using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(AudioManager))]
public class StukaDiveSound : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioManager>().Play("dive");
    }
}
