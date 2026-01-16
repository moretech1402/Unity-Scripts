using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Nodes.Values;
using MC.Core.Characters.Graph.Runtime;
using MC.Core.Operators;
using MC.Core.Values;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Threshold/ScaledValue")]
    public sealed class ThresholdScaledValueNodeSO : NodeSO
    {
        public NumericValueNodeSO Left;
        public NumericValueNodeSO Right;
        public int Multiplier = 100;
        public ComparisonOperator Comparison;

        public override Node Build(GraphContext context)
        {
            var leftNode = context.RequireNode<NumericValueNode>(Left.Id);
            var rightNode = context.RequireNode<NumericValueNode>(Right.Id);

            var left = new NodeValueProvider<int>(leftNode);
            var right = new MultiplyValueProvider(
                new NodeValueProvider<int>(rightNode),
                Multiplier
            );

            return new ThresholdNode<int>(
                Id,
                () => left.Get(),
                () => right.Get(),
                Comparison
            );
        }
    }

}