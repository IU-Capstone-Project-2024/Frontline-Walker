using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessangeSender : MonoBehaviour
{
    public TestMessageReceiver receiver;

    public void SendMessage()
    {
        //Debug.Log("message sended");
        if (receiver != null)
        {
            receiver.ReceiveMessage();    
        }
    }

    public void SendTerminationMessage()
    {
        if (receiver != null)
        {
            receiver.ReceiveTerminationMessage();    
        }
    }
}
