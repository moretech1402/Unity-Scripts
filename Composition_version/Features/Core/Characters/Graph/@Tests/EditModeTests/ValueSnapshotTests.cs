using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;
using System.Collections.Generic;

namespace MC.Core.Characters.Graph.Tests
{
    public class ValueSnapshotTests
    {
        [Test]
        public void Snapshot_Captures_Current_Values()
        {
            var context = new GraphContext();

            var level = new NumericValueNode("Level", 1);
            var strength = new NumericValueNode("Strength", 5);

            level.Add(1, context);
            strength.Add(2, context);

            var nodes = new List<IValueNode> { level, strength };
            var snapshot = ValueSnapshot.Capture(nodes);

            Assert.AreEqual(2, snapshot["Level"]);
            Assert.AreEqual(7, snapshot["Strength"]);
        }
    }
}
