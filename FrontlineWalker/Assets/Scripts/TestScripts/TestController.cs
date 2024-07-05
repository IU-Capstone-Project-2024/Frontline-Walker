using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class TestController : MonoBehaviour
{
    public TestProceduralWalkerAnimation proceduralWalkerAnimation;
    public TestTorsoController torsoController;
    
    public float forward_speed = 2f;
    public float backward_speed = 1f;
    public float forward_force = 50f;
    public float backward_force = 50f;

    private float _current_y;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Right()
    {
        if (AbleToMove())
        {
            var _forward_speed = torsoController.GetCurrentYRatio() * forward_speed;
            if (Mathf.Abs(_rb.velocity.x) < _forward_speed)
            {
                _rb.AddForce(Vector2.right * forward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _forward_speed;
            }
        }
    }

    public void Left()
    {
        if (AbleToMove())
        {
            var _backward_speed = torsoController.GetCurrentYRatio() * backward_speed;
            if (Mathf.Abs(_rb.velocity.x) < _backward_speed)
            {
                _rb.AddForce(-Vector2.right * backward_force);
                proceduralWalkerAnimation.shakeHeight = 0.01f * _backward_speed;
            }
        }
    }

    private bool AbleToMove()
    {
        return torsoController.isStabilized();
    }
}