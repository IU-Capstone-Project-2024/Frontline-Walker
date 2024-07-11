using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RadialSlider: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
	public int currentValue;
	public int angleMax = 30;
    public int angleMin = 30;
    public GameObject slideSprite;
	private bool isPointerDown=false;

    public void Start()
    {
        GetComponent<Image>().fillAmount = 0.25f;

        slideSprite.transform.localRotation = Quaternion.Euler(0, 0, 90);
        slideSprite.transform.localPosition = new Vector2(0, -50);

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

					if (localPos.y <= 0f)
					{
						float angle = (Mathf.Atan2(localPos.y, localPos.x) * 180f / Mathf.PI + 180f) / 360f;

						GetComponent<Image>().fillAmount = angle;

						if (angle < 0.25f)
                        {
                            currentValue = Mathf.RoundToInt((angle - 0.25f) * angleMin * 4);
                        }
						else
						{
                            currentValue = Mathf.RoundToInt((angle - 0.25f) * angleMax * 4);
                        }


						slideSprite.transform.localRotation = Quaternion.Euler(0, 0, angle * 360);

                        float angleInRadians = (angle * 360 + 180) * Mathf.Deg2Rad;

                        float x = 50 * Mathf.Cos(angleInRadians);
                        float y = 50 * Mathf.Sin(angleInRadians);

                        slideSprite.transform.localPosition = new Vector2(x, y);
                    }

                }

				yield return 0;
			}        
		}    
	}
}
