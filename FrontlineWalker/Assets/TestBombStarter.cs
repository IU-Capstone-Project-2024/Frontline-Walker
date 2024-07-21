using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestBomb))]
public class TestBombStarter : TestMessageReceiver
{

    private TestBomb testBomb;

    private void Start()
    {
        testBomb = GetComponent<TestBomb>();
    }

    public override void ReceiveTerminationMessage()
    {
        testBomb.Explode();
    }
}
