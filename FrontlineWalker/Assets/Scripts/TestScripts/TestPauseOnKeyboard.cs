using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPauseOnKeyboard : MonoBehaviour
{

    public KeyCode pauseButton;

    private bool _timePaused;
    private float _initialTimeScale;
    
    // Start is called before the first frame update
    void Start()
    {
        _timePaused = false;
        _initialTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseButton))
        {
            if (_timePaused)
            {
                Debug.Log("Resume");
                Time.timeScale = _initialTimeScale;
                _timePaused = false;
            }
            else
            {
                Debug.Log("Pause");
                Time.timeScale = 0;
                _timePaused = true;
            }
        }
    }
}
