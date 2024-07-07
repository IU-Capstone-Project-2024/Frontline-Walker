using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonHandler : MonoBehaviour
{
    public bool activate = false; // Button flag

    public void onClick()
    {
        activate = true;
    }
}
