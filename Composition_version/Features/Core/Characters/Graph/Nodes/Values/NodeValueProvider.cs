using MC.Core.Values;

namespace MC.Core.Characters.Graph.Nodes.Values
{
    public sealed class NodeValueProvider<T> : IValueProvider<T>
    {
        private readonly ValueNode<T> _node;

        public NodeValueProvider(ValueNode<T> node)
        {
            _node = node;
        }

        public T Get() => _node.Value;
    }

}

