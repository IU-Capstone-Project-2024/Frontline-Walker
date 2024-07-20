using System;
using UnityEngine;

public class TestInoyPlane : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 200.0f;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float escapeAngle = 30.0f;
    [SerializeField] private float maxAwayDistance = 10;
    private GameObject _target;
    private bool _flyAway;
    private Rigidbody2D _rigidbody;
    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (_target == null)
        {
            throw new ArgumentException();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        _flyAway = true;
        gameObject.GetComponent<BombHolder>().Release();
    }
    
    private void Update()
    {
        _rigidbody.AddForce(-transform.right * movementSpeed);
        CheckToChangeDirection();
        if (!_flyAway)
        {
            FlyToTarget();
        }
        else
        {
            FlyFromTarget();
        }
    }

    private void CheckToChangeDirection()
    {
        if (transform.rotation.eulerAngles.y == 0)
        {
            if (_target.transform.position.x - transform.position.x  > maxAwayDistance)
            {
                _flyAway = false;
                var rotation = transform.rotation.eulerAngles;
                rotation.y = 180;
                transform.rotation = Quaternion.Euler(rotation);
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            if (transform.position.x - _target.transform.position.x  > maxAwayDistance)
            {
                _flyAway = false;
                var rotation = transform.rotation.eulerAngles;
                rotation.y = 0;
                transform.rotation = Quaternion.Euler(rotation);
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }

    private void FlyToTarget()
    {
        
        Vector2 direction = _target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180;
        Vector3 targetRotation = new Vector3(0, transform.rotation.eulerAngles.y, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),
            rotationSpeed * Time.deltaTime);
    }

    private void FlyFromTarget()
    {
        Vector3 targetRotation = new Vector3(0, transform.rotation.eulerAngles.y, -escapeAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation),
            rotationSpeed * Time.deltaTime);
    }
}
