using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Characters.Graph.Runtime
{
    public sealed class Connection
    {
        public Node Source { get; }
        public Node Target { get; }
        public NodeTrigger Trigger { get; }

        public Connection(Node source, NodeTrigger trigger, Node target)
        {
            Source = source;
            Trigger = trigger;
            Target = target;
        }

        public bool Matches(Node source, NodeTrigger trigger)
        {
            return Source == source && Trigger == trigger;
        }
    }
}
