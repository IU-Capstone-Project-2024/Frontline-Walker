using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escapePlane : MonoBehaviour
{
    public float rotationAwaySpeed = 100.0f; // Speed of the plane rotation during flying away
    public float angleAway = 150f; // The maximum angle of the plane during the flying away to the left (91-179), flips horizontally when the plane is going to the right 
    public chasePlane chase; // chasePlane script

    private void Update()
    {
        if (chase.goAway)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, 0));

            if (!chase.leftSide)
            {
                targetRotation = Quaternion.Euler(new Vector3(0, 0, angleAway));
            }
            else
            {
                targetRotation = Quaternion.Euler(new Vector3(0, 0, 180 - angleAway));
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationAwaySpeed * Time.deltaTime);

            if (transform.position.y > chase.startYpos)
            {
                var position = chase.target.transform.position;
                chase.goAway = false;

                if (chase.leftSide)
                {
                    chase.leftSide = false;
                    transform.eulerAngles = new Vector3(0, 0, chase.startAngle);
                    transform.position = new Vector2(position.x + chase.startXpos, position.y + chase.startYpos);

                }
                else
                {
                    chase.leftSide = true;
                    transform.eulerAngles = new Vector3(0, 0, 540 - chase.startAngle);
                    transform.position = new Vector2(position.x - chase.startXpos, position.y + chase.startYpos);
                }
            }

            transform.position += transform.right * chase.movementSpeed * Time.deltaTime;
        }
    }
}
