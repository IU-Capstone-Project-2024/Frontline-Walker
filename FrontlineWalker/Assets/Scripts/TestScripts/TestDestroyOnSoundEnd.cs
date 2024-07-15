using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TestDestroyOnSoundEnd : MonoBehaviour
{

    private AudioSource _as;
    
    // Start is called before the first frame update
    void Start()
    {
        _as = GetComponent<AudioSource>();
        Debug.Log("Sound instantiated");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_as.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
