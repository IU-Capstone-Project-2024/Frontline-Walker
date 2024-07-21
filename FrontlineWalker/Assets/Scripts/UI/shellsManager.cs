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
        
        Vector2 scale = shellsSprites.sizeDelta;
        shellsSprites.sizeDelta = new Vector2(23 * 6, scale.y);
        shellsSprites.anchoredPosition = new Vector2(xZero + (11.5f * 6), position.y);

        shellsTextGameObject.SetActive(true);
    }

    void Update()
    {
        shellsText.text = mainCannon.GetNumberOfRemainingShells().ToString();
        if (mainCannon.GetNumberOfRemainingShells() < 6)
        {
            Vector2 position = shellsSprites.anchoredPosition;
            Vector2 targetPosition = new Vector2(xZero + (23f * (mainCannon.GetNumberOfRemainingShells() - 4)), position.y) * Time.timeScale;

            if (!mainCannon.ReadyToFire())
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * (mainCannon.GetNumberOfRemainingShells() - 4)), position.y);
            }
        }
        else
        {
            Vector2 position = shellsSprites.anchoredPosition;
            
            Vector2 targetPosition = new Vector2(xZero + (23f * 2), position.y) * Time.timeScale;
            
            if (!mainCannon.ReadyToFire())
            {
                shellsSprites.anchoredPosition = Vector2.MoveTowards(shellsSprites.anchoredPosition, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                shellsSprites.anchoredPosition = new Vector2(xZero + (23f * 3), position.y);
                if (mainCannon.GetNumberOfRemainingShells() == 6)
                {
                    shellsSprites.anchoredPosition = new Vector2(xZero + (23f * 2), position.y);
                }
            }
        }
    }
}
