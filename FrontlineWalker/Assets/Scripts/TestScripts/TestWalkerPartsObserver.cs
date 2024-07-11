using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TestWalkerPartsObserver : TestMessageReceiver
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
    public float maxMovementPenalty;
    [Range(0,1)]
    public float upperLegMovementPenalty;
    [Range(0,1)]
    public float lowerLegMovementPenalty;
    [Range(0,1)]
    public float footMovementPenalty;

    [Header("Torso movement penalty")] 
    [Range(0,1)]
    public float maxTorsoMovementPenalty;
    [Range(0,1)] 
    public float lowerLegTorsoMovementPenalty;
    
    [Header("Friction penalty")] 
    [Range(0,1)]
    public float maxFrictionPenalty;
    [Range(0,1)] 
    public float footFrictionPenalty;

    [Header("Message receivers")] 
    public TestMessageReceiver controller;

    private float _currentMovementPenalty;
    private float _currentTorsoMovementPenalty;
    private float _currentFrictionPenalty;


    private void Start()
    {
        _currentMovementPenalty = 0;
        _currentTorsoMovementPenalty = 0;
        _currentFrictionPenalty = 0;

    }

    private void ClampPenalties()
    {
        if (_currentMovementPenalty < 0) _currentMovementPenalty = 0;
        if (_currentMovementPenalty > maxMovementPenalty) _currentMovementPenalty = maxMovementPenalty;
        
        if (_currentTorsoMovementPenalty < 0) _currentTorsoMovementPenalty = 0;
        if (_currentTorsoMovementPenalty > maxTorsoMovementPenalty) _currentTorsoMovementPenalty = maxTorsoMovementPenalty;
        
        if (_currentFrictionPenalty < 0) _currentFrictionPenalty = 0;
        if (_currentFrictionPenalty > maxFrictionPenalty) _currentFrictionPenalty = maxFrictionPenalty;
    }

    public void CalculatePenalties()
    {
        _currentMovementPenalty = 0;
        _currentMovementPenalty += upperRightLeg.IsWorking() ? 0 : upperLegMovementPenalty;
        _currentMovementPenalty += upperLeftLeg.IsWorking() ? 0 : upperLegMovementPenalty;
        _currentMovementPenalty += lowerRightLeg.IsWorking() ? 0 : lowerLegMovementPenalty;
        _currentMovementPenalty += lowerLeftLeg.IsWorking() ? 0 : lowerLegMovementPenalty;
        _currentMovementPenalty += rightFoot.IsWorking() ? 0 : footMovementPenalty;
        _currentMovementPenalty += leftFoot.IsWorking() ? 0 : footMovementPenalty;

        _currentTorsoMovementPenalty = 0;
        _currentTorsoMovementPenalty += lowerRightLeg.IsWorking() ? 0 : lowerLegTorsoMovementPenalty;
        _currentTorsoMovementPenalty += lowerLeftLeg.IsWorking() ? 0 : lowerLegTorsoMovementPenalty;
        
        _currentFrictionPenalty = 0;
        _currentFrictionPenalty += rightFoot.IsWorking() ? 0 : footFrictionPenalty;
        _currentFrictionPenalty += leftFoot.IsWorking() ? 0 : footFrictionPenalty;
        
        ClampPenalties();

        if (_currentMovementPenalty > 0)
        {
            controller.ReceiveMessage();
        }
    }

    public float GetCurrentMovementPenalty()
    {
        return _currentMovementPenalty;
    }
    
    public float GetCurrentTorsoMovementPenalty()
    {
        return _currentTorsoMovementPenalty;
    }
    
    public float GetCurrentFrictionPenalty()
    {
        return _currentFrictionPenalty;
    }
    
    public override void ReceiveMessage()
    {
        Debug.Log("Recalculating penalties");
        CalculatePenalties();
    }
}
