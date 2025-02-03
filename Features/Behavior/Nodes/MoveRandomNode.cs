using UnityEngine;
using UnityEngine.AI; // Required for NavMesh functionality

namespace Behavior
{
    public class MoveRandomNode : Node
    {
        // The target position to which the character will move
        private Vector3 targetPosition;

        NPCBehavior npc;

        // Reference to the movement component
        private CharacterMovePathfinding characterMove;

        // Constructor to initialize the movement component
        public MoveRandomNode(NPCBehavior npc)
        {
            this.npc = npc;
            characterMove = npc.CharacterMove;
            GenerateNewTarget(); // Generate the first target at the start
        }

        /// <summary>
        /// Checks if a position is on the NavMesh.
        /// </summary>
        /// <param name="position">The position to check.</param>
        /// <returns>True if the position is on the NavMesh, False otherwise.</returns>
        private bool IsPositionOnNavMesh(Vector3 position) => NavMesh.SamplePosition(position, out _, 2f, NavMesh.AllAreas);

        /// <summary>
        /// Generates a new valid random target within the NavMesh.
        /// </summary>
        private void GenerateNewTarget()
        {
            int maxAttempts = 100; // Limit the number of attempts to find a valid target
            Vector3 randomPosition;

            for (int i = 0; i < maxAttempts; i++)
            {
                // Generate a random position within a specific range
                randomPosition = new Vector3(
                    Random.Range(-5f, 5f), // X
                    Random.Range(0, .5f),  // height (will be adjusted if on NavMesh)
                    Random.Range(-5f, 5f)  // Z
                );

                // Check if the position is near a passable area in the NavMesh
                if (IsPositionOnNavMesh(randomPosition))
                {
                    // Adjust the position to the nearest point on the NavMesh
                    NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 2f, NavMesh.AllAreas);

                    // Set the adjusted position as the new target
                    targetPosition = hit.position;
                    return; // Exit the method after finding a valid target
                }
            }

            // If no valid target was found after several attempts, use the current position
            Debug.LogWarning("Could not find a valid target after " + maxAttempts + " attempts.");
            targetPosition = characterMove.transform.position; // Use the current position as a fallback
        }

        /// <summary>
        /// Checks if the character has reached the target.
        /// </summary>
        /// <returns>True if the character is close to the target, False otherwise.</returns>
        private bool HasReachedTarget()
        {
            if (targetPosition == Vector3.zero) return false;

            // Check if the distance to the target is less than a threshold (e.g., 0.5 units)
            float distanceToTarget = Vector3.Distance(characterMove.transform.position, targetPosition);
            return distanceToTarget < 0.5f;
        }

        /// <summary>
        /// Evaluates the patrol behavior.
        /// </summary>
        /// <returns>NodeStatus.Running while patrolling, NodeStatus.Success when reaching a target.</returns>
        public override NodeStatus Evaluate()
        {
            npc.currentAction = "Patrolling";
            // If the character has reached the current target, generate a new one
            if (HasReachedTarget())
            {
                GenerateNewTarget();
                return NodeStatus.Success; // Indicates that the objective has been achieved
            }

            // Move the character towards the current target
            if (targetPosition != Vector3.zero) // Ensure there is a valid target
            {
                characterMove.MoveTo(targetPosition);
                return NodeStatus.Running; // Indicates that the node is still in execution
            }
            else
            {
                characterMove.StopMoving();
                return NodeStatus.Failure; // Indicates that a valid objective could not be generated
            }
        }
    }
}