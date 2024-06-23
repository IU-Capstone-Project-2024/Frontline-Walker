using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPoldPlane : MonoBehaviour
{
    public float rotationForwardSpeed = 200.0f; // Speed of the plane rotation during following the player 
    public float rotationAwaySpeed = 100.0f; // Speed of the plane rotation during flying away
    public float movementSpeed = 5.0f; // Speed of the plane
    public float startAngle = 240f; // The angle of the spawned plane from the right (181-269), flips horizontally when the plane is from the left 
    public float angleAway = 150f; // The maximum angle of the plane during the flying away to the left (91-179), flips horizontally when the plane is going to the right 
    public float startXpos = 10f; // X coordinate from the player for spawning point (flips horizontally for the left)
    public float startYpos = 5f; // Y coordinate from the player for spawning point
    public float goAwayAtY = -0f; // Y global coordinate that's automatically makes the plane to fly away (like an area collision with the player)
    public bool leftSide = false; // The initial side of the plane when it attacks for the first time
    public bool targetFollowLimit = false; // Limits the plane, so it can't rotate to the ground during following the player
    
    private GameObject target;
    private bool goAway = false;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goAway = true;
        }
    }

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        var position = target.transform.position;
        if (!leftSide)
        {
            transform.eulerAngles = new Vector3(0, 0, startAngle);
            transform.position = new Vector2(position.x + startXpos, position.y + startYpos);
        } else
        {
            transform.eulerAngles = new Vector3(0, 0, 540 - startAngle);
            transform.position = new Vector2(position.x - startXpos, position.y + startYpos);
        } 
    }

    private void Update()
    {
        if (!leftSide)
        {
            if (!goAway)
            {
                Vector3 direction = target.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if (targetRotation.z < transform.rotation.z || !targetFollowLimit)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationForwardSpeed * Time.deltaTime);
                }
            } else
            {
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angleAway));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationAwaySpeed * Time.deltaTime);
            }
        }

        else
        {
            if (!goAway)
            {
                Vector3 direction = target.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                if (targetRotation.z > transform.rotation.z || !targetFollowLimit)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationForwardSpeed * Time.deltaTime);
                }
            } else
            {
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 180-angleAway));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationAwaySpeed * Time.deltaTime);
            }
        }

        if (transform.position.y < goAwayAtY)
        {
            goAway = true;
        }

        if (transform.position.y > startYpos && goAway)
        {
            var position = target.transform.position;
            
            if (leftSide)
            {
                leftSide = false;
                goAway = false;
                transform.eulerAngles = new Vector3(0, 0, startAngle);
                transform.position = new Vector2(position.x + startXpos, position.y + startYpos);

            }
            else
            {
                leftSide = true;
                goAway = false;
                transform.eulerAngles = new Vector3(0, 0, 540 - startAngle);
                transform.position = new Vector2(position.x - startXpos, position.y + startYpos);
            }
        }

        transform.position += transform.right * movementSpeed * Time.deltaTime;
    }
}
