using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SimulationController : MonoBehaviour
{
    public NNTiltCalculator tiltCalculator;
    
    public int currentNumberOfTrainingSets = 0;
    public int runsSetsPerTraining = 10; // number of simulation runs before training network
    public Noedify.Net net;

    [Space(10)]
    [Header("Training Parameters")]
    public int trainingBatchSize = 4;
    public int trainingEpochs = 100;
    public float trainingRate = 1f;
    
    public List<NNTiltCalculator.ObservationDecision> trainingSet;
    
    Noedify_Solver trainingSolver;
    private int _numberOfThreads = 8;

    private int _observationsNumber = 5;
    private int _hiddenLayerSize = 150;
    private int _decisionsNumber = 1;

    private void Start()
    {
        trainingSolver = Noedify.CreateSolver();
        trainingSet = new List<NNTiltCalculator.ObservationDecision>();
        BuildNetwork();
    }

    private void Update()
    {
        if (currentNumberOfTrainingSets % runsSetsPerTraining == 0 & currentNumberOfTrainingSets > 0)
            StartCoroutine(TrainNetwork());
    }
    
    public void AddTrainingSet(NNTiltCalculator.ObservationDecision newSet)
    {
        currentNumberOfTrainingSets++;
        trainingSet.Add(newSet);
    }
    
    void BuildNetwork()
    {
        net = new Noedify.Net();

        /* Input layer */
        Noedify.Layer inputLayer = new Noedify.Layer(
            Noedify.LayerType.Input, // layer type
            _observationsNumber, // input size
            "input layer" // layer name
        );
        net.AddLayer(inputLayer);

        // Hidden layer 1 
        Noedify.Layer hiddenLayer0 = new Noedify.Layer(
            Noedify.LayerType.FullyConnected, // layer type
            _hiddenLayerSize, // layer size
            Noedify.ActivationFunction.Sigmoid, // activation function
            "fully connected 1" // layer name
        );
        net.AddLayer(hiddenLayer0);

        /* Output layer */
        Noedify.Layer outputLayer = new Noedify.Layer(
            Noedify.LayerType.Output, // layer type
            _decisionsNumber, // layer size
            Noedify.ActivationFunction.Sigmoid, // activation function
            "output layer" // layer name
        );
        net.AddLayer(outputLayer);

        net.BuildNetwork();
    }

    public IEnumerator TrainNetwork()
    {
        if (trainingSet != null)
        {
            if (trainingSet.Count > 0)
            {
                while (trainingSolver.trainingInProgress) { yield return null; }
                List<float[,,]> observation_inputs = new List<float[,,]>();
                List<float[]> decision_outputs = new List<float[]>();
                List<float> trainingSetWeights = new List<float>();
                for (int n=0; n < trainingSet.Count; n++)
                {
                    observation_inputs.Add(Noedify_Utils.AddTwoSingularDims(trainingSet[n].observation));
                    decision_outputs.Add(trainingSet[n].decision);
                    trainingSetWeights.Add(trainingSet[n].weight);
                }
                trainingSolver.TrainNetwork(net, observation_inputs, decision_outputs, trainingEpochs, trainingBatchSize, trainingRate, Noedify_Solver.CostFunction.MeanSquare, Noedify_Solver.SolverMethod.MainThread, trainingSetWeights, _numberOfThreads);
            }
        }
    }
}
