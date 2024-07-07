using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDestroyOnTimer : MonoBehaviour
{

    public float timeBeforeDestruction;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeBeforeDestruction);
    }
}
