using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class EventNodeTests
    {
        [Test]
        public void EventNode_TriggersConnectedNodes()
        {
            var runtime = new GraphRuntime();

            var onEnemyDefeated = new EventNode("OnEnemyDefeated");
            var listener = new TestNode("Listener");

            runtime.Context.Register(
                new Connection(onEnemyDefeated, NodeTrigger.OnExecuted, listener)
            );

            onEnemyDefeated.Raise(runtime.Context);
            runtime.Tick();

            Assert.IsTrue(listener.Executed);
        }
    }
}
