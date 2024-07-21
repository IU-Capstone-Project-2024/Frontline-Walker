using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;

public class EventSystemEnabler : MonoBehaviour
{
    private StandaloneInputModule _standaloneInputModule;

    void Start()
    {
        _standaloneInputModule = GetComponent<StandaloneInputModule>();
    }

    private void OnEnable()
    {
        StartCoroutine(Co_ActivateInputComponent());
    }

    private IEnumerator Co_ActivateInputComponent()
    {
        yield return new WaitForEndOfFrame();
        _standaloneInputModule.enabled = false;
        yield return new WaitForSeconds(0.2f);
        _standaloneInputModule.enabled = true;
    }
}