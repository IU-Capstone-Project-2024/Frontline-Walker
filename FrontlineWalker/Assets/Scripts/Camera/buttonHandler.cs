using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public bool binoculars = false;
    public bool backBinoculars = false;
    public bool shoot = false;
    public bool gun = false;
    public bool stabilize = false;

    public void BinocularsActivate()
    {
        binoculars = true;
    }
    public void BinocularsBack()
    {
        backBinoculars = true;
    }
    public void Shoot()
    {
        shoot = true;
    }
    public void Gun()
    {
        gun = true;
    }
    public void Stabilize()
    {
        stabilize = true;
    }
}
