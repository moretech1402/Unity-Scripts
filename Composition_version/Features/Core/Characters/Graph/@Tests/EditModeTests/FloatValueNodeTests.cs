using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Modifiers;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class FloatValueNodeTests
    {
        [Test]
        public void Modifiers_AffectFinalValueCorrectly()
        {
            var context = new GraphContext();
            var damage = new FloatValueNode("Damage", 10f);

            damage.AddModifier(new Modifier(ModifierType.Add, 5f), context);
            damage.AddModifier(new Modifier(ModifierType.Multiply, 1.5f), context);
            damage.AddModifier(new Modifier(ModifierType.PostAdd, 2f), context);

            Assert.AreEqual(24.5f, damage.FinalValue, 0.001f);
        }
    }
}
