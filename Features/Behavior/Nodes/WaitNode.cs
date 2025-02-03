using Core.Events;
using UnityEngine;

namespace Behavior
{
    public class WaitNode : Node
    {
        float timer, duration;
        NPCBehavior npc;

        public WaitNode(NPCBehavior npc, float duration) { this.npc = npc; this.duration = duration; }
        public WaitNode(NPCBehavior npc, float durationMin, float durationMax) { this.npc = npc; duration = Random.Range(durationMin, durationMax); }

        /// <summary> Evaluate the waiting node. </summary>
        /// <returns>Nodestatus.running while waiting, nodestatus.success when it ends.</returns>
        public override NodeStatus Evaluate()
        {
            npc.currentAction = "Waiting";
            MoveEventManager.ComplexMove(npc.gameObject.GetInstanceID(), MovementState.Stopped);
            // Increase the timer over time from the last frame
            timer += Time.deltaTime;

            // Verify if the waiting time is over
            if (timer >= duration)
            {
                // Restart the timer for the next execution
                timer = 0f;
                return NodeStatus.Success; // The wait is over
            }

            return NodeStatus.Running; // He is still waiting
        }
    }

}
