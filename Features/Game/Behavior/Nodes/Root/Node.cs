using UnityEngine;

namespace Behavior
{
    public enum NodeStatus{ Success, Failure, Running };

    public abstract class Node
    {
        public abstract NodeStatus Evaluate();
    }

}

