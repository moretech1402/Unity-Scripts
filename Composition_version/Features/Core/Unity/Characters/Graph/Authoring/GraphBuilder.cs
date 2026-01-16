using System;
using MC.Core.Characters.Graph.Nodes;
using MC.Core.Characters.Graph.Runtime;

namespace MC.Core.Unity.Characters.Graph.Authoring
{
    public static class GraphBuilder
    {
        public static GraphRuntime Build(CharacterGraphSO definition)
        {
            if (definition == null)
                throw new ArgumentNullException(nameof(definition));

            var context = new GraphContext();

            // 1. Build & register nodes explicitly listed
            foreach (var nodeSO in definition.Nodes)
            {
                if (nodeSO == null)
                    continue;

                context.RegisterNode(nodeSO.Build(context));
            }

            // 2. Build connections (auto-register endpoints)
            foreach (var conn in definition.Connections)
            {
                if (conn == null)
                    continue;

                var source = GetOrBuildNode(conn.source, context);
                var target = GetOrBuildNode(conn.target, context);

                context.Register(new Connection(
                    source,
                    conn.trigger,
                    target
                ));
            }

            return new GraphRuntime(context);
        }

        private static Node GetOrBuildNode(NodeSO so, GraphContext context)
        {
            if (so == null)
                throw new ArgumentNullException(nameof(so));

            if (context.TryGetNode<Node>(so.Id, out var node))
                return node;

            node = so.Build(context);
            context.RegisterNode(node);
            return node;
        }

    }
}
