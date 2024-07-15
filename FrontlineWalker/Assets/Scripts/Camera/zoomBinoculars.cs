using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoomBinoculars : MonoBehaviour
{
    public GameObject binocularsSliderObject;
    public Camera cameraObject;
    private cameraInit cameraInit;
    private binoculars binoculars;
    private RadialSlider binocularsSlider;

    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        binoculars = cameraObject.GetComponent<binoculars>();
        binocularsSlider = binocularsSliderObject.GetComponent<RadialSlider>();
    }

    void Update()
    {
        if (cameraInit.binocularsActive && binoculars.startZoom && !binoculars.returnBinoculars && !binoculars.activationIsMoving)
        {
            Camera.main.orthographicSize = binoculars.maxZoom - (binocularsSlider.currentValue * binoculars.zoomPerOne);
        }
    }
}