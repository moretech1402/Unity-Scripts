using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class GraphTracingTests
    {
        [Test]
        public void Trace_Records_NodeExecution()
        {
            var trace = new GraphTraceCollector();
            var runtime = new GraphRuntime(trace);

            var node = new TestNode("Test");

            runtime.Context.Enqueue(node);
            runtime.Tick();

            var traceEvents = trace.Events;
            var firstTrace = traceEvents[0];

            Assert.AreEqual(2, traceEvents.Count);
            Assert.AreEqual(node, firstTrace.Node);
            Assert.AreEqual(NodeTrigger.OnExecuted, firstTrace.Trigger);
        }
    }
}
