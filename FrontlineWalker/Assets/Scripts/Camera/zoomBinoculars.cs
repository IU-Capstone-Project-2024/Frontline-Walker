using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class zoomBinoculars : MonoBehaviour
{
    public Slider binocularsSlider;
    public Camera cameraObject;
    private cameraInit cameraInit;
    private binoculars binoculars;

    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        binoculars = cameraObject.GetComponent<binoculars>();
    }

    public void zooming()
    {
        if (cameraInit.binocularsActive && binoculars.startZoom && !binoculars.returnBinoculars && !binoculars.activationIsMoving)
        {
            Camera.main.orthographicSize = binocularsSlider.value;
        }
    }
}