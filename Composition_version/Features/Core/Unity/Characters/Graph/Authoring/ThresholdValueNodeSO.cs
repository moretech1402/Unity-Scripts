using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using MC.Core.Operators;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Threshold/ValueNode")]
    public sealed class ThresholdValueNodeSO : NodeSO
    {
        public NumericValueNodeSO Left;
        public NumericValueNodeSO Right;
        public ComparisonOperator Comparison;

        public override string Id => $"ThresholdNode-{base.Id}";

        public override Node Build(GraphContext context)
        {
            var leftNode = context.RequireNode<NumericValueNode>(Left.Id);
            var rightNode = context.RequireNode<NumericValueNode>(Right.Id);

            return new ThresholdNode<int>(
                Id,
                () => leftNode.Value,
                () => rightNode.Value,
                Comparison
            );
        }
    }

}