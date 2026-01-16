using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;
using NUnit.Framework;

namespace MC.Core.Characters.Graph.Tests
{
    public class LevelUpGraphTests
    {
        [Test]
        public void ExpGain_LevelsUpAndIncreasesStrength()
        {
            var runtime = new GraphRuntime();

            var initialExpValue = 0;
            var exp = new NumericValueNode("EXP", initialExpValue);
            var level = new NumericValueNode("Level", 1);
            var strength = new NumericValueNode("Strength", 5);

            // Condition: EXP >= Level * 100
            var levelUpCondition = new ConditionNode(
                "LevelUpCondition",
                () => exp.Value >= level.Value * 100
            );

            // Operations
            var addLevel = new OperationNode(
                "AddLevel",
                ctx => level.Add(1, ctx)
            );

            var addStrength = new OperationNode(
                "AddStrength",
                ctx => strength.Add(1, ctx)
            );

            // Connections
            var context = runtime.Context;
            context.Register(new Connection(exp, NodeTrigger.OnValueChanged, levelUpCondition));
            context.Register(new Connection(levelUpCondition, NodeTrigger.OnTrue, addLevel));
            context.Register(new Connection(level, NodeTrigger.OnValueChanged, addStrength));

            // Act
            var expToAdd = 100;
            exp.Add(expToAdd, context);
            runtime.Tick();

            // Assert
            Assert.AreEqual(initialExpValue + expToAdd, exp.Value);        // no reset yet
            Assert.AreEqual(2, level.Value);
            Assert.AreEqual(6, strength.Value);
        }
    }
}
