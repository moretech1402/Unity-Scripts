using Core.Events;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class CharacterMovePathfinding : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    Animator animator;

    bool isMoving;

    /// <summary>
    /// Update the animation to indicate that the character is walking or not
    /// </summary>
    /// <param name="active">If is walking or not</param>
    void WalkingAnimation(bool active){
        MovementState state = active ? MovementState.Walking : MovementState.Stopped;

        MoveEventManager.ComplexMove(gameObject.GetInstanceID(), state);
        isMoving = active;
    }

    /// <summary>
    /// Move the character towards the specified destination.
    /// </summary>
    /// <param name="target">Target position</param>
    public void MoveTo(Vector3 target)
    {
        WalkingAnimation(true);

        // Establish destiny
        navMeshAgent.SetDestination(target); 
    }

    /// <summary> He stops the character and updates the animation. </summary>
    public void StopMoving()
    {
        WalkingAnimation(false);

        // Reset the route to stop the agent
        navMeshAgent.ResetPath();
    }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
}
