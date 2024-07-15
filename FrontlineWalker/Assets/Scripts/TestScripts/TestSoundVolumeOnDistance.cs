using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class TestSoundVolumeOnDistance : MonoBehaviour
{
    public float maxVolume = 0.5f;
    public float minDist=1;
    public float maxDist=400;
    
    private AudioSource _audioSource;
    private GameObject _listener;

    public bool showDebugLog = true;
    private void Awake()
    {
        _listener = GameObject.FindWithTag("Player");
        _audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, _listener.transform.position);
 
        if(dist < minDist)
        {
            _audioSource.volume = 1 * maxVolume;
        }
        else if(dist > maxDist)
        {
            _audioSource.volume = 0;
        }
        else
        {
            _audioSource.volume = (1 - (dist - minDist) / (maxDist - minDist)) * maxVolume;
        }
        
        if (showDebugLog) Debug.Log(dist + " " + _audioSource.volume);
    }
}
