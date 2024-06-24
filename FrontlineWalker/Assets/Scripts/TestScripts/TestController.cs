using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TestController : MonoBehaviour
{
    public float speed = 1f;
    public KeyCode right;
    public KeyCode left;

    private float _current_y;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
       if(Mathf.Abs(_rb.velocity.x) < speed)
        {
            if (Input.GetKey(right))
            {
                _rb.AddForce(Vector2.right * 50f);
            }
            if (Input.GetKey(left))
            {
                _rb.AddForce(- Vector2.right * 50f);
            }
        }
    }
}
