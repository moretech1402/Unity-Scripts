using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Characters.Graph.Runtime
{
    public sealed class GraphRuntime
    {
        private int _tickCounter;
        private readonly GraphContext _context;

        public GraphContext Context => _context;

        public GraphRuntime(GraphTraceCollector trace = null)
        {
            _context = new GraphContext(trace);
        }

        public void Tick()
        {
            _context.BeginTick(++_tickCounter);

            while (_context.TryDequeue(out var node))
            {
                _context.Trace?.Record(
                    new GraphTraceEvent(node, NodeTrigger.OnExecuted, _context.TickIndex)
                );

                node.Process(_context);
                node.Emit(NodeTrigger.OnExecuted, _context);
            }
        }
    }
}
