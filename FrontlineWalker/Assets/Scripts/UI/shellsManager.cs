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
    public float speed = 5f;

    [SerializeField] TextMeshProUGUI shellsText;

    void Start()
    {
        cannonScript = mainCannon.GetComponent<TestCannon>();
        Vector2 position = shellsSprites.anchoredPosition;
        xZero = position.x - (11.5f * 5f);
        shellsSprites.anchoredPosition = new Vector2(xZero + (11.5f * 6), position.y);
    }

    void Update()
    {
        shellsText.text = "+" + (cannonScript.GetNumberOfRemainingShells() - 5).ToString();
        if (cannonScript.GetNumberOfRemainingShells() < 6)
        {
            shellsTextGameObject.SetActive(false);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 targetPosition = new Vector2(xZero + (23f * (-3f + cannonScript.GetNumberOfRemainingShells())), position.y) * Time.timeScale;

            if (cannonScript._cannonIsFired)
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);

                if (shellsSprites.anchoredPosition.x <= targetPosition.x)
                {
                    cannonScript._cannonIsFired = false;
                }
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * (-3f + cannonScript.GetNumberOfRemainingShells())), position.y);
            }
        }
        else
        {
            shellsTextGameObject.SetActive(true);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 targetPosition = new Vector2(xZero + (23f * 2), position.y) * Time.timeScale;

            if (cannonScript._cannonIsFired)
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);

                if (shellsSprites.anchoredPosition.x <= targetPosition.x)
                {
                    cannonScript._cannonIsFired = false;
                }
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * 3), position.y);
            }
        }
    }
}
