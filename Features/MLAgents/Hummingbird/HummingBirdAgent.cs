using Unity.MLAgents;
using UnityEngine;

/// <summary>Humming Bird Machine Learning Agent</summary>
public class HummingBirdAgent : Agent
{
    [Header("Stats")]

    [Tooltip("Force to apply when moving")]
    [SerializeField] float moveForce = 2f;

    [Tooltip("Speed to pitch up and down")]
    [SerializeField] float pitchSpeed = 100;

    [Tooltip("Speed to rotate around the Y axis")]
    [SerializeField] float yawSpeed = 100;


    [Header("References")]

    [Tooltip("Transform at the tip of the beak")]
    [SerializeField] Transform beakTip;

    [Tooltip("The agent's camera")]
    [SerializeField] Camera agentCamera;


    [Header("ML")]

    [Tooltip("Whether this is training mode or gameplay mode")]
    [SerializeField] bool trainingMode;


    /// <summary>The rigidbody of the agent</summary>
    new Rigidbody rigidbody;

    /// <summary>The flower area that the agent is in</summary>
    FlowerArea flowerArea;

    /// <summary>The nearest flower to the agent</summary>
    Flower nearestFlower;

    /// <summary>Allows for smoother pitch changes</summary>
    float smootherPitchChange;

    /// <summary>Allows for smoother yaw changes</summary>
    float smootherYawChange;

    /// <summary>Maximum angle that the bird can pitch up or down</summary>
    const float MAX_PITCH_ANGLE = 80;

    /// <summary>Maximum distance from the beak tip to accept nectar collision</summary>
    const float BEAK_TIP_RADIUS = .008f;

    /// <summary>Whether the agent is frozen (intentionally not flying)</summary>
    bool frozen;

    /// <summary>The amount of nectar the agent has obtained this episode</summary>
    public float NectarObtained { get; private set; }
}
