using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class TestDifferentMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2;
    private float _timer;
    enum MovementType
    {
        Forward,
        Stop,
        Ellipse,
        Sinus,
        SinusSpeed,
        RandomForward
    }
    private MovementType _currentMovementType = MovementType.Forward;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchMovementType();
        }
           
        Move();
    }
    
    public void SwitchMovementType()
    {
        _timer = 0;
        transform.rotation = Quaternion.identity;
        _currentMovementType = (MovementType)(((int)_currentMovementType + 1) % System.Enum.GetValues(typeof(MovementType)).Length);
    }
       
    void Move()
    {
        switch (_currentMovementType)
        {
            case MovementType.Forward:
                MoveForward();
                break;
            case MovementType.Ellipse:
                MoveInEllipse();
                break;
            case MovementType.Sinus:
                MoveInSinus();
                break;
            case MovementType.Stop:
                MoveStop();
                break;
            case MovementType.SinusSpeed:
                MoveSinusSpeed();
                break;
            case MovementType.RandomForward:
                MoveRandomly();
                break;
        }
    }

    void MoveForward()
    {
        transform.Translate(Time.deltaTime * speed, 0, 0);
    }
    
    void MoveInEllipse()
    {
        _timer += Time.deltaTime;
        transform.Translate(2 * Mathf.Cos(_timer) * speed * Time.deltaTime,Mathf.Sin(_timer) * speed * Time.deltaTime,0);
    }
    
    void MoveInSinus()
    {
        _timer += Time.deltaTime;
        transform.Translate( Mathf.Abs(Mathf.Cos(_timer) * speed * Time.deltaTime),Mathf.Sin(_timer) * speed * Time.deltaTime / 2,0);
    }
    
    void MoveStop()
    {
        
    }

    void MoveSinusSpeed()
    {
        _timer += Time.deltaTime;
        transform.Translate(Mathf.Abs(Mathf.Cos(_timer) * speed * Time.deltaTime), 0, 0);
    }

    void MoveRandomly()
    {
        _timer += Time.deltaTime;
        if (_timer > 1)
        {
            _timer = 0;
            var newRot = Quaternion.Euler(new Vector3(0, 0, UnityEngine.Random.Range(-45, 45)));
            transform.rotation = newRot;
        }
        transform.Translate(speed * Time.deltaTime * transform.right);
    }

}
