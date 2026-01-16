using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public sealed class NumericValueNode : ValueNode<int>
    {
        public NumericValueNode(string id, int initialValue)
            : base(id, initialValue)
        {
        }

        public override void Process(GraphContext context)
        {

        }

        public void Add(int amount, GraphContext context)
        {
            SetValue(Value + amount, context);
        }

        public void Set(int value, GraphContext context)
        {
            SetValue(value, context);
        }
    }
}
