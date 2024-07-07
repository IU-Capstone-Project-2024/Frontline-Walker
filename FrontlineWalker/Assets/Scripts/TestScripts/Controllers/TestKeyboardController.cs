using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyboardController : MonoBehaviour
{
    public TestController controller;

    public KeyCode upCannon;
    public KeyCode downCannon;
    public KeyCode FireMainCannon;
    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode setToInitialTorsoHeight;
    public KeyCode distabilize;
    
    void FixedUpdate()
    {
        if (Input.GetKey(upCannon))
        {
            controller.mainCannonController.Up();
        }
        if (Input.GetKey(downCannon))
        {
            controller.mainCannonController.Down();
        }
        if (Input.GetKeyDown(FireMainCannon))
        {
            controller.mainCannonController.Fire();    
        }
        if (Input.GetKey(up))
        {
            controller.torsoController.Up();
        }
        if (Input.GetKey(down))
        {
            controller.torsoController.Down();
        }
        if (Input.GetKey(right))
        {
            controller.Right();
        }
        if (Input.GetKey(left))
        {
            controller.Left();
        }

        if (Input.GetKeyDown(setToInitialTorsoHeight))
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
        }

        if (Input.GetKeyDown(distabilize))
        {
            controller.torsoController.Distabilaze();
        }
    }
}