using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public Camera cameraObject;
    private cameraInit cameraInit;

    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
    }

    void Update()
    {
        if (!cameraInit.binocularsActive)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}
