using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPistonController : MonoBehaviour
{
    [SerializeField] private TestPiston _piston;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            _piston.Up();
        } else if (Input.GetKey(KeyCode.E))
        {
            _piston.Down();
        }
    }
}
