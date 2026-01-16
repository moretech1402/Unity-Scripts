using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public abstract class ValueNode<T> : Node, IValueNode
    {
        public T Value { get; private set; }

        protected ValueNode(string id, T initialValue)
            : base(id)
        {
            Value = initialValue;
        }

        public object GetValue() => Value;

        protected void SetValue(T newValue, GraphContext context)
        {
            if (Equals(Value, newValue))
                return;

            Value = newValue;
            OnValueChanged(context);
        }

        protected void OnValueChanged(GraphContext context)
        {
            Emit(NodeTrigger.OnValueChanged, context);
        }
    }
}
