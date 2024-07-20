using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class shellsManager : MonoBehaviour
{
    public TestCannon mainCannon;
    public GameObject shellsTextGameObject;
    public RectTransform shellsSprites;
    [Range(0, 1000)]
    //public int numberOfShells = 6;
    //public float shellWidth = 23;
    private float xZero;
    private float _reloadTime;
    private float speed;

    [SerializeField] TextMeshProUGUI shellsText;

    void Start()
    {
        _reloadTime = mainCannon.reloadTime;
        Vector2 position = shellsSprites.anchoredPosition;
        xZero = position.x - (11.5f * 6f);
        Vector2 targetPosition = new Vector2(xZero + (23f * 6), position.y) * Time.timeScale;
        speed = (targetPosition.magnitude - position.magnitude) / _reloadTime;
        Debug.Log("Speed:" + speed);
        
        Vector2 scale = shellsSprites.sizeDelta;
        shellsSprites.sizeDelta = new Vector2(23 * 6, scale.y);
        shellsSprites.anchoredPosition = new Vector2(xZero + (11.5f * 6), position.y);
    }

    void Update()
    {
        shellsText.text = "+" + (mainCannon.GetNumberOfRemainingShells() - 5).ToString();
        if (mainCannon.GetNumberOfRemainingShells() < 6)
        {
            shellsTextGameObject.SetActive(false);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 targetPosition = new Vector2(xZero + (23f * (-3f + mainCannon.GetNumberOfRemainingShells())), position.y) * Time.timeScale;

            if (!mainCannon.ReadyToFire())
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * (-3f + mainCannon.GetNumberOfRemainingShells())), position.y);
            }
        }
        else
        {
            shellsTextGameObject.SetActive(true);
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 targetPosition = new Vector2(xZero + (23f * 2), position.y) * Time.timeScale;

            if (!mainCannon.ReadyToFire())
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * 3), position.y);
            }
        }
    }
}
