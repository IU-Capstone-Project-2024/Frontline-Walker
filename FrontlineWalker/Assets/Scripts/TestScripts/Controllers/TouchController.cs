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
    public GameObject aimSlider;
    public GameObject aimSliderWorker;

    public GameObject binocularsManager;
    private buttonHandler buttonHandler;
    public Camera cameraObject;
    private cameraInit cameraInit;

    public GameObject verSliderGameObject;
    public GameObject horSliderGameObject;
    public GameObject UI_backdrop;
    public GameObject shellsTextGameObject;
    public GameObject fuelTextGameObject;
    public GameObject stabilizeGameObject;
    public GameObject shootGameObject;

    [SerializeField] TextMeshProUGUI aimText;
    private RadialSlider aimSliderScript;


    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        buttonHandler = binocularsManager.GetComponent<buttonHandler>();
        aimSliderScript = aimSlider.GetComponent<RadialSlider>();
        controller.mainCannonController.setMaxAngle(aimSliderScript.angleMax);
        controller.mainCannonController.setMinAngle(-aimSliderScript.angleMin);
    }

    void FixedUpdate()
    {
        if (!cameraInit.binocularsActive)
        {
            verSliderGameObject.SetActive(true);
            horSliderGameObject.SetActive(true);
            aimSliderWorker.SetActive(true);
            stabilizeGameObject.SetActive(true);
            shootGameObject.SetActive(true);
            UI_backdrop.SetActive(true);
            shellsTextGameObject.SetActive(true);
            fuelTextGameObject.SetActive(true);

            aimText.text = aimSliderScript.currentValue.ToString() + "°";

            if (buttonHandler.shoot)
            {
                controller.mainCannonController.Fire();
                buttonHandler.shoot = false;
            }
            controller.mainCannonController.SetTargetAngle(aimSliderScript.currentValue);
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
            aimSliderWorker.SetActive(false);
            stabilizeGameObject.SetActive(false);
            shootGameObject.SetActive(false);
            UI_backdrop.SetActive(false);
            shellsTextGameObject.SetActive(false);
            fuelTextGameObject.SetActive(false);
            verSlider.value = 0;
            horSlider.value = 0;
            buttonHandler.shoot = false;
            buttonHandler.stabilize = false;
        }
    }
}
