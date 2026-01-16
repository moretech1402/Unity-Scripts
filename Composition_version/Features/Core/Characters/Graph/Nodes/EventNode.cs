using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public sealed class EventNode : Node
    {
        public EventNode(string id) : base(id)
        {
        }

        public override void Process(GraphContext context)
        {
            Emit(NodeTrigger.OnExecuted, context);
        }

        public void Raise(GraphContext context)
        {
            context.Enqueue(this);
        }
    }
}
