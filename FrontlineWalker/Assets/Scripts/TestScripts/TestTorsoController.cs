using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTorsoController : MonoBehaviour
{
    public float maxY = 0.5f;
    public float minY = -0.5f;
    public float speed = 0.1f;

    private float _currentY;
    private float _initialY;
    
    // Start is called before the first frame update
    void Start()
    {
        _currentY = 0;
        _initialY = transform.localPosition.y;
    }

    void Clamp()
    {
        if (_currentY > maxY)
        {
            _currentY = maxY;
        }
        if (_currentY < minY)
        {
            _currentY = minY;
        }
    }

    void setTorsoY()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, _initialY + _currentY, transform.localPosition.z);
    }

    public void Up()
    {
        _currentY += speed * Time.deltaTime;
        Clamp();
        setTorsoY();
    }

    public void Down()
    {
        _currentY -= speed * Time.deltaTime;
        Clamp();
        setTorsoY();
    }
}
