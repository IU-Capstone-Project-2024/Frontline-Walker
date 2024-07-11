using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWalkerPartsObserver : MonoBehaviour
{
    [Header("Parts")]
    public TestCharacterPart mainCannon;
    public TestCharacterPart torso;
    public TestCharacterPart upperRightLeg;
    public TestCharacterPart upperLeftLeg;
    public TestCharacterPart lowerRightLeg;
    public TestCharacterPart lowerLeftLeg;
    public TestCharacterPart rightFoot;
    public TestCharacterPart leftFoot;

    [Header("Movement penalty")]
    [Range(0,1)]
    public float lowerLegPenalty;
    [Range(0,1)]
    public float footPenalty;

    private float _currentPenalty;

    private void ClampPenalty()
    {
        if (_currentPenalty < 0) _currentPenalty = 0;
        if (_currentPenalty > 1) _currentPenalty = 1;
    }

    public float GetCurrentPenalty()
    {
        _currentPenalty = 0;
        _currentPenalty += lowerRightLeg.IsWorking() ? 0 : lowerLegPenalty;
        _currentPenalty += lowerLeftLeg.IsWorking() ? 0 : lowerLegPenalty;
        _currentPenalty += rightFoot.IsWorking() ? 0 : footPenalty;
        _currentPenalty += leftFoot.IsWorking() ? 0 : footPenalty;
        ClampPenalty();
        //Debug.Log(_currentPenalty);
        return _currentPenalty;
    }
}
