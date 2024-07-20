using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fuelManager : MonoBehaviour
{
    public GameObject fuelObject;
    public GameObject fuelMeter;
    private TestController fuelScript;
    public float fuelRate;
    public float currentAngle;
    public float targetAngle;
    public float rotationSpeed = 100f;

    void Start()
    {
        fuelScript = fuelObject.GetComponent<TestController>();
        fuelRate = 150 / fuelScript.tankCapacity;
        currentAngle = fuelMeter.transform.eulerAngles.z;
    }

    void Update()
    {
        currentAngle = fuelMeter.transform.eulerAngles.z;
        if ( currentAngle > 100)
        {
            currentAngle -= 360;
        }
        targetAngle = -75 + ((fuelScript.tankCapacity-fuelScript.GetFuelLeft()) * fuelRate);
        if (Mathf.Abs(Mathf.Abs(targetAngle) - Mathf.Abs(currentAngle)) > 0.1f)
        {
            float angleToRotate = rotationSpeed * Time.deltaTime;
            if (currentAngle > targetAngle)
            {
                angleToRotate = -angleToRotate;
            }

            fuelMeter.transform.Rotate(0, 0, angleToRotate);
            currentAngle += angleToRotate;

            if (angleToRotate > 0 && currentAngle > targetAngle)
            {
                currentAngle = targetAngle;
                fuelMeter.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
            else if (angleToRotate < 0 && currentAngle < targetAngle)
            {
                currentAngle = targetAngle;
                fuelMeter.transform.rotation = Quaternion.Euler(0, 0, targetAngle);
            }
        }
    }
}
