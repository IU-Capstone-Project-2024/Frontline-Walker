using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHingeJointMotor : MonoBehaviour
{
    [SerializeField] private float _motor_speed;
    private float _current_motor_speed;
    
    private HingeJoint2D _joint;
    
    // Start is called before the first frame update
    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var _motor = _joint.motor;
        _motor.motorSpeed = _current_motor_speed;
        _joint.motor = _motor;
        Debug.Log(_joint.motor.motorSpeed);
    }

    public void Up()
    {
        _current_motor_speed = _motor_speed;
    }

    public void Down()
    {
        _current_motor_speed = -_motor_speed;
    }

    public void Rest()
    {
        _current_motor_speed = 0;
    }
}
