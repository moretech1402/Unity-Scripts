using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Modifiers
{
    public sealed class TemporalModifier : ITemporal
    {
        private readonly FloatValueNode _target;
        private readonly Modifier _modifier;
        private int _remainingTicks;

        public TemporalModifier(
            FloatValueNode target,
            Modifier modifier,
            int durationTicks,
            GraphContext context)
        {
            _target = target;
            _modifier = modifier;
            _remainingTicks = durationTicks;

            _target.AddModifier(_modifier, context);
        }

        public bool Tick(GraphContext context)
        {
            _remainingTicks--;

            if (_remainingTicks > 0)
                return true;

            _target.RemoveModifier(_modifier, context);
            return false;
        }
    }
}
