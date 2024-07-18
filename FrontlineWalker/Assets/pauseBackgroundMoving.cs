using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class pauseBackgroundMoving : MonoBehaviour
{
    public float speed = 10f;
    public bool _isPaused = false;
    private float _initialTimeScale;

    private RectTransform rectTransform;
    public RectTransform binoculars;
    public RectTransform cannonShoot;
    public RectTransform gunShoot;
    public RectTransform aimSlider;

    private Vector2 rectTransformPos;
    private Vector2 binocularsPos;
    private Vector2 cannonShootPos;
    private Vector2 gunShootPos;
    private Vector2 aimSliderPos;

    private Vector2 rectTransformTPos;
    private Vector2 binocularsTPos;
    private Vector2 cannonShootTPos;
    private Vector2 gunShootTPos;
    private Vector2 aimSliderTPos;


    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        _initialTimeScale = Time.timeScale;

        rectTransformPos = rectTransform.anchoredPosition;
        binocularsPos = binoculars.anchoredPosition;
        cannonShootPos = cannonShoot.anchoredPosition;
        gunShootPos = gunShoot.anchoredPosition;
        aimSliderPos = aimSlider.anchoredPosition;

        rectTransformTPos = new Vector2(0, 0);
        binocularsTPos = new Vector2(binoculars.anchoredPosition.x - Screen.width*2, binoculars.anchoredPosition.y) * Time.timeScale;
        cannonShootTPos = new Vector2(cannonShoot.anchoredPosition.x - Screen.width * 2, cannonShoot.anchoredPosition.y) * Time.timeScale;
        gunShootTPos = new Vector2(gunShoot.anchoredPosition.x - Screen.width * 2, gunShoot.anchoredPosition.y) * Time.timeScale;
        aimSliderTPos = new Vector2(aimSlider.anchoredPosition.x - Screen.width * 2, aimSlider.anchoredPosition.y) * Time.timeScale;
    }

    void Update()
    {
        if(_isPaused) {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, rectTransformTPos, speed * Time.unscaledDeltaTime);
            binoculars.anchoredPosition = Vector2.MoveTowards(binoculars.anchoredPosition, binocularsTPos, speed * Time.unscaledDeltaTime);
            cannonShoot.anchoredPosition = Vector2.MoveTowards(cannonShoot.anchoredPosition, cannonShootTPos, speed * Time.unscaledDeltaTime);
            gunShoot.anchoredPosition = Vector2.MoveTowards(gunShoot.anchoredPosition, gunShootTPos, speed * Time.unscaledDeltaTime);
            aimSlider.anchoredPosition = Vector2.MoveTowards(aimSlider.anchoredPosition, aimSliderTPos, speed * Time.unscaledDeltaTime);
        }

        if (!_isPaused)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, rectTransformPos, speed * Time.unscaledDeltaTime);
            binoculars.anchoredPosition = Vector2.MoveTowards(binoculars.anchoredPosition, binocularsPos, speed * Time.unscaledDeltaTime);
            cannonShoot.anchoredPosition = Vector2.MoveTowards(cannonShoot.anchoredPosition, cannonShootPos, speed * Time.unscaledDeltaTime);
            gunShoot.anchoredPosition = Vector2.MoveTowards(gunShoot.anchoredPosition, gunShootPos, speed * Time.unscaledDeltaTime);
            aimSlider.anchoredPosition = Vector2.MoveTowards(aimSlider.anchoredPosition, aimSliderPos, speed * Time.unscaledDeltaTime);
        }
    }

    // Update is called once per frame
    public void PauseActivate()
    {
        _isPaused = true;
        Time.timeScale = 0;
    }

    public void PauseDeactivate()
    {
        _isPaused = false;
        Time.timeScale = _initialTimeScale;
    }
}
