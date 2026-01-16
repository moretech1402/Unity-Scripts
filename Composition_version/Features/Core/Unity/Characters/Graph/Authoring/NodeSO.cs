using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using UnityEngine;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    public abstract class NodeSO : ScriptableObject
    {
        public virtual string Id => $"{name}-{GetInstanceID()}";

        public abstract Node Build(GraphContext context);
    }
}
