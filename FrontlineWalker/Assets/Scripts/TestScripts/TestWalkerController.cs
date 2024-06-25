using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkerController : MonoBehaviour
{
    public TestController controller;
    public TestTorsoController torsoController;

    public KeyCode up;
    public KeyCode down;
    public KeyCode right;
    public KeyCode left;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(up))
        {
            torsoController.Up();
        }
        if (Input.GetKey(down))
        {
            torsoController.Down();
        }
        if (Input.GetKey(right))
        {
            controller.Right();
        }
        if (Input.GetKey(left))
        {
            controller.Left();
        }
    }
}
