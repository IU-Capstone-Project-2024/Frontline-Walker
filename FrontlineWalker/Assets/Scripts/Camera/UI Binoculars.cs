using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBinoculars : MonoBehaviour
{
    public float scaleSpeed = 3f; // Sprite scaling speed
    public Vector3 inScale = new Vector3(1,1,1); // Activated scaling
    public Vector3 outScale = new Vector3(3, 3, 1); // Deactivated scaling
    
    public Camera cameraObject;
    private cameraInit cameraInit;
    private binoculars binoculars;
    private RectTransform rectTransform;

    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        binoculars = cameraObject.GetComponent<binoculars>();
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.Scale(rectTransform.localScale, outScale);
    }

    void Update()
    {
        if (cameraInit.binocularsActive)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, inScale, scaleSpeed * Time.deltaTime);
        }

        if (binoculars.endZoom || !cameraInit.binocularsActive)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, outScale, scaleSpeed * Time.deltaTime);
        }
    }
}
