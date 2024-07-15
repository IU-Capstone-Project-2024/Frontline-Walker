using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkerGunRotate : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public TestTransformCollections uiArea;

    private Coroutine rotateCoroutine;

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
            float currentAngle = transform.eulerAngles.z;
            if (currentAngle > 180) currentAngle -= 360;
            float angle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (Mathf.Approximately(angle, targetAngle))
                break;

            yield return null;
        }

        rotateCoroutine = null;
    }
}
