using System.Collections;
using System.Collections.Generic;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : TestMessageReceiver
{
    public static SceneController instance;

    public int masterVolume;
    public int musicVolume;
    public int soundVolume;

    public volumeManager _volumeManager;

    private AudioSource _audioSource;
    private float _initialVolume;

    private SetControllerData setControllerData;
    
    // Start is called before the first frame update
    void Awake()
    {
        setControllerData = FindObjectOfType<SetControllerData>().GetComponent<SetControllerData>();

        masterVolume = setControllerData.masterVolume;
        musicVolume = setControllerData.musicVolume;
        soundVolume = setControllerData.soundVolume;
        _initialVolume = setControllerData.initialVolume;
        
        if (_audioSource == null)
        {
            _audioSource = setControllerData.audioSource;
        }
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        _volumeManager.GA_masterVolume.GetComponent<RadialSlider>().currentValue = masterVolume;
        _volumeManager.GA_musicVolume.GetComponent<RadialSlider>().currentValue = musicVolume;
        _volumeManager.GA_soundVolume.GetComponent<RadialSlider>().currentValue = soundVolume;
    }

    // Update is called once per frame
    void Update()
    {
        masterVolume = _volumeManager.masterVolume;
        musicVolume = _volumeManager.musicVolume;
        soundVolume = _volumeManager.soundVolume;

        _audioSource.volume = _initialVolume * musicVolume / 100f * masterVolume / 100f;
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Scenes/BarabachaTestScene");
    }
    
    public override void ReceiveMessage()
    {
        Debug.Log("Message received");
    }

    public override void ReceiveTerminationMessage()
    {
        
        
        Debug.Log("Receive termination message");
        LoadLevel1();
        Time.timeScale = 1;
    }

    public void FillSceneControllerData()
    {
        setControllerData.masterVolume = masterVolume;
        setControllerData.musicVolume = musicVolume;
        setControllerData.soundVolume = soundVolume;
        setControllerData.initialVolume = _initialVolume;
    }
}
