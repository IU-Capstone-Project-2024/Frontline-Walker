using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHingeJointMotorController : MonoBehaviour
{
    [SerializeField] private TestHingeJointMotor _motor;
    [SerializeField] private KeyCode _up_keycode;
    [SerializeField] private KeyCode _down_keycode;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(_up_keycode))
        {
            _motor.Up();
        } else if (Input.GetKey(_down_keycode))
        {
            _motor.Down();
        }
        else
        {
            _motor.Rest();
        }
    }
}
