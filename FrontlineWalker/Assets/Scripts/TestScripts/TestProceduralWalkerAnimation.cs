using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProceduralWalkerAnimation : MonoBehaviour
{
    public GameObject[] foots;
    public Transform[] legTargets;
    public float foots_rotation_speed = 5f; 
    public float stepSize = 1.3f;
    public int step_smoothness = 20;
    public float stepHeight = 0.5f;
    public bool bodyOrientation = true;

    public float raycastRange = 1.5f;
    public LayerMask layerMask;
    
    private Vector2[] defaultLegPositions;
    private Vector2[] lastLegPositions;
    private Vector2[] _foots_normals;
    private Vector2 lastBodyUp;
    private bool stepCooled;
    private int nbLegs;
    
    private Vector2 velocity;
    private Vector2 lastVelocity;
    private Vector2 lastBodyPos;

    private float velocityMultiplier = 7f;

    Vector2[] MatchToSurfaceFromAbove(Vector2 point, float halfRange, Vector2 up)
    {
        Vector2[] res = new Vector2[2];
        res[1] = Vector3.zero;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(point + halfRange * up / 2f, -up, 2f * halfRange, layerMask);
        Debug.DrawRay(point + halfRange * up / 2f, - up * 2f * halfRange, Color.red, step_smoothness * Time.deltaTime);
        if (hit.collider)
        {
            res[0] = hit.point;
            res[1] = hit.normal;
        }
        else
        {
            res[0] = point;
        }
        return res;
    }
    
    void Start()
    {
        if (foots.Length != legTargets.Length)
        {
            Debug.Log("Number of foots doesn't match number of legs!");
            foots = Array.Empty<GameObject>();
        }

        _foots_normals = new Vector2[foots.Length];
        for (int i = 0; i < _foots_normals.Length; i++) 
        {
            _foots_normals[i] = new Vector2(0, 1);
        }
        
        lastBodyUp = transform.up;

        nbLegs = legTargets.Length;
        defaultLegPositions = new Vector2[nbLegs];
        lastLegPositions = new Vector2[nbLegs];
        stepCooled = true;
        for (int i = 0; i < nbLegs; ++i)
        {
            defaultLegPositions[i] = legTargets[i].localPosition;
            lastLegPositions[i] = legTargets[i].position;
        }
        lastBodyPos = transform.position;
    }

    IEnumerator PerformStep(int index, Vector3 targetPoint)
    {
        Vector3 startPos = lastLegPositions[index];
        for(int i = 1; i <= step_smoothness; ++i)
        {
            legTargets[index].position = Vector3.Lerp(startPos, targetPoint, i / (float)(step_smoothness + 1f));
            legTargets[index].position += transform.up * Mathf.Sin(i / (float)(step_smoothness + 1f) * Mathf.PI) * stepHeight;
            yield return new WaitForFixedUpdate();
        }
        legTargets[index].position = targetPoint;
        lastLegPositions[index] = legTargets[index].position;
        stepCooled = true;
    }


    void FixedUpdate()
    {
        velocity = (Vector2)transform.position - lastBodyPos;
        velocity = (velocity + step_smoothness * lastVelocity) / (step_smoothness + 1f);

        if (velocity.magnitude < 0.000025f)
            velocity = lastVelocity;
        else
            lastVelocity = velocity;
        
        
        Vector2[] desiredPositions = new Vector2[nbLegs];
        int indexToMove = -1;
        float maxDistance = stepSize;
        for (int i = 0; i < nbLegs; ++i)
        {
            desiredPositions[i] = transform.TransformPoint(defaultLegPositions[i]);

            float distance = Vector3.ProjectOnPlane(desiredPositions[i] + velocity * velocityMultiplier - lastLegPositions[i], transform.up).magnitude;
            if (distance > maxDistance)
            {
                maxDistance = distance;
                indexToMove = i;
            }
        }
        for (int i = 0; i < nbLegs; ++i)
            if (i != indexToMove)
                legTargets[i].position = lastLegPositions[i];
        
        if (indexToMove != -1 && stepCooled)
        {
            Vector2 targetPoint = desiredPositions[indexToMove] + Mathf.Clamp(velocity.magnitude * velocityMultiplier, 0.0f, 1.5f) * (desiredPositions[indexToMove] - 
                (Vector2)legTargets[indexToMove].position) + velocity * velocityMultiplier;

            Vector2[] positionAndNormalFwd = MatchToSurfaceFromAbove(targetPoint + velocity * velocityMultiplier, raycastRange, 
                ((Vector2)transform.parent.up - velocity * 10).normalized);

            Vector2[] positionAndNormalBwd = MatchToSurfaceFromAbove(targetPoint + velocity * velocityMultiplier, raycastRange*(1f + velocity.magnitude), 
                ((Vector2)transform.parent.up + velocity * 10).normalized);

            stepCooled = false;
            
            if (positionAndNormalFwd[1] == Vector2.zero)
            {
                StartCoroutine(PerformStep(indexToMove, positionAndNormalBwd[0]));
                _foots_normals[indexToMove] = positionAndNormalBwd[1];
            }
            else
            {
                StartCoroutine(PerformStep(indexToMove, positionAndNormalFwd[0]));
                _foots_normals[indexToMove] = positionAndNormalFwd[1];
            }
        }
        
        //rotating foot
        for (int i = 0; i < foots.Length; i++)
        {
            
            var _forward = new Vector2(_foots_normals[i].y, -_foots_normals[i].x);
            var _new_z = -Vector2.SignedAngle(_forward, new Vector2(1,0));
            var target_rotation = Quaternion.Euler(0,0, _new_z);
            foots[i].transform.rotation = Quaternion.Lerp(foots[i].transform.rotation, target_rotation, foots_rotation_speed * Time.deltaTime);
        }
        
        //
        lastBodyPos = transform.position;
        if (nbLegs > 1 && bodyOrientation)
        {
            Vector2 v1 = (legTargets[1].position - legTargets[0].position).normalized;
            
            Vector3 v2 = Vector3.back;
            Vector3 normal = Vector3.Cross(v1, v2).normalized;
            Vector3 up = Vector3.Lerp(lastBodyUp, normal, 1f / (float)(step_smoothness + 1));
            transform.up = up;
            transform.rotation = Quaternion.LookRotation(transform.parent.forward, up);
            lastBodyUp = transform.up;
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < nbLegs; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(legTargets[i].position, 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.TransformPoint(defaultLegPositions[i]), stepSize);
        }
    }
}
