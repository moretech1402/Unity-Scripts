using System;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public sealed class OperationNode : Node
    {
        private readonly Action<GraphContext> _operation;

        public OperationNode(string id, Action<GraphContext> operation)
            : base(id)
        {
            _operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        public override void Process(GraphContext context)
        {
            _operation(context);
            Emit(NodeTrigger.OnExecuted, context);
        }
    }
}
