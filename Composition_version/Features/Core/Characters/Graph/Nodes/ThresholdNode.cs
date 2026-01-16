using System;
using MC.Core.Operators;

namespace MC.Core.Characters.Graph.Nodes
{
    public sealed class ThresholdNode<T> : ConditionNode
        where T : IComparable<T>
    {
        public ThresholdNode(
            string id,
            Func<T> value,
            Func<T> threshold,
            ComparisonOperator comparison)
            : base(
                id,
                CreateCondition(value, threshold, comparison)
            )
        {
        }

        private static Func<bool> CreateCondition(
            Func<T> value,
            Func<T> threshold,
            ComparisonOperator comparison)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (threshold == null) throw new ArgumentNullException(nameof(threshold));

            return () => ComparisonEvaluator.Evaluate(
                value().CompareTo(threshold()),
                comparison
            );
        }
    }
}
