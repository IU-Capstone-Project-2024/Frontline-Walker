using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraInit : MonoBehaviour
{
    public float xPos = 0.0f; // X coordinate from the target
    public float yPos = 0.0f; // Y coordinate from the target
    public float cameraZoom = 6.0f; // Initial camera zoom 
    public KeyCode keyBinoculars = KeyCode.LeftShift; // KeyCode for binoculars activation
    public GameObject target;  // Target object
    public bool binocularsActive = false; // Binoculars activation flag (don't touch)

    void Start()
    {
        Camera.main.orthographicSize = cameraZoom;
    }

    void Update()
    {
        if (!binocularsActive)
        {
            var position = target.transform.position;
            transform.position = new Vector3(position.x + xPos, position.y + yPos, -1);
            if (Input.GetKey(keyBinoculars))
            {
                binocularsActive = true;
            }
        }
    }
}
