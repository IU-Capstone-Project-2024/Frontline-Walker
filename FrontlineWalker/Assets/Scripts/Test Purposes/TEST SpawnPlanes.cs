using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPlanes : MonoBehaviour
{
    public GameObject plane;
    public bool isActivated;

    void Update()
    {
        if (isActivated)
        {
            Instantiate(plane);
            isActivated = false;
        }
    }
}
