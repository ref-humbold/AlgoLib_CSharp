using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Cutting
    {
        /// <summary>Finds edge cut of given graph.</summary>
        /// <param name="graph">An undirected graph.</param>
        /// <returns>Edges in the edge cut.</returns>
        public static IEnumerable<Edge<TVertexId>> FindEdgeCut<TVertexId, TVertexProperty, TEdgeProperty>(
            this IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var strategy = new CuttingStrategy<TVertexId>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices
                        .Where(vertex => strategy.hasBridge(vertex))
                        .Select(vertex => graph[vertex, strategy.dfsParents[vertex]]);
        }

        /// <summary>Finds vertex cut of given graph.</summary>
        /// <param name="graph">An undirected graph.</param>
        /// <returns>Vertices in the vertex cut.</returns>
        public static IEnumerable<Vertex<TVertexId>> FindVertexCut<TVertexId, TVertexProperty, TEdgeProperty>(
            this IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var strategy = new CuttingStrategy<TVertexId>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices.Where(vertex => strategy.isSeparator(vertex));
        }

        private class CuttingStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            internal readonly Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> dfsParents = new();
            internal readonly Dictionary<Vertex<TVertexId>, List<Vertex<TVertexId>>> dfsChildren = new();
            internal readonly Dictionary<Vertex<TVertexId>, int> dfsDepths = new();
            internal readonly Dictionary<Vertex<TVertexId>, int> lowValues = new();
            private int depth = 0;

            public void ForRoot(Vertex<TVertexId> root)
            {
            }

            public void OnEntry(Vertex<TVertexId> vertex)
            {
                dfsDepths[vertex] = depth;
                lowValues[vertex] = depth;
                dfsChildren.TryAdd(vertex, new List<Vertex<TVertexId>>());
                ++depth;
            }

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
                dfsParents[neighbour] = vertex;
                dfsChildren[vertex].Add(neighbour);
            }

            public void OnExit(Vertex<TVertexId> vertex)
            {
                int? childLowValue = dfsChildren[vertex].Min(child => (int?)lowValues[child]);
                int minimalLowValue = childLowValue ?? int.MaxValue;

                lowValues[vertex] = Math.Min(lowValues[vertex], minimalLowValue);
                --depth;
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
                if(!neighbour.Equals(dfsParents[vertex]))
                    lowValues[vertex] = Math.Min(lowValues[vertex], dfsDepths[neighbour]);
            }

            internal bool hasBridge(Vertex<TVertexId> vertex) =>
                !isDfsRoot(vertex) && lowValues[vertex] == dfsDepths[vertex];

            internal bool isSeparator(Vertex<TVertexId> vertex) =>
                isDfsRoot(vertex)
                    ? dfsChildren[vertex].Count > 1
                    : dfsChildren[vertex].Any(child => lowValues[child] >= dfsDepths[vertex]);

            internal bool isDfsRoot(Vertex<TVertexId> vertex) => dfsDepths[vertex] == 0;
        }
    }
}
