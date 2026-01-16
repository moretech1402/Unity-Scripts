using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public interface INodeEmitter
    {
        void Emit(NodeTrigger trigger, GraphContext context);
    }
}
