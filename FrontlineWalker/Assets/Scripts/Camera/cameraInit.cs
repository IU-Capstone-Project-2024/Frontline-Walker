using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cameraInit : MonoBehaviour
{
    public float xPos = 0.0f; // X coordinate from the target
    public float yPos = 0.0f; // Y coordinate from the target
    public float cameraZoom = 6.0f; // Initial camera zoom 
    public GameObject target;  // Target object
    public bool binocularsActive = false; // Binoculars activation flag (don't touch)
    
    public GameObject binocularsManager;
    private buttonHandler buttonHandler;

    void Start()
    {
        buttonHandler = binocularsManager.GetComponent<buttonHandler>();
        Camera.main.orthographicSize = cameraZoom;
    }

    void Update()
    {
        if (!binocularsActive)
        {
            var position = target.transform.position;
            transform.position = new Vector3(position.x + xPos, position.y + yPos, -1);
            if (buttonHandler.activate)
            {
                binocularsActive = true;
            }
            buttonHandler.activate = false;
        }
    }
}
