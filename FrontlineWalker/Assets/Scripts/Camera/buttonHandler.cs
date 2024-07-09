using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public bool binoculars = false;
    public bool shoot = false;
    public bool stabilize = false;

    public void BinocularsActivate()
    {
        binoculars = true;
    }
    public void Shoot()
    {
        shoot = true;
    }
    public void Stabilize()
    {
        stabilize = true;
    }
}
