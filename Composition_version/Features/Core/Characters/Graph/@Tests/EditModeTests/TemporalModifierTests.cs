using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Modifiers;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class TemporalModifierTests
    {
        [Test]
        public void Modifier_Expires_AfterDuration()
        {
            var context = new GraphContext();
            var strength = new FloatValueNode("Strength", 10f);

            var mod = new Modifier(ModifierType.Add, 5f);

            var temporal = new TemporalModifier(
                strength,
                mod,
                durationTicks: 2,
                context);

            context.RegisterTemporal(temporal);

            Assert.AreEqual(15f, strength.FinalValue);

            context.Tick(); // 1
            Assert.AreEqual(15f, strength.FinalValue);

            context.Tick(); // 2 (expira)
            Assert.AreEqual(10f, strength.FinalValue);
        }
    }
}
