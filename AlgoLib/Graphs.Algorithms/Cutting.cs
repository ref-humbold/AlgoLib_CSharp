using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Cutting
    {
        /// <summary>Finds edge cut of given graph.</summary>
        /// <param name="graph">An undirected graph.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>Edges in the edge cut.</returns>
        public static IEnumerable<Edge<TVertexId>> FindEdgeCut<TVertexId, TVertexProperty, TEdgeProperty>(
            this IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var strategy = new CuttingStrategy<TVertexId>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices
                        .Where(vertex => strategy.hasBridge(vertex))
                        .Select(vertex => graph[vertex, strategy.DfsParents[vertex]]);
        }

        /// <summary>Finds vertex cut of given graph.</summary>
        /// <param name="graph">An undirected graph.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
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
            private readonly Dictionary<Vertex<TVertexId>, List<Vertex<TVertexId>>> dfsChildren = new();
            private readonly Dictionary<Vertex<TVertexId>, int> dfsDepths = new();
            private readonly Dictionary<Vertex<TVertexId>, int> lowValues = new();
            private int depth = 0;

            public Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> DfsParents { get; } = new();

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
                DfsParents[neighbour] = vertex;
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
                if(!neighbour.Equals(DfsParents[vertex]))
                    lowValues[vertex] = Math.Min(lowValues[vertex], dfsDepths[neighbour]);
            }

            public bool hasBridge(Vertex<TVertexId> vertex) =>
                !isDfsRoot(vertex) && lowValues[vertex] == dfsDepths[vertex];

            public bool isSeparator(Vertex<TVertexId> vertex) =>
                isDfsRoot(vertex)
                    ? dfsChildren[vertex].Count > 1
                    : dfsChildren[vertex].Any(child => lowValues[child] >= dfsDepths[vertex]);

            public bool isDfsRoot(Vertex<TVertexId> vertex) => dfsDepths[vertex] == 0;
        }
    }
}
