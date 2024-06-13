using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelAim : MonoBehaviour
{
    private Transform target;
    public float rotationSpeed = 200.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = collision.transform;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }
}
