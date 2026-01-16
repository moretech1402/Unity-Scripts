using System;
using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Unity.Characters.Graph.Authoring
{

    public class OperationNodeSO : NodeSO
    {
        public NumericValueNodeSO Target;

        public override Node Build(GraphContext context)
        {
            var targetId = Target.Id;
            if (!context.TryGetNode<NumericValueNode>(targetId, out var valueNode))
            {
                throw new InvalidOperationException(
                    $"OperationNode '{Id}' references missing NumericValueNode '{targetId}'."
                );
            }

            return new OperationNode(
            Id,
            ctx => { }
            );
        }
    }
}
