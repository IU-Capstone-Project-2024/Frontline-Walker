using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessageReceiver : MonoBehaviour
{
    public virtual void ReceiveMessage()
    {
        Debug.Log("Message received");
    }

    public virtual void ReceiveTerminationMessage()
    {
        Debug.Log("Receive termination message");
        Destroy(gameObject);
    }
}