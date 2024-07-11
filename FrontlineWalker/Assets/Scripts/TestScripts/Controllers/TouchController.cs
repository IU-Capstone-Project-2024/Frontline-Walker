using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{
    public TestController controller;

    public Slider verSlider;
    public Slider horSlider;
    public Slider aimSlider;

    public GameObject binocularsManager;
    private buttonHandler buttonHandler;
    public Camera cameraObject;
    private cameraInit cameraInit;

    public GameObject verSliderGameObject;
    public GameObject horSliderGameObject;
    public GameObject aimSliderGameObject;
    public GameObject aimTextGameObject;
    public GameObject shellsTextGameObject;
    public GameObject fuelTextGameObject;
    public GameObject stabilizeGameObject;
    public GameObject shootGameObject;

    [SerializeField] TextMeshProUGUI aimText;


    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        buttonHandler = binocularsManager.GetComponent<buttonHandler>();
        
        controller.mainCannonController.setMaxAngle(aimSlider.maxValue);
        controller.mainCannonController.setMinAngle(aimSlider.minValue);
    }

    void FixedUpdate()
    {
        if (!cameraInit.binocularsActive)
        {
            verSliderGameObject.SetActive(true);
            horSliderGameObject.SetActive(true);
            aimSliderGameObject.SetActive(true);
            stabilizeGameObject.SetActive(true);
            shootGameObject.SetActive(true);
            aimTextGameObject.SetActive(true);
            shellsTextGameObject.SetActive(true);
            fuelTextGameObject.SetActive(true);

            aimText.text = aimSlider.value + "°";

            if (buttonHandler.shoot)
            {
                controller.mainCannonController.Fire();
                buttonHandler.shoot = false;
            }
            controller.mainCannonController.SetTargetAngle(aimSlider.value);
            if (verSlider.value >= 1)
            {
                controller.torsoController.Up();
            }
            if (verSlider.value <= -1)
            {
                controller.torsoController.Down();
            }
            if (horSlider.value >= 1)
            {
                controller.Right();
            }
            if (horSlider.value <= -1)
            {
                controller.Left();
            }

            if (buttonHandler.stabilize)
            {
                if (controller.torsoController.isDistabilazed())
                {
                    controller.torsoController.Stabilize();
                }
                else
                {
                    if (controller.torsoController.isMovingToInitialHeight())
                    {
                        controller.torsoController.StopMovingToInitialHeight();
                    }
                    else
                    {
                        controller.torsoController.StartMovingToInitialHeight();
                    }
                }
                buttonHandler.stabilize = false;
            }
        }
        else
        {
            verSliderGameObject.SetActive(false);
            horSliderGameObject.SetActive(false);
            aimSliderGameObject.SetActive(false);
            stabilizeGameObject.SetActive(false);
            shootGameObject.SetActive(false);
            aimTextGameObject.SetActive(false);
            shellsTextGameObject.SetActive(false);
            fuelTextGameObject.SetActive(false);
            verSlider.value = 0;
            horSlider.value = 0;
            buttonHandler.shoot = false;
            buttonHandler.stabilize = false;
        }
    }
}
