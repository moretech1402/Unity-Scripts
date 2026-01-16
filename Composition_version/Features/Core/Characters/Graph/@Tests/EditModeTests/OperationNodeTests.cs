using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class OperationNodeTests
    {
        [Test]
        public void OperationNode_ExecutesOperation()
        {
            var runtime = new GraphRuntime();
            var strength = new NumericValueNode("Strength", 5);

            var addStrength = new OperationNode(
                "AddStrength",
                ctx => strength.Add(1, ctx)
            );

            runtime.Context.Enqueue(addStrength);
            runtime.Tick();

            Assert.AreEqual(6, strength.Value);
        }
    }
}
