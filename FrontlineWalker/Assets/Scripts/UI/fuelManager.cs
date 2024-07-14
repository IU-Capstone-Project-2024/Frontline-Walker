using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class fuelManager : MonoBehaviour
{
    public GameObject fuelObject;
    private TestController fuelScript;

    [SerializeField] TextMeshProUGUI fuelText;

    void Start()
    {
        fuelScript = fuelObject.GetComponent<TestController>();
    }

    void Update()
    {
        fuelText.text = "Fuel: " + Mathf.RoundToInt(fuelScript.GetFuelLeft()).ToString() + "/" + fuelScript.tankCapacity.ToString();
    }
}
