using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public int currentValue;
	public int angleMax = 30;
    public int angleMin = 30;
	public float pushPos = -150f;
	public bool isX;
	public bool zeroToValueMode;
	public float initialAngle;
    public GameObject slideSprite;
	private bool isPointerDown=false;
	private float trueAngleMax;
	private float trueAngleMin;

    public void Start()
    {

        slideSprite.transform.localRotation = Quaternion.Euler(0, 0, initialAngle);
		if (isX)
		{
            slideSprite.transform.localPosition = new Vector2(pushPos, 0);
        }
		else
		{
            slideSprite.transform.localPosition = new Vector2(0, pushPos);
        }

		trueAngleMax = angleMax / 360f;
        trueAngleMin = 1f - angleMin / 360f;


    }

    public void OnPointerEnter( PointerEventData eventData )
	{
		StartCoroutine( "TrackPointer" );            
	}
	
	public void OnPointerExit( PointerEventData eventData )
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		isPointerDown= true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		isPointerDown= false;
	}

	IEnumerator TrackPointer()
	{
		var ray = GetComponentInParent<GraphicRaycaster>();
		var input = FindObjectOfType<StandaloneInputModule>();
		
		if( ray != null && input != null )
		{
			while( Application.isPlaying )
			{                    
				if (isPointerDown)
				{

					Vector2 localPos;
					RectTransformUtility.ScreenPointToLocalPointInRectangle( transform as RectTransform, Input.mousePosition, ray.eventCamera, out localPos );

                    float angle = (Mathf.Atan2(localPos.y, localPos.x) * 180f / Mathf.PI + 180f) / 360f;

                    if (angle > trueAngleMax && angle <= trueAngleMax + 0.05f)
                    {
                        angle = trueAngleMax;
                    }
                    else if (angle < trueAngleMin && angle >= trueAngleMin - 0.05f)
                    {
                        angle = trueAngleMin;
                    }

					if (angle <= trueAngleMax || angle >= trueAngleMin)
					{
						if (!zeroToValueMode)
						{
                            if (angle <= 0.5f)
                            {
                                currentValue = Mathf.RoundToInt(angle * 360);
                            }
                            else
                            {
                                currentValue = Mathf.RoundToInt(angle * 360) - 360;
                            }
                        }
						else
						{
                            if (angle <= 0.5f)
                            {
                                currentValue = 90 + Mathf.RoundToInt(angle * 360);
                            }
                            else
                            {
                                currentValue = 90 - Mathf.Abs(Mathf.RoundToInt(angle * 360) - 360);
                            }
                        }


                        slideSprite.transform.localRotation = Quaternion.Euler(0, 0, angle * 360);

                        float angleInRadians = (angle * 360 + 180) * Mathf.Deg2Rad;

                        float x = Mathf.Abs(pushPos) * Mathf.Cos(angleInRadians);
                        float y = Mathf.Abs(pushPos) * Mathf.Sin(angleInRadians);

                        slideSprite.transform.localPosition = new Vector2(x, y);
                    }


                }

				yield return 0;
			}        
		}    
	}
}
