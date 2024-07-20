using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapRotate : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public TestTransformCollections uiArea;
    public TestRotationController rotationController;

    private Coroutine rotateCoroutine;

    private void Start()
    {
        rotationController.maxAngle = maxAngle;
        rotationController.minAngle = minAngle;
        rotationController.rotationSpeed = rotationSpeed;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 tapPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tapPosition.z = 0;

            if (IsTapWithinUIArea(Input.mousePosition))
            {
                Vector3 direction = tapPosition - transform.position;
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                targetAngle = Mathf.Clamp(targetAngle, minAngle, maxAngle);

                if (rotateCoroutine != null)
                {
                    StopCoroutine(rotateCoroutine);
                }

                rotateCoroutine = StartCoroutine(RotateToAngle(targetAngle));
            }
        }
    }

    private bool IsTapWithinUIArea(Vector2 tapPosition)
    {
        return uiArea.Contains(tapPosition);
    }

    private IEnumerator RotateToAngle(float targetAngle)
    {
        while (true)
        {
            float currentAngle = rotationController.GetCurrentAngle();
            if (currentAngle < targetAngle - 0.02f) rotationController.Up();
            if (currentAngle > targetAngle + 0.02f) rotationController.Down();

            if (Mathf.Abs(targetAngle - currentAngle) <= 0.02f)
            {
                rotationController.SetCurrentAngle(targetAngle);
                yield return null;    
            }
        }

        rotateCoroutine = null;
    }
}
