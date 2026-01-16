using System;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes.Operations
{
    public sealed class AddOperation : IValueOperation<int>
    {
        private readonly Func<int> _amount;

        public AddOperation(Func<int> amount)
        {
            _amount = amount ?? throw new ArgumentNullException(nameof(amount));
        }

        public int Apply(int current, GraphContext context)
        {
            return current + _amount();
        }
    }
}
