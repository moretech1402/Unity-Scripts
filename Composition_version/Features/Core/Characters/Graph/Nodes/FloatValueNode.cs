using System.Collections.Generic;
using System.Linq;
using MC.Core.Characters.Graph.Modifiers;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Characters.Graph.Nodes
{
    public sealed class FloatValueNode : ValueNode<float>
    {
        private readonly List<Modifier> _modifiers = new();

        public FloatValueNode(string id, float baseValue)
            : base(id, baseValue)
        {
        }

        public float BaseValue => Value;

        public float FinalValue
        {
            get
            {
                float result = BaseValue;

                foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Add))
                    result += mod.Value;

                foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.Multiply))
                    result *= mod.Value;

                foreach (var mod in _modifiers.Where(m => m.Type == ModifierType.PostAdd))
                    result += mod.Value;

                return result;
            }
        }

        public void SetBase(float value, GraphContext context)
        {
            SetValue(value, context);
        }

        public void AddModifier(Modifier modifier, GraphContext context)
        {
            _modifiers.Add(modifier);
            Emit(NodeTrigger.OnValueChanged, context);
        }

        public void RemoveModifier(Modifier modifier, GraphContext context)
        {
            if (_modifiers.Remove(modifier))
            {
                Emit(NodeTrigger.OnValueChanged, context);
            }
        }

        public override void Process(GraphContext context)
        {
            throw new System.NotImplementedException();
        }

        public IReadOnlyList<Modifier> Modifiers => _modifiers;
    }
}
