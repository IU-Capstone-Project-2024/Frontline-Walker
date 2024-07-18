using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pauseBackgroundMoving : MonoBehaviour
{
    public float speed = 10f;
    public bool isPaused = false;
    private RectTransform rectTransform;
    public RectTransform binoculars;
    public RectTransform cannonShoot;
    public RectTransform aimSlider;

    private Vector2 binocularsPos;
    private Vector2 cannonShootPos;
    private Vector2 aimSliderPos;

    private Vector2 binocularsTPos;
    private Vector2 cannonShootTPos;
    private Vector2 aimSliderTPos;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        binocularsPos = binoculars.anchoredPosition;
        cannonShootPos = cannonShoot.anchoredPosition;
        aimSliderPos = aimSlider.anchoredPosition;

        binocularsTPos = new Vector2(binoculars.anchoredPosition.x - Screen.width*2, binoculars.anchoredPosition.y) * Time.timeScale;
        cannonShootTPos = new Vector2(cannonShoot.anchoredPosition.x - Screen.width * 2, cannonShoot.anchoredPosition.y) * Time.timeScale;
        aimSliderTPos = new Vector2(aimSlider.anchoredPosition.x - Screen.width * 2, aimSlider.anchoredPosition.y) * Time.timeScale;
    }

    void Update()
    {
        if(isPaused) {
            Vector2 targetPosition = new Vector2(0, 0) * Time.timeScale;

            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, speed * Time.unscaledDeltaTime);
            binoculars.anchoredPosition = Vector2.MoveTowards(binoculars.anchoredPosition, binocularsTPos, speed * Time.unscaledDeltaTime);
            cannonShoot.anchoredPosition = Vector2.MoveTowards(cannonShoot.anchoredPosition, cannonShootTPos, speed * Time.unscaledDeltaTime);
            aimSlider.anchoredPosition = Vector2.MoveTowards(aimSlider.anchoredPosition, aimSliderTPos, speed * Time.unscaledDeltaTime);
        }
    }

    // Update is called once per frame
    public void PauseActivate()
    {
        isPaused = true;
        Time.timeScale = 0;
    }
}
