using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class shellsManager : MonoBehaviour
{
    public GameObject mainCannon;
    public GameObject shellsTextGameObject;
    public RectTransform shellsSprites;
    private TestCannon cannonScript;
    private float xZero;

    [SerializeField] TextMeshProUGUI shellsText;

    void Start()
    {
        cannonScript = mainCannon.GetComponent<TestCannon>();
        Vector2 position = shellsSprites.anchoredPosition;
        xZero = position.x - (11.5f * 8f);
    }

    void Update()
    {
        shellsText.text = "+" + (cannonScript.GetNumberOfRemainingShells() - 8).ToString();
        if (cannonScript.GetNumberOfRemainingShells() < 9)
        {
            shellsTextGameObject.SetActive(false);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 scale = shellsSprites.sizeDelta;
            shellsSprites.sizeDelta = new Vector2(23 * cannonScript.GetNumberOfRemainingShells(), scale.y);
            shellsSprites.anchoredPosition = new Vector2(xZero + (11.5f * cannonScript.GetNumberOfRemainingShells()), position.y);
        }
        else
        {
            shellsTextGameObject.SetActive(true);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 scale = shellsSprites.sizeDelta;
            shellsSprites.sizeDelta = new Vector2(23 * 8, scale.y);
            shellsSprites.anchoredPosition = new Vector2(xZero + (11.5f * 8f), position.y);
        }
    }
}
