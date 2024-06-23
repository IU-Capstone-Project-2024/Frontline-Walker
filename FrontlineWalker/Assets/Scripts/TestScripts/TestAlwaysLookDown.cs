using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAlwaysLookDown : MonoBehaviour
{
    [SerializeField] private GameObject [] _objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var _object in _objects)
        {
            _object.transform.SetPositionAndRotation(_object.transform.position, transform.rotation);
        }
    }
}
