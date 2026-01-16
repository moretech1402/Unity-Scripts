using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Operations/Create new Add Operation")]
    public sealed class AddOperationNodeSO : NodeSO
    {
        public NumericValueNodeSO Target;
        public int Amount;

        public override string Id => $"AddOperation-{base.Id}";

        public override Node Build(GraphContext context)
        {
            var target = context.RequireNode<NumericValueNode>(Target.Id);

            return new OperationNode(
                Id,
                (context) =>
                {
                    target.Add(Amount, context);
                }
            );
        }
    }

}
