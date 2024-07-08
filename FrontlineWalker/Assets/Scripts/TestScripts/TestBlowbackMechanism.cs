using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class TestBlowbackMechanism : MonoBehaviour
{

    public float blowbackDistance = 0.2f;
    public float blowbackSpeed = 1;
    public float returnSpeed = 0.5f;

    private Vector2 _initialPosition;
    private Vector2 _currentPosition;
    private Vector2 _direction;
    private bool _blowingBack;
    
    void Start()
    {
        _initialPosition = new Vector2(transform.localPosition.x, transform.localPosition.y);
        _currentPosition = new Vector2(0,0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(_currentPosition.x + " " + _currentPosition.y + " " + _direction.x + " " + _direction.y);
        _direction = new Vector2(Mathf.Cos(transform.rotation.z), Mathf.Sin(transform.rotation.z));
        if (_blowingBack)
        {
            if (_currentPosition.magnitude < blowbackDistance && _currentPosition.x <= 0)
            {
                _currentPosition -= _direction * blowbackSpeed * Time.deltaTime;
                Clamp();
                setCurrentX();
            }
        }
        else
        {
            if (_currentPosition.magnitude > 0)
            {
                _currentPosition += _direction * returnSpeed * Time.deltaTime;
                Clamp();
                setCurrentX();
            }
        }
    }

    private void Clamp()
    {
        if (_currentPosition.magnitude > blowbackDistance && _currentPosition.x < 0)
        {
            _currentPosition = _direction * -blowbackDistance;
            _blowingBack = false;
        }

        if (_currentPosition.x > 0)
        {
            _currentPosition = new Vector2(0,0);
        }
    }

    private void setCurrentX()
    {
        transform.localPosition = new Vector3(_initialPosition.x + _currentPosition.x, _initialPosition.y + _currentPosition.y, transform.localPosition.z);
    }
    
    public void Blowback()
    {
        _blowingBack = true;
    }

    public bool isBlowingBack()
    {
        return _blowingBack;
    }
}
