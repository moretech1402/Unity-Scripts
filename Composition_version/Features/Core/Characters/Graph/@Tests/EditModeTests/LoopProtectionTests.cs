using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class LoopProtectionTests
    {
        [Test]
        public void Node_IsNotExecutedMoreThanOnce_PerTick()
        {
            var runtime = new GraphRuntime();

            var nodeA = new TestNode("A");
            var nodeB = new TestNode("B");

            runtime.Context.Register(new Connection(nodeA, NodeTrigger.OnExecuted, nodeB));
            runtime.Context.Register(new Connection(nodeB, NodeTrigger.OnExecuted, nodeA));

            runtime.Context.Enqueue(nodeA);
            runtime.Tick();

            Assert.IsTrue(nodeA.Executed);
            Assert.IsTrue(nodeB.Executed);
        }
    }
}
