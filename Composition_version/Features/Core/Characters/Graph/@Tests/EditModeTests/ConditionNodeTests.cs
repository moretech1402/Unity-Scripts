using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public sealed class ConditionNodeTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Condition_TriggersCorrectBranch(bool conditionValue)
        {
            // Arrange
            var runtime = new GraphRuntime();
            var context = runtime.Context;

            var condition = new ConditionNode("Cond", () => conditionValue);
            var onTrueNode = new TestNode("OnTrue");
            var onFalseNode = new TestNode("OnFalse");

            context.Register(new Connection(condition, NodeTrigger.OnTrue, onTrueNode));
            context.Register(new Connection(condition, NodeTrigger.OnFalse, onFalseNode));

            // Act
            context.Enqueue(condition);
            runtime.Tick();

            // Assert
            Assert.AreEqual(conditionValue, onTrueNode.Executed);
            Assert.AreEqual(!conditionValue, onFalseNode.Executed);
        }

    }
}
