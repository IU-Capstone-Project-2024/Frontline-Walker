using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTransformCollections : MonoBehaviour
{

    public List<RectTransform> transforms;

    public bool Contains(Vector2 tapPosition)
    {
        foreach (var _rectTransform in transforms)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, tapPosition, null, out Vector2 localPoint);
            if (_rectTransform.rect.Contains(localPoint))
            {
                return true;
            }
        }
        return false;
    }
}
