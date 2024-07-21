using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TakeVolumeFromSceneController : MonoBehaviour
{

    private float _initialVolume;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _initialVolume = _audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        _audioSource.volume = _initialVolume * SceneController.instance.masterVolume / 100 *
            SceneController.instance.soundVolume / 100;
    }
}
