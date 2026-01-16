using System;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public class ConditionNode : Node
    {
        private readonly Func<bool> _condition;

        public ConditionNode(string id, Func<bool> condition)
            : base(id)
        {
            _condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }

        protected bool Evaluate()
        {
            return _condition();
        }

        public override void Process(GraphContext context)
        {
            Emit(
                Evaluate() ? NodeTrigger.OnTrue : NodeTrigger.OnFalse,
                context
            );
        }
    }
}
