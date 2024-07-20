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

        rectTransformPos = rectTransform.position;
        binocularsPos = binoculars.position;
        cannonShootPos = cannonShoot.position;
        gunShootPos = gunShoot.position;
        aimSliderPos = aimSlider.position;

        rectTransformTPos = new Vector2(rectTransform.position.x - Screen.width, rectTransform.position.y);
        binocularsTPos = new Vector2(binoculars.position.x - Screen.width, binoculars.position.y);
        cannonShootTPos = new Vector2(cannonShoot.position.x - Screen.width, cannonShoot.position.y);
        gunShootTPos = new Vector2(gunShoot.position.x - Screen.width, gunShoot.position.y);
        aimSliderTPos = new Vector2(aimSlider.position.x - Screen.width, aimSlider.position.y);
    }

    void Update()
    {
        if(_isPaused) {
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, rectTransformTPos, speed * Time.unscaledDeltaTime);
            binoculars.position = Vector2.MoveTowards(binoculars.position, binocularsTPos, speed * Time.unscaledDeltaTime);
            cannonShoot.position = Vector2.MoveTowards(cannonShoot.position, cannonShootTPos, speed * Time.unscaledDeltaTime);
            gunShoot.position = Vector2.MoveTowards(gunShoot.position, gunShootTPos, speed * Time.unscaledDeltaTime);
            aimSlider.position = Vector2.MoveTowards(aimSlider.position, aimSliderTPos, speed * Time.unscaledDeltaTime);
        }

        if (!_isPaused)
        {
            rectTransform.position = Vector2.MoveTowards(rectTransform.position, rectTransformPos, speed * Time.unscaledDeltaTime);
            binoculars.position = Vector2.MoveTowards(binoculars.position, binocularsPos, speed * Time.unscaledDeltaTime);
            cannonShoot.position = Vector2.MoveTowards(cannonShoot.position, cannonShootPos, speed * Time.unscaledDeltaTime);
            gunShoot.position = Vector2.MoveTowards(gunShoot.position, gunShootPos, speed * Time.unscaledDeltaTime);
            aimSlider.position = Vector2.MoveTowards(aimSlider.position, aimSliderPos, speed * Time.unscaledDeltaTime);
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
