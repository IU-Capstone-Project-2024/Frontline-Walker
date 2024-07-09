using System.Collections;
using System.Collections.Generic;
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
    public GameObject stabilizeGameObject;
    public GameObject shootGameObject;


    void Start()
    {
        cameraInit = cameraObject.GetComponent<cameraInit>();
        buttonHandler = binocularsManager.GetComponent<buttonHandler>();
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

            if (aimSlider.value >= 1)
            {
                controller.mainCannonController.Up();
            }
            if (aimSlider.value <= -1)
            {
                controller.mainCannonController.Down();
            }
            if (buttonHandler.shoot)
            {
                controller.mainCannonController.Fire();
                buttonHandler.shoot = false;
            }
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
            verSlider.value = 0;
            horSlider.value = 0;
            aimSlider.value = 0;
            buttonHandler.shoot = false;
            buttonHandler.stabilize = false;
        }
    }
}
