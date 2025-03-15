using System.Collections.Generic;

namespace Behavior
{
    public class Selector : Node
    {
        List<Node> nodes = new();
        int currentIndex;

        public Selector(List<Node> nodes) => this.nodes = nodes;

        public override NodeStatus Evaluate()
        {
            while (currentIndex < nodes.Count)
            {
                NodeStatus childState = nodes[currentIndex].Evaluate();

                if (childState == NodeStatus.Running)
                    return NodeStatus.Running;  // Do not advance until it ends

                if (childState == NodeStatus.Success)
                {
                    currentIndex = 0; // Restart index at the end
                    return NodeStatus.Success;
                }

                currentIndex++; // Try next node if you failed
            }

            currentIndex = 0;
            return NodeStatus.Failure;
        }
    }
}
