using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes.Operations
{
    public interface IValueOperation<T>
    {
        T Apply(T current, GraphContext context);
    }
}
