using Unity.MLAgents;
using UnityEngine;

/// <summary>Humming Bird Machine Learning Agent</summary>
public class HummingBirdAgent : Agent
{
    #region Variables & Accesor

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

    #endregion

    #region Functions

    #region Functions/Move

    /// <summary>
    /// Generates a random position within the flower area.
    /// </summary>
    /// <returns>A Vector3 representing the random position.</returns>
    private Vector3 GenerateRandomPositionInArea()
    {
        // Pick a random height from the ground
        float height = Random.Range(1.2f, 2.5f);

        // Pick a random radius from the center of area
        float radius = Random.Range(2f, 7f);

        // Pick a random direction rotated around y axis
        var direction = Quaternion.Euler(0, Random.Range(-180f, 180f), 0);

        // Combine height, radius and direction to pick a potential position
        return flowerArea.transform.position + Vector3.up * height + direction * Vector3.forward * radius;
    }

    /// <summary>
    /// Generates a position in front of a random flower.
    /// </summary>
    /// <returns>A Vector3 representing the position in front of the flower.</returns>
    private Vector3 GeneratePositionInFrontOfFlower(Flower flower)
    {
        // Position 10 to 20 cm in front of the flower
        float distanceToFlower = Random.Range(.1f, .2f);
        return flower.transform.position + flower.FlowerVectorUp * distanceToFlower;
    }

    /// <summary>
    /// Generates a random rotation.
    /// </summary>
    /// <returns>A Quaternion representing the random rotation.</returns>
    private Quaternion GenerateRandomRotation()
    {
        // Choose and set random starting pitch and yaw
        var pitch = Random.Range(-60f, 60f);
        var yaw = Random.Range(-180f, 180f);
        return Quaternion.Euler(pitch, yaw, 0);
    }

    /// <summary>
    /// Sets the potential position and rotation of the agent based on whether it should be positioned in front of a flower or at a random location within the flower area.
    /// </summary>
    /// <param name="inFrontOfFlower">
    /// A boolean indicating whether the agent should be positioned in front of a flower.
    ///   - If true, the agent will be placed in front of a randomly chosen flower from the flowerArea.
    ///   - If false, the agent will be placed at a random position within the flower area.
    /// </param>
    /// <param name="potentialPosition">
    /// (out Vector3) A Vector3 that will be set to the calculated potential position of the agent.
    /// This parameter is passed by reference, meaning the method modifies the original variable passed to it.
    /// </param>
    /// <param name="potentialRotation">
    /// (out Quaternion) A Quaternion that will be set to the calculated potential rotation of the agent.
    /// This parameter is passed by reference, meaning the method modifies the original variable passed to it.
    /// </param>
    void SetPotentialPositionAndRotation(bool inFrontOfFlower, out Vector3 potentialPosition, out Quaternion potentialRotation)
    {
        if (inFrontOfFlower)
        {
            // Pick a random flower
            Flower randomFlower = flowerArea.Flowers[Random.Range(0, flowerArea.Flowers.Count)];

            potentialPosition = GeneratePositionInFrontOfFlower(randomFlower);

            // Point beak to flower (bird's head is center of transform)
            Vector3 toFlower = randomFlower.FlowerCenterPosition - potentialPosition;
            potentialRotation = Quaternion.LookRotation(toFlower);
        }
        else
        {
            potentialPosition = GenerateRandomPositionInArea();
            potentialRotation = GenerateRandomRotation();
        }
    }

    /// <summary>
    /// Move the agent to a safe random position (i.e. does not collide with anything)
    /// If in front of flower, also point the beak at the flower
    /// </summary>
    /// <param name="inFrontOfFlower">Whether to choose a spot in front of a flower</param>
    public void MoveToSafeRandomPosition(bool inFrontOfFlower)
    {
        int maxAttempts = 100; // For preventing infinite loop
        bool foundSafePositionWithinAttempts = false;
        var potentialPosition = Vector3.zero;
        var potentialRotation = new Quaternion();

        // Loop until safe position is found or we run out of attempts
        while (!foundSafePositionWithinAttempts && maxAttempts > 0)
        {
            maxAttempts--;
            SetPotentialPositionAndRotation(inFrontOfFlower, out potentialPosition, out potentialRotation);

            // Check to see if the agent collide with anything
            bool hasCollisions = Physics.CheckSphere(potentialPosition, .05f);

            // Safe position is found if no colliders are overlapped
            foundSafePositionWithinAttempts = !hasCollisions;
        }

        Debug.Assert(foundSafePositionWithinAttempts, "Could not find a safe position to spawn");

        // Set the position and rotation
        transform.SetPositionAndRotation(potentialPosition, potentialRotation);
    }

    #endregion

    #region Functions/Flower

    /// <summary>Update the nearest flower to the agent</summary>
    public void UpdateNearestFlower()
    {
        foreach(var flower in flowerArea.Flowers){
            if(nearestFlower == null && flower.HasNectar){
                // No current nearest flower and this flower has nectar, so set to this flower
                nearestFlower = flower;
            } else if(flower.HasNectar){
                // Calculate distance to this flower and distance to current nearest flower
                float distanceToFlower = Vector3.Distance(flower.transform.position, beakTip.position);
                float distanceToCurrentNearestFlower = Vector3.Distance(nearestFlower.transform.position, beakTip.position);

                if(!nearestFlower.HasNectar || distanceToFlower < distanceToCurrentNearestFlower)
                    nearestFlower = flower;
            }
        }
    }

    #endregion

    #region Life Cycle

    public override void Initialize()
    {
        rigidbody = GetComponent<Rigidbody>();
        flowerArea = GetComponentInParent<FlowerArea>();

        // If not training mode, no max step, play forever
        if (!trainingMode) MaxStep = 0;
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
        if (trainingMode)
        {
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

    #endregion

    #endregion
}
