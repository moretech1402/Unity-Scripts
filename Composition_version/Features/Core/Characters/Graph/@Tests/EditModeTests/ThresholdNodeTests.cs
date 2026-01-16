using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using MC.Core.Operators;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class ThresholdNodeTests
    {
        [Test]
        public void Threshold_GreaterOrEqual_IsTrue_WhenValueMeetsThreshold()
        {
            var exp = 120;
            var level = 1;

            var node = new ThresholdNode<int>(
                "Test",
                () => exp,
                () => level * 100,
                ComparisonOperator.GreaterOrEqual
            );

            Assert.DoesNotThrow(() =>
            {
                node.Process(new GraphContext());
            });
        }

        [Test]
        public void Threshold_True_When_Value_Exceeds()
        {
            var context = new GraphContext();
            int value = 120;
            int threshold = 100;

            var node = new ThresholdNode<int>(
                "Threshold",
                () => value,
                () => threshold,
                ComparisonOperator.GreaterOrEqual);

            bool triggered = false;

            var listener = new OperationNode(
                "Listener",
                _ => triggered = true);

            context.Register(new Connection(
                node,
                NodeTrigger.OnTrue,
                listener));

            context.Enqueue(node);
            context.Tick();

            Assert.IsTrue(triggered);
        }

        [Test]
        public void Threshold_Reevaluates_When_Level_Changes()
        {
            var context = new GraphContext();

            var exp = new NumericValueNode("EXP", 250);
            var level = new NumericValueNode("Level", 1);

            var threshold = new ThresholdNode<int>(
                "Threshold",
                () => exp.Value,
                () => level.Value * 100,
                ComparisonOperator.Greater);

            var levelUp = new OperationNode(
                "LevelUp",
                ctx => level.Add(1, ctx));

            context.Enqueue(exp);
            context.Enqueue(threshold);
            context.Enqueue(level);

            context.Register(new Connection(exp, NodeTrigger.OnValueChanged, threshold));
            context.Register(new Connection(threshold, NodeTrigger.OnTrue, levelUp));
            context.Register(new Connection(level, NodeTrigger.OnValueChanged, threshold));

            exp.Add(0, context);

            context.Tick();

            Assert.AreEqual(3, level.Value);
        }

    }

}
