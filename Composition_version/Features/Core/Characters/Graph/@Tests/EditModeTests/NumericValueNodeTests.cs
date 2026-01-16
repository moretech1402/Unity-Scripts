using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.@Tests
{

    public sealed class TestNode : Node
    {
        public bool Executed { get; private set; }

        public TestNode(string id) : base(id)
        {
        }

        public override void Process(GraphContext context)
        {
            Executed = true;
        }
    }

    public class NumericValueNodeTests
    {
        private GraphRuntime _runtime;
        NumericValueNode _expNode;

        [SetUp]
        public void SetUp()
        {
            _runtime = new GraphRuntime();
            _expNode = new NumericValueNode("EXP", 0);
        }

        [Test]
        public void Add_IncreasesValue()
        {
            _expNode.Add(100, _runtime.Context);

            Assert.AreEqual(100, _expNode.Value);
        }

        [Test]
        public void ValueChange_TriggersConnectedNode()
        {
            var listener = new TestNode("Listener");

            _runtime.Context.Register(
                new Connection(_expNode, NodeTrigger.OnValueChanged, listener)
            );

            _expNode.Add(50, _runtime.Context);
            _runtime.Tick();

            Assert.IsTrue(listener.Executed);
        }
    }

}
