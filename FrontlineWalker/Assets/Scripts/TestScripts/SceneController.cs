using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : TestMessageReceiver
{
    public static SceneController instance;

    public int masterVolume;
    public int musicVolume;
    public int soundVolume;

    public volumeManager _volumeManager;
    
    // Start is called before the first frame update
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

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
        musicVolume = _volumeManager.masterVolume;
        soundVolume = _volumeManager.masterVolume;
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
}
