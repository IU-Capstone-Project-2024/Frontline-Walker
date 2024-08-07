using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapRotate : MonoBehaviour
{
    public TestTransformCollections uiArea;
    public TestCannon testCannon;

    private float _targetAngle;
    public bool activation;

    private void FixedUpdate()
    {
        if (activation)
        {
            Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tapPosition.z = 0;

            if (IsTapWithinUIArea(Input.mousePosition))
            {
                Vector3 direction = tapPosition - transform.position;
                _targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                testCannon.SetTargetAngle(_targetAngle);
            }
        }
    }

    public void PointerDown()
    {
        activation = true;
    }

    public void PointerUp()
    {
        activation = false;
    }

    private bool IsTapWithinUIArea(Vector2 tapPosition)
    {
        return uiArea.Contains(tapPosition);
    }
}