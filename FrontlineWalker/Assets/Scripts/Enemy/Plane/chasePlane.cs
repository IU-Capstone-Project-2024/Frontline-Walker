using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlane : MonoBehaviour
{
    public float rotationForwardSpeed = 200.0f; // Speed of the plane rotation during following the player 
    public float movementSpeed = 5.0f; // Speed of the plane
    public float startAngle = 240f; // The angle of the spawned plane from the right (181-269), flips horizontally when the plane is from the left 
    public float startXpos = 10f; // X coordinate from the player for spawning point (flips horizontally for the left)
    public float startYpos = 5f; // Y coordinate from the player for spawning point
    public float goAwayAtY = -0f; // Y global coordinate that's automatically makes the plane to fly away (like an area collision with the player)
    public bool leftSide = false; // The initial side of the plane when it attacks for the first time
    public bool targetFollowLimit = false; // Limits the plane, so it can't rotate to the ground during following the player
    public bool ableToEscape = true; // Allows plane to leave

    public bool goAway = false; // Do not touch it
    public GameObject target;  // Do not touch it


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (ableToEscape && !goAway && collision.gameObject.CompareTag("Player"))
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
        if (!goAway)
        {
            Vector3 direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            if ((!leftSide && (targetRotation.z < transform.rotation.z)) || (leftSide && (targetRotation.z > transform.rotation.z)) || !targetFollowLimit)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationForwardSpeed * Time.deltaTime);
            }

            if (transform.position.y < goAwayAtY && ableToEscape)
            {
                goAway = true;
            }

            transform.position += transform.right * movementSpeed * Time.deltaTime;
        }
    }
}
