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


    public void MoveToSafeRandomPosition(bool inFrontOfFlower)
    {
        // ...
    }

    public void UpdateNearestFlower()
    {
        // ...
    }

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        flowerArea = GetComponentInParent<FlowerArea>();

        // If not training mode, no max step, play forever
        if(!trainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        // Reset Nectar obtained
        NectarObtained = 0f;

        // Zero out velocities so that movement stops before a new episode begins
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;

        // Default to spawning in front of a flower
        bool inFrontOfFlower = true;
        if(trainingMode){
            // Only reset in training when there is one agent per area
            flowerArea.ResetFlowers();

            // Spawn in front of a flower 50% of the time during training
            inFrontOfFlower = Random.value > .5f;
        }

        // Move the agent to a new random position
        MoveToSafeRandomPosition(inFrontOfFlower);

        // Recalculate the nearest flowers now that the agent has moved
        UpdateNearestFlower();
    }
}
