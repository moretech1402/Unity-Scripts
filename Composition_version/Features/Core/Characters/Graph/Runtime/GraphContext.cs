using System;
using System.Collections.Generic;
using MC.Core.Characters.Graph.Nodes;

namespace MC.Core.Characters.Graph.Runtime
{
    public sealed class GraphContext
    {
        private readonly Queue<Node> executionQueue = new();
        private readonly List<ITemporal> _temporals = new();
        private readonly List<Connection> connections = new();
        private readonly Dictionary<Node, int> executionCounts = new();
        private readonly Dictionary<string, Node> nodesById = new();

        private const int MaxExecutionsPerTick = 999;

        public int TickIndex { get; private set; }
        public GraphTraceCollector Trace { get; }

        public GraphContext(GraphTraceCollector trace = null)
        {
            Trace = trace;
        }

        public void RegisterTemporal(ITemporal temporal)
        {
            _temporals.Add(temporal);
        }

        internal void BeginTick(int tickIndex)
        {
            TickIndex = tickIndex;
            executionCounts.Clear();
        }

        public void RegisterNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (nodesById.ContainsKey(node.Id))
                throw new InvalidOperationException(
                    $"Node with id '{node.Id}' already registered."
                );

            nodesById[node.Id] = node;
        }

        public void Register(Connection connection)
        {
            connections.Add(connection);
        }

        public void Enqueue(Node node)
        {
            if (!CanExecute(node))
                return;

            executionQueue.Enqueue(node);
            IncrementExecution(node);
        }

        public void Emit(Node source, NodeTrigger trigger)
        {
            Trace?.Record(new GraphTraceEvent(source, trigger, TickIndex));

            foreach (var connection in connections)
            {
                if (connection.Matches(source, trigger))
                {
                    Enqueue(connection.Target);
                }
            }
        }

        public bool TryDequeue(out Node node)
        {
            return executionQueue.TryDequeue(out node);
        }

        public void Tick()
        {
            int executions = 0;

            for (int i = _temporals.Count - 1; i >= 0; i--)
            {
                if (!_temporals[i].Tick(this))
                {
                    _temporals.RemoveAt(i);
                }
            }

            while (TryDequeue(out var node))
            {
                if (++executions > MaxExecutionsPerTick)
                {
                    throw new InvalidOperationException(
                        "Graph execution exceeded safe limits. Possible infinite loop."
                    );
                }

                node.Process(this);
            }
        }

        private bool CanExecute(Node node)
        {
            return !executionCounts.TryGetValue(node, out var count)
                   || count < MaxExecutionsPerTick;
        }

        private void IncrementExecution(Node node)
        {
            executionCounts[node] = executionCounts.TryGetValue(node, out var count)
                ? count + 1
                : 1;
        }

        public bool TryGetNode<T>(string id, out T node) where T : Node
        {
            if (nodesById.TryGetValue(id, out var raw) && raw is T typed)
            {
                node = typed;
                return true;
            }

            node = null;
            return false;
        }

        public T RequireNode<T>(string id) where T : Node
        {
            if (!TryGetNode<T>(id, out var node))
            {
                throw new InvalidOperationException(
                    $"Required node '{id}' of type {typeof(T).Name} not found."
                );
            }

            return node;
        }

    }
}
