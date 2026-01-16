using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using MC.Core.Operators;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{

    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Threshold/Constant")]
    public sealed class ThresholdConstantNodeSO : NodeSO
    {
        public NumericValueNodeSO Value;
        public int Threshold;
        public ComparisonOperator Comparison;

        public override string Id => $"ThresholdConst-{base.Id}";

        public override Node Build(GraphContext context)
        {
            var valueNode = context.RequireNode<NumericValueNode>(Value.Id);

            return new ThresholdNode<int>(
                Id,
                () => valueNode.Value,
                () => Threshold,
                Comparison
            );
        }
    }
}
