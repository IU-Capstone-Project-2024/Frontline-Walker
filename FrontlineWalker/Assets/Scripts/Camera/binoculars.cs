using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class binoculars : MonoBehaviour
{
    public float activateSpeed = 10f; // Start zooming speed
    public float returnSpeed = 10f; // End zooming speed
    public float activationMoveDistance = 5f;
    public float activationMoveDuration = 2f;
    public float maxX = 30f;
    public float minX = -30f;
    public float maxY = 20f;
    public float minY = -8f;
    public float maxZoom = 30f;
    public float minZoom = 20f;
    public float zoomPerOne;
    public cameraInit cameraInit; // Camera script
    public GameObject binocularsSliderSystem;
    public GameObject binocularsSliderGameObject;
    public GameObject binocularsStick;
    public GameObject binocularsButton;
    public GameObject backButton;
    public GameObject binocularsManager;
    private buttonHandler buttonHandler;
    private RadialSlider binocularsSlider;
    public bool startZoom = false; // Flag for start zooming (don't touch)
    public bool returnBinoculars = false;  // Flag for returning to initial camera position (don't touch)
    public bool endZoom = false; // Flag for end zooming (don't touch)
    public bool previousPositionSaved = false;

    private Vector3 actStartPosition;
    private Vector3 actTargetPosition;
    private Vector3 previousPosition;
    private float actElapsedTime = 0f;
    public bool activationIsMoving = false;

    [SerializeField] private InputActionReference moveActionToUse;
    [SerializeField] private float moveSpeed;

    void Start()
    {
        buttonHandler = binocularsManager.GetComponent<buttonHandler>();
        binocularsSlider = binocularsSliderSystem.GetComponent<RadialSlider>();
        previousPosition = new Vector3(float.MinValue, 0, -1);
        binocularsSliderGameObject.SetActive(false);
        binocularsStick.SetActive(false);
        zoomPerOne = (maxZoom-minZoom) / (binocularsSlider.angleMax + binocularsSlider.angleMin);
    }

    void Update()
    {
        if (cameraInit.binocularsActive)
        {
            // Binoculars activation process
            if (Camera.main.orthographicSize < maxZoom - binocularsSlider.currentValue*zoomPerOne && !startZoom && !returnBinoculars)
            {

                Camera.main.orthographicSize += activateSpeed * Time.deltaTime;
                binocularsButton.SetActive(false);
                if (Camera.main.orthographicSize >= maxZoom - binocularsSlider.currentValue * zoomPerOne)
                {
                    Camera.main.orthographicSize = maxZoom - binocularsSlider.currentValue * zoomPerOne;
                    startZoom = true;

                    actStartPosition = transform.position;
                    if (previousPosition.x - transform.position.x <= activationMoveDistance)
                    {
                        actTargetPosition = actStartPosition + new Vector3(activationMoveDistance, 0, -1);
                    }
                    else
                    {
                        actTargetPosition = previousPosition;
                    }
                    previousPositionSaved = false;

                    actElapsedTime = 0f;
                    activationIsMoving = true;
                }
            }

            if (activationIsMoving)
            {
                actElapsedTime += Time.deltaTime;
                float t = Mathf.Clamp(actElapsedTime / activationMoveDuration, 0, 1);
                transform.position = Vector3.Lerp(actStartPosition, actTargetPosition, t);

                if (t >= 1)
                {
                    activationIsMoving = false;
                }
            }

            // Moving binoculars
            if (startZoom && !returnBinoculars && !activationIsMoving)
            {
                binocularsSliderGameObject.SetActive(true);
                binocularsStick.SetActive(true);
                backButton.SetActive(true);
                Vector2 moveDirection = moveActionToUse.action.ReadValue<Vector2>();

                if ((moveDirection.x > 0 && transform.localPosition.x >= maxX) || (moveDirection.x < 0 && transform.localPosition.x <= minX))
                {
                    moveDirection.x = 0;
                }

                if ((moveDirection.y > 0 && transform.localPosition.y >= maxY) || (moveDirection.y < 0 && transform.localPosition.y <= minY))
                {
                    moveDirection.y = 0;
                }

                transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

                if (buttonHandler.backBinoculars)
                {
                    returnBinoculars = true;
                    binocularsSliderGameObject.SetActive(false);
                    binocularsStick.SetActive(false);
                    backButton.SetActive(false);
                }
            }

            // Returning to initial camera position
            if (returnBinoculars && !endZoom)
            {
                if (!previousPositionSaved)
                {
                    previousPosition = transform.position;
                    previousPositionSaved = true;
                }
                
                actTargetPosition = transform.position;
                
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
                Camera.main.orthographicSize -= activateSpeed * Time.deltaTime;
                if (Camera.main.orthographicSize <= cameraInit.cameraZoom)
                {
                    Camera.main.orthographicSize = cameraInit.cameraZoom;
                    cameraInit.binocularsActive = false;
                    startZoom = false;
                    returnBinoculars = false;
                    endZoom = false;
                    binocularsButton.SetActive(true);
                }
            }
            buttonHandler.binoculars = false;
            buttonHandler.backBinoculars = false;
        }
    }
}
