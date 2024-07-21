using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class volumeManager : MonoBehaviour
{
    public int musicVolume;
    public int soundVolume;
    public int masterVolume;

    public GameObject GA_musicVolume;
    public GameObject GA_soundVolume;
    public GameObject GA_masterVolume;

    private RadialSlider RS_musicVolume;
    private RadialSlider RS_soundVolume;
    private RadialSlider RS_masterVolume;

    // Start is called before the first frame update
    void Start()
    {
        RS_musicVolume = GA_musicVolume.GetComponent<RadialSlider>();
        RS_soundVolume = GA_soundVolume.GetComponent<RadialSlider>();
        RS_masterVolume = GA_masterVolume.GetComponent<RadialSlider>();
    }

    // Update is called once per frame
    void Update()
    {
        musicVolume = (180 - RS_musicVolume.currentValue) * 100 / 180;
        soundVolume = (180 - RS_soundVolume.currentValue) * 100 / 180;
        masterVolume = (180 - RS_masterVolume.currentValue) * 100 / 180;
    }
    
    
}
