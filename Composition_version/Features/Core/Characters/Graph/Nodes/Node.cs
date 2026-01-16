using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public abstract class Node : INodeEmitter
    {
        public string Id { get; }

        protected Node(string id)
        {
            Id = id;
        }

        public abstract void Process(GraphContext context);

        public void Emit(NodeTrigger trigger, GraphContext context)
        {
            context.Emit(this, trigger);
        }

    }
}
