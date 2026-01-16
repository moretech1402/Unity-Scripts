using System.Collections.Generic;
using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Characters.Graph.Runtime
{
    public readonly struct GraphTraceEvent
    {
        public readonly Node Node;
        public readonly NodeTrigger Trigger;
        public readonly int TickIndex;

        public GraphTraceEvent(Node node, NodeTrigger trigger, int tickIndex)
        {
            Node = node;
            Trigger = trigger;
            TickIndex = tickIndex;
        }
    }

    public sealed class GraphTraceCollector
    {
        private readonly List<GraphTraceEvent> _events = new();

        public IReadOnlyList<GraphTraceEvent> Events => _events;

        internal void Record(GraphTraceEvent traceEvent)
        {
            _events.Add(traceEvent);
        }

        public void Clear()
        {
            _events.Clear();
        }
    }
}
