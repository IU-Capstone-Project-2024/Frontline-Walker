using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkerController : MonoBehaviour
{
    public TestController controller;

    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    public KeyCode setToInitialTorsoHeight;
    public KeyCode distabilize;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
