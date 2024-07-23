using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNTiltCalculator : MonoBehaviour
{
    [Header("Settings")] 
    public GameObject player;
    public float currentAngle;
    public float shellMass;
    public float shellInitialForce;
    
    [Header("Training")]
    public bool train;
    public SimulationController simulationController;
    public float decisionOutcomeCheckDelay = 1.5f;
    
    List<float[,,]> _observationInputs;
    List<float[]> _decisionOutputs;
    List<float> _decisionWeights;
    
    const int _observationsNumber = 5;
    const int _hiddenLayerSize = 150;
    const int _decisionsNumber = 1;
    
    Noedify_Solver evalSolver;
    
    [HideInInspector]
    [System.Serializable]
    public class ObservationDecision
    {
        public float[] observation;
        public float[] decision;
        public float weight;
    }

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
        
        if (simulationController == null)
            simulationController = GameObject.Find("SimulationController").GetComponent<SimulationController>();
        
        _observationInputs = new List<float[,,]>();
        _decisionOutputs = new List<float[]>();
        _decisionWeights = new List<float>();
        
        evalSolver = Noedify.CreateSolver();
    }

    private void Update()
    {
        float[] observations = AcquireObservations();
    }
    
    float[] AcquireObservations()
    {
        /* sensorInputs[5]
         sensorInputs[0]: playerRelativePositionX
         sensorInputs[1]: playerRelativePositionY
         sensorInputs[2]: currentAngle
         sensorInputs[3]: shellMass
         sensorInputs[4]: shellInitialForce
        */
        float[] sensorInputs = new float[_observationsNumber];

        sensorInputs[0] = transform.position.x - player.transform.position.x;
        sensorInputs[1] = transform.position.y - player.transform.position.y;
        sensorInputs[2] = currentAngle;
        sensorInputs[3] = shellMass;
        sensorInputs[4] = shellInitialForce;
        
        return sensorInputs;
    }
}
