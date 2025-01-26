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
}
