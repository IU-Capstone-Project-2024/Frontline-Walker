using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class shellsManager : MonoBehaviour
{
    public GameObject mainCannon;
    private TestCannon cannonScript;

    [SerializeField] TextMeshProUGUI shellsText;

    void Start()
    {
        cannonScript = mainCannon.GetComponent<TestCannon>();
    }

    void Update()
    {
        shellsText.text = "Shells: " + cannonScript.GetNumberOfRemainingShells().ToString();
    }
}
