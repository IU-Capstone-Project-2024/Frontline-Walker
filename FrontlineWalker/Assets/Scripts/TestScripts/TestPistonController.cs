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
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _piston.Up();
        } else if (Input.GetKey(KeyCode.S))
        {
            _piston.Down();
        }
    }
}
