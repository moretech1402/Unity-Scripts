using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    [System.Serializable]
    public sealed class ConnectionData
    {
        public NodeSO source;
        public NodeTrigger trigger;
        public NodeSO target;
    }

}