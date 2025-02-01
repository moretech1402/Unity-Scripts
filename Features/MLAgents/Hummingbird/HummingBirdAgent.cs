using System;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Tooltip("Name of nectar tag")]
    [SerializeField] string nectarTag = "Nectar";


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
    float smoothPitchChange;

    /// <summary>Allows for smoother yaw changes</summary>
    float smoothYawChange;

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

    /// <summary>
    /// Active or deactive moving and actions of the agent
    /// </summary>
    /// <param name="freeze">Whether agent active or deactive</param>
    public void FreezeAgent(bool freeze){
        Debug.Assert(trainingMode == false, "Freeze/Unfreeze not supported in training");
        frozen = freeze;
        if(freeze) rigidbody.Sleep();
        else rigidbody.WakeUp();
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

    /// <summary>
    /// Called when an action is received from either the player input or the neural network
    /// </summary>
    /// <param name="actions">
    ///     actions.ContinuousActions[0]: move vector x (-1: left, +1: right)
    ///     actions.ContinuousActions[1]: move vector y (-1: down, +1: up)
    ///     actions.ContinuousActions[2]: move vector z (-1: backward, +1: forward)
    ///     actions.ContinuousActions[3]: pitch angle (-1: pitch down, +1: pitch up)
    ///     actions.ContinuousActions[4]: yaw angle (-1: turn left, +1: turn right)
    /// </param>
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Don't take actions if frozen
        if(frozen) return;

        var continuousActions = actions.ContinuousActions;
        // Calculate movement vector
        Vector3 move = new(continuousActions[0], continuousActions[1], continuousActions[2]);

        // Add force in the direction of the move vector
        rigidbody.AddForce(move * moveForce);

        // Get the current rotation
        var currentRotation = transform.rotation.eulerAngles;

        // Calculate pitch and yaw rotation
        var pitchChange = continuousActions[3];
        var yawChange = continuousActions[4];

        // Calculate smooth rotation changes
        smoothPitchChange = Mathf.MoveTowards(smoothPitchChange, pitchChange, 2f * Time.fixedDeltaTime);
        smoothYawChange = Mathf.MoveTowards(smoothYawChange, yawChange, 2f * Time.fixedDeltaTime);

        // Calculate new pitch and yaw based on new smoothed values
        // Clamp pitch to avoid flipping upside down
        float pitch = currentRotation.x + smoothPitchChange * Time.fixedDeltaTime * pitchSpeed;
        if(pitch > 180f) pitch -= 360f;
        pitch = Mathf.Clamp(pitch, -MAX_PITCH_ANGLE, MAX_PITCH_ANGLE);

        float yaw = currentRotation.y + smoothYawChange * Time.fixedDeltaTime * yawSpeed;

        // Apply the new rotation
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    /// <summary>
    /// Collect vector observations from the environment
    /// </summary>
    /// <param name="sensor">The vector sensor</param>
    public override void CollectObservations(VectorSensor sensor)
    {
        // If nearest flower is null, observe an empty array and return early
        if(nearestFlower == null){
            sensor.AddObservation(new float[10]);
            return;
        }

        // Observe the agent's local rotation (4 observation)
        sensor.AddObservation(transform.localRotation.normalized);

        // Get a vector from the beak tip to the nearest flower
        Vector3 toFlower = nearestFlower.FlowerCenterPosition - beakTip.position;

        // Observe a normalized vector pointing to the nearest flower (3 observation)
        sensor.AddObservation(toFlower.normalized);

        // Observe a dot product that indicates whether the beak tip is in front of the flower (1 observation)
        // (+1 means that the beak tip is directly in front of the flower, -1 means directly behaind)
        Vector3.Dot(toFlower.normalized, -nearestFlower.FlowerVectorUp.normalized);

        // Observe a dot product that indicates whether the beak tip is pointing toward the flower (1 observation)
        // (+1 means that the beak tip is pointing directly the flower, -1 means directly away)
        Vector3.Dot(beakTip.forward.normalized, -nearestFlower.FlowerVectorUp.normalized);

        // Observe the relative distance from beak tip to the flower (1 observation)
        sensor.AddObservation(toFlower.magnitude / FlowerArea.AREA_DIAMETER);

        // 10 total observations
    }

    /// <summary>
    /// When Behavior Type is set to "Heuristic Only" on the agent's Behavior Parameters this function will be called.
    /// Its return values will be fed into <see cref="OnActionReceived(ActionBuffers)"/> instead of using neural network.
    /// </summary>
    /// <param name="actionsOut">An output action buffer</param>
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Create placeholders for all movement/turning
        Vector3 forward = Vector3.zero, left = Vector3.zero, up = Vector3.zero;
        float pitch, yaw;

        // Convert keyboard inputs in movement and turning.
        // All values should be beetween -1 and +1

        static float GetDirectionMult(KeyCode positive, KeyCode negative){
            if(Input.GetKey(positive)) return 1;
            else if(Input.GetKey(negative)) return -1;
            return 0;
        }

        forward = transform.forward * GetDirectionMult(KeyCode.W, KeyCode.S);
        left = transform.right * GetDirectionMult(KeyCode.D, KeyCode.A);
        up = transform.up * GetDirectionMult(KeyCode.E, KeyCode.C);

        pitch = 1 * GetDirectionMult(KeyCode.UpArrow, KeyCode.DownArrow);
        yaw = 1 * GetDirectionMult(KeyCode.RightArrow, KeyCode.LeftArrow);

        // Combine the movement vectors and normalize
        Vector3 combined = (forward + left + up).normalized;

        // Add the movement, pitch and yaw values to actionsOut
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = combined.x;
        continuousActionsOut[1] = combined.y;
        continuousActionsOut[2] = combined.z;
        continuousActionsOut[3] = pitch;
        continuousActionsOut[4] = yaw;
    }

    #region Collisions

    /// <summary>
    /// Called when the agent's enters or stays a trigger collider
    /// </summary>
    /// <param name="other">The trigger's collider</param>
    void TriggerEnterOrStay(Collider collider){
        // Check if the agents is colliding with nectar
        if(collider.CompareTag(nectarTag)){
            Vector3 closestPointToBeakTip = collider.ClosestPoint(beakTip.position);

            // Check if collider closest point is close to the beak tip
            // Note: a collision with anything but the beak tip should not count
        }
    }

    /// <summary>
    /// Called when the agent's enters a trigger collider
    /// </summary>
    /// <param name="other">The trigger's collider</param>
    private void OnTriggerEnter(Collider other) => TriggerEnterOrStay(other);

    /// <summary>
    /// Called when the agent's stays a trigger collider
    /// </summary>
    /// <param name="other">The trigger's collider</param>
    private void OnTriggerStay(Collider other) => TriggerEnterOrStay(other);

    #endregion

    #endregion

    #endregion
}
