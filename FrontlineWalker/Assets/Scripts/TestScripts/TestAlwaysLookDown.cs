using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlwaysLookDown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -90), 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
