using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binoculars : MonoBehaviour
{
    public KeyCode keyOff = KeyCode.LeftShift; // KeyCode for activation
    public KeyCode keyHigh = KeyCode.Q; // KeyCode for zoom out
    public KeyCode keyLow = KeyCode.E; // KeyCode for zoom in
    public float maxZoom = 15f; // Maximum zooming
    public float minZoom = 10f; // Minimum zooming
    public float activateSpeed = 10f; // Start zooming speed
    public float returnSpeed = 10f; // End zooming speed
    public float zoomSpeed = 3f; // Changing zoom speed
    public float moveSpeed = 5f; // Moving speed

    public cameraInit cameraInit; // Camera script

    public bool startZoom = false; // Flag for start zooming (don't touch)
    public bool returnBinoculars = false;  // Flag for returning to initial camera position (don't touch)
    public bool endZoom = false; // Flag for end zooming (don't touch)

    void Update()
    {
        if (cameraInit.binocularsActive)
        {
            // Binoculars activation process
            if (Camera.main.orthographicSize < maxZoom && !startZoom && !returnBinoculars)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize + activateSpeed * Time.deltaTime;
                if (Camera.main.orthographicSize >= maxZoom)
                {
                    Camera.main.orthographicSize = maxZoom;
                    startZoom = true;
                }
            }

            // Moving binoculars
            if (!returnBinoculars)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector2 movement = new Vector2(moveHorizontal, moveVertical);
                transform.Translate(movement * moveSpeed * Time.deltaTime);
            }

            // Changing zoom value and deactivation start
            if (startZoom && !returnBinoculars)
            {
                if (Input.GetKey(keyHigh) && Camera.main.orthographicSize < maxZoom)
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize + zoomSpeed * Time.deltaTime;
                    if (Camera.main.orthographicSize > maxZoom)
                    {
                        Camera.main.orthographicSize = maxZoom;
                    }
                }

                if (Input.GetKey(keyLow) && Camera.main.orthographicSize > minZoom)
                {
                    Camera.main.orthographicSize = Camera.main.orthographicSize - zoomSpeed * Time.deltaTime;
                    if (Camera.main.orthographicSize < minZoom)
                    {
                        Camera.main.orthographicSize = minZoom;
                    }
                }

                if (Input.GetKey(keyOff))
                {
                    returnBinoculars = true;
                }
            }

            // Returning to initial camera position
            if (returnBinoculars && !endZoom)
            {
                var position = cameraInit.target.transform.position;
                transform.position = Vector3.Lerp(transform.position, new Vector3(position.x + cameraInit.xPos, position.y + cameraInit.yPos, -1), returnSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, new Vector3(position.x + cameraInit.xPos, position.y + cameraInit.yPos, -1)) < 0.01f)
                {
                    endZoom = true;
                    transform.position = new Vector3(position.x + cameraInit.xPos, position.y + cameraInit.yPos, -1);
                }
            }

            // Binoculars deactivation process
            if (endZoom)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize - activateSpeed * Time.deltaTime;
                if (Camera.main.orthographicSize <= cameraInit.cameraZoom)
                {
                    Camera.main.orthographicSize = cameraInit.cameraZoom;
                    cameraInit.binocularsActive = false;
                    startZoom = false;
                    returnBinoculars = false;
                    endZoom = false;
                }
            }
        }
    }
}
