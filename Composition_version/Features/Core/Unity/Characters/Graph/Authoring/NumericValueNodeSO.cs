using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{

    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Numeric Value")]
    public sealed class NumericValueNodeSO : NodeSO
    {
        public int InitialValue;

        public override string Id => $"NumericValue-{base.Id}";
        public override Node Build(GraphContext context)
        {
            return new NumericValueNode(Id, InitialValue);
        }
    }
}
