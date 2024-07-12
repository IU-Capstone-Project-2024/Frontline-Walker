using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public TestMessageReceiver torsoController;

    private float _currentMovementPenalty;
    private float _currentTorsoMovementPenalty;
    private float _currentFrictionPenalty;
    
    [Header("Debug")] 
    public bool showDebugLog = true;

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
        
        if (showDebugLog) Debug.Log("New movement penalty: " + _currentMovementPenalty);
        
        _currentTorsoMovementPenalty = 0;
        _currentTorsoMovementPenalty += lowerRightLeg.IsWorking() ? 0 : lowerLegTorsoMovementPenalty;
        _currentTorsoMovementPenalty += lowerLeftLeg.IsWorking() ? 0 : lowerLegTorsoMovementPenalty;
        
        if (showDebugLog) Debug.Log("New torso movement penalty: " + _currentTorsoMovementPenalty);
        
        _currentFrictionPenalty = 0;
        _currentFrictionPenalty += rightFoot.IsWorking() ? 0 : footFrictionPenalty;
        _currentFrictionPenalty += leftFoot.IsWorking() ? 0 : footFrictionPenalty;
        
        if (showDebugLog) Debug.Log("New friction penalty: " + _currentFrictionPenalty);
        
        ClampPenalties();

        controller.ReceiveMessage();
        torsoController.ReceiveMessage();
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
        if (showDebugLog) Debug.Log("Recalculating penalties");
        CalculatePenalties();
    }

    public override void ReceiveTerminationMessage()
    {
        if (showDebugLog) Debug.Log("Walker destroyed");
        SceneManager.LoadScene("Scenes/BarabachaTestScene");
    }
}
