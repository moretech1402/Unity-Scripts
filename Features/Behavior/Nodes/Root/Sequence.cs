using System.Collections.Generic;

namespace Behavior
{
    public class Sequence : Node{
        List<Node> nodes = new();
        int currentIndex;

        public Sequence(List<Node> nodes) => this.nodes = nodes;

        public override NodeStatus Evaluate()
    {
        while (currentIndex < nodes.Count)
        {
            NodeStatus childState = nodes[currentIndex].Evaluate();
            
            if (childState == NodeStatus.Running)
                return NodeStatus.Running;  // Wait for you to end
            
            if (childState == NodeStatus.Failure)
            {
                currentIndex = 0; // Restart
                return NodeStatus.Failure;
            }

            currentIndex++; // Advance if he succeeded
        }

        currentIndex = 0;
        return NodeStatus.Success;
    }
    }

}
