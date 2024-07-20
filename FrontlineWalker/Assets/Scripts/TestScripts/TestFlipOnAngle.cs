using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFlipOnAngle : MonoBehaviour
{
    [Range(0, 180)]
    public float flipAngle;
    [Range(180, 360)]
    public float unflipAngle;
    public SpriteRenderer spriteRenderer;
    public bool initialFlipY = false;
    
    
    void FixedUpdate()
    {
        if (transform.rotation.eulerAngles.z > flipAngle && transform.rotation.eulerAngles.z < unflipAngle)
        {
            spriteRenderer.flipY = !initialFlipY;
        }
        else
        {
            spriteRenderer.flipY = initialFlipY;
        }
    }
}
