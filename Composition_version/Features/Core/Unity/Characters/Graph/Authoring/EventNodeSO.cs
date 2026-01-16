using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{

    [CreateAssetMenu(menuName = "Core/Characters/Graph/Nodes/Create new Event Node")]
    public class EventNodeSO : NodeSO
    {
        public override string Id => $"Event-{base.Id}";
        public override Node Build(GraphContext context)
        {
            return new EventNode(Id);
        }
    }
}
