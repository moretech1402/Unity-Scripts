using System.Collections.Generic;
using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Characters.Graph.Runtime
{
    public sealed class ValueSnapshot
    {
        private readonly Dictionary<string, object> _values = new();

        public IReadOnlyDictionary<string, object> Values => _values;

        public object this[string id] => _values[id];

        public static ValueSnapshot Capture(IEnumerable<IValueNode> nodes)
        {
            var snapshot = new ValueSnapshot();

            foreach (var node in nodes)
            {
                snapshot._values[node.Id] = node.GetValue();
            }

            return snapshot;
        }
    }
}
