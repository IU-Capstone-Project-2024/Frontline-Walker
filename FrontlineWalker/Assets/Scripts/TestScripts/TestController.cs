using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class TestController : MonoBehaviour
{
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
        if (Mathf.Abs(_rb.velocity.x) < forward_speed)
        {
            _rb.AddForce(Vector2.right * forward_force);
        }
    }

    public void Left()
    {
        if (Mathf.Abs(_rb.velocity.x) < backward_speed)
        {
            _rb.AddForce(-Vector2.right * backward_force);
        }
    }
}
