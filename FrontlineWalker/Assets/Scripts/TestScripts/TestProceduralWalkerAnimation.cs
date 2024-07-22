using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestProceduralWalkerAnimation : MonoBehaviour
{
    [Header("limbs")]
    public GameObject[] foots;
    public Transform[] legTargets;
    public float foots_rotation_speed = 5f; 
    public float initialStepSize = 1.3f;
    private float stepSize; 
    public int step_smoothness = 20;
    public float stepHeight = 0.5f;
    public bool bodyOrientation = true;
    [Header("Torso")] 
    public TestTorsoController torsoController;
    public float shakeHeight = 0.01f;
    public bool shakeTorso = true;
    public float torsoBalanceDelta = 0.3f;
    public bool considerTorsoBalance = true;

    [Header("Calculations")]
    public float raycastRange = 1.5f;
    public LayerMask layerMask;
    
    private Vector2[] _defaultLegPositions;
    private Vector2[] _lastLegPositions;
    private Vector2[] _lastLocalLegTargetPositions;
    private Vector2[] _footsNormals;
    private bool[] _movingLegs;
    private Vector2 _lastBodyUp;
    private bool _stepCooled;
    private int _nbLegs;
    
    private Vector2 _velocity;
    private Vector2 _lastVelocity;
    private Vector2 _lastBodyPos;

    private float _velocityMultiplier = 14f;

    private bool _animate;

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
        _animate = true;
        
        if (foots.Length != legTargets.Length)
        {
            Debug.Log("Number of foots doesn't match number of legs!");
            foots = Array.Empty<GameObject>();
        }

        _movingLegs = new bool[foots.Length];
        for (int i = 0; i < _movingLegs.Length; i++)
        {
            _movingLegs[i] = false;
        }

        _footsNormals = new Vector2[foots.Length];
        for (int i = 0; i < _footsNormals.Length; i++) 
        {
            _footsNormals[i] = new Vector2(0, 1);
        }
        
        _lastBodyUp = transform.up;

        _nbLegs = legTargets.Length;
        _defaultLegPositions = new Vector2[_nbLegs];
        _lastLegPositions = new Vector2[_nbLegs];
        _lastLocalLegTargetPositions = new Vector2[_nbLegs];
        _stepCooled = true;
        for (int i = 0; i < _nbLegs; ++i)
        {
            _defaultLegPositions[i] = legTargets[i].localPosition;
            _lastLegPositions[i] = legTargets[i].position;
        }
        _lastBodyPos = transform.position;

        stepSize = initialStepSize;
    }

    IEnumerator PerformStep(int index, Vector3 targetPoint)
    {
        Vector3 startPos = _lastLegPositions[index];
        
        for(int i = 1; i <= step_smoothness; ++i)
        {
            //shake torso
            if (shakeTorso)
            {
                torsoController.AddToCurrentY((Mathf.Sin(i / (float)(step_smoothness + 1f) * 2 * Mathf.PI)) * shakeHeight);
            }
            
            //move legs
            legTargets[index].position = Vector3.Lerp(startPos, targetPoint, i / (float)(step_smoothness + 1f));
            legTargets[index].position += transform.up * Mathf.Sin(i / (float)(step_smoothness + 1f) * Mathf.PI) * stepHeight;
            yield return new WaitForFixedUpdate();
        }

        _movingLegs[index] = false;
        legTargets[index].position = targetPoint;
        _lastLegPositions[index] = legTargets[index].position;
        _stepCooled = true;
    }


    void FixedUpdate()
    {
        stepSize = initialStepSize * (1.7f - torsoController.GetCurrentYRatio());
        
        if (_animate)
        {
            _velocity = (Vector2) transform.position - _lastBodyPos;
            _velocity = (_velocity + step_smoothness * _lastVelocity) / (step_smoothness + 1f);
            
            if (_velocity.magnitude < 0.000025f)
                _velocity = _lastVelocity;
            else
                _lastVelocity = _velocity;
                    
                    
            Vector2[] desiredPositions = new Vector2[_nbLegs];
            int indexToMove = -1;
            float maxDistance = stepSize;
            for (int i = 0; i < _nbLegs; ++i)
            {
                desiredPositions[i] = transform.TransformPoint(new Vector2(_defaultLegPositions[i].x, _defaultLegPositions[i].y - torsoController.getCurrentY()));
                float distance = Vector3.ProjectOnPlane(desiredPositions[i] + _velocity * _velocityMultiplier - _lastLegPositions[i], transform.up).magnitude;
                if (distance > maxDistance)
                {
                    maxDistance = distance;
                    indexToMove = i;
                }
            }

            if (considerTorsoBalance)
            {
                var max = float.MinValue;
                var min = float.MaxValue;
                var minIndex = -1;
                var maxIndex = -1;
                for (int i = 0; i < _nbLegs; ++i)
                {
                    var positionX = foots[i].transform.position.x; 
                    if (positionX < min)
                    {
                        min = positionX;
                        minIndex = i;
                    }
                
                    if (positionX > max)
                    {
                        max = positionX;
                        maxIndex = i;
                    }
                }
                            
                if (transform.position.x < min - torsoBalanceDelta || transform.position.x > max + torsoBalanceDelta)
                {
                    if (_velocity.x > 0)
                    {
                        indexToMove = minIndex;
                    }
                    else
                    {
                        indexToMove = maxIndex;
                    }
                }
            }
            
            
            for (int i = 0; i < _nbLegs; ++i)
                if (i != indexToMove)
                    legTargets[i].position = _lastLegPositions[i];
                    
            if (indexToMove != -1 && _stepCooled)
            {
                Vector2 targetPoint = desiredPositions[indexToMove] + Mathf.Clamp(_velocity.magnitude * _velocityMultiplier, 0.0f, stepSize) * (desiredPositions[indexToMove] - 
                    (Vector2)legTargets[indexToMove].position) + _velocity * _velocityMultiplier;
            
                Vector2[] positionAndNormalFwd = MatchToSurfaceFromAbove(targetPoint + _velocity * _velocityMultiplier, raycastRange, 
                    ((Vector2)transform.parent.up - _velocity * 10).normalized);
            
                Vector2[] positionAndNormalBwd = MatchToSurfaceFromAbove(targetPoint + _velocity * _velocityMultiplier, raycastRange*(1f + _velocity.magnitude), 
                    ((Vector2)transform.parent.up + _velocity * 10).normalized);
            
                _stepCooled = false;
                _movingLegs[indexToMove] = true;
                        
                if (positionAndNormalFwd[1] == Vector2.zero)
                {
                    StartCoroutine(PerformStep(indexToMove, positionAndNormalBwd[0]));
                    _footsNormals[indexToMove] = positionAndNormalBwd[1];
                }
                else
                {
                    StartCoroutine(PerformStep(indexToMove, positionAndNormalFwd[0]));
                    _footsNormals[indexToMove] = positionAndNormalFwd[1];
                }
            }
                    
            //orienting body
            _lastBodyPos = transform.position;
            if (_nbLegs > 1 && bodyOrientation)
            {
                Vector2 v1 = (legTargets[1].position - legTargets[0].position).normalized;
                Vector3 v2 = Vector3.back;
                Vector3 normal = Vector3.Cross(v1, v2).normalized;
                Vector3 up = Vector3.Lerp(_lastBodyUp, normal, 1f / (float)(step_smoothness + 1));
                transform.up = up;
                transform.rotation = Quaternion.LookRotation(transform.parent.forward, up);
                _lastBodyUp = transform.up;
            }
        }
        else
        {
            _lastBodyPos = transform.position;
            
            for (int i = 0; i < _nbLegs; i++)
            {
                var centr = new Vector2(transform.position.x, transform.position.y);
                var offset = new Vector2(_lastLocalLegTargetPositions[i].x, 0);
                var start = centr + offset;
                var dir = Vector2.down;
                var hit = Physics2D.Raycast(start, dir, 10, layerMask);
                var point = hit.point;
                legTargets[i].position = point;
                _lastLegPositions[i] = point;

                _footsNormals[i].x = hit.normal.x;
                _footsNormals[i].y = hit.normal.y;
            }
        }

        //updating leg targets if floor moved
        for (int i = 0; i < _movingLegs.Length; i++)
        {
            if (!_movingLegs[i])
            {
                RaycastHit2D hit;
                Vector2 original = new Vector2(legTargets[i].position.x, legTargets[i].position.y);
                hit = Physics2D.Raycast(original + Vector2.up * 0.3f, Vector2.down, 2f, layerMask);
                legTargets[i].position = hit.point;
            }
        }
        
        //rotating foot
        for (int i = 0; i < foots.Length; i++)
        {
            var _forward = new Vector2(_footsNormals[i].y, -_footsNormals[i].x);
            var _new_z = -Vector2.SignedAngle(_forward, new Vector2(1,0));
            var target_rotation = Quaternion.Euler(0,0, _new_z);
            foots[i].transform.rotation = Quaternion.Lerp(foots[i].transform.rotation, target_rotation, foots_rotation_speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < _nbLegs; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(legTargets[i].position, 0.05f);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.TransformPoint(new Vector2(_defaultLegPositions[i].x, _defaultLegPositions[i].y - torsoController.getCurrentY())), stepSize);
        }
    }

    public void ResumeAnimation()
    {
        _animate = true;
    }

    public void StopAnimation()
    {
        _animate = false;
        for (int i = 0; i < _nbLegs; i++)
        {
            _lastLocalLegTargetPositions[i] = legTargets[i].localPosition;
        }
    }
}
