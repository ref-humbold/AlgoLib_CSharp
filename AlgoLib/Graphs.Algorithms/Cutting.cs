using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public static class Cutting
    {
        /// <summary>Finds an edge cut of given graph.</summary>
        /// <param name="graph">an undirected graph</param>
        /// <returns>edges in the edge cut</returns>
        public static IEnumerable<Edge<VertexId>> FindEdgeCut<VertexId, VertexProperty, EdgeProperty>(
            IUndirectedGraph<VertexId, VertexProperty, EdgeProperty> graph)
        {
            CuttingStrategy<VertexId> strategy = new CuttingStrategy<VertexId>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices
                        .Where(vertex => strategy.HasBridge(vertex))
                        .Select(vertex => graph[vertex, strategy.dfsParents[vertex]]);
        }

        /// <summary>Finds a vertex cut of given graph.</summary>
        /// <param name="graph">an undirected graph</param>
        /// <returns>vertices in the vertex cut</returns>
        public static IEnumerable<Vertex<VertexId>> FindVertexCut<VertexId, VertexProperty, EdgeProperty>(
            IUndirectedGraph<VertexId, VertexProperty, EdgeProperty> graph)
        {
            CuttingStrategy<VertexId> strategy = new CuttingStrategy<VertexId>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices.Where(vertex => strategy.IsSeparator(vertex));
        }

        private class CuttingStrategy<VertexId> : IDfsStrategy<VertexId>
        {
            internal readonly Dictionary<Vertex<VertexId>, Vertex<VertexId>> dfsParents =
                new Dictionary<Vertex<VertexId>, Vertex<VertexId>>();

            internal readonly Dictionary<Vertex<VertexId>, List<Vertex<VertexId>>> dfsChildren =
                new Dictionary<Vertex<VertexId>, List<Vertex<VertexId>>>();

            internal readonly Dictionary<Vertex<VertexId>, int> dfsDepths = new Dictionary<Vertex<VertexId>, int>();
            internal readonly Dictionary<Vertex<VertexId>, int> lowValues = new Dictionary<Vertex<VertexId>, int>();
            private int depth = 0;

            public void ForRoot(Vertex<VertexId> root)
            {
            }

            public void OnEntry(Vertex<VertexId> vertex)
            {
                dfsDepths[vertex] = depth;
                lowValues[vertex] = depth;
                dfsChildren.TryAdd(vertex, new List<Vertex<VertexId>>());
                ++depth;
            }

            public void OnNextVertex(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
            {
                dfsParents[neighbour] = vertex;
                dfsChildren[vertex].Add(neighbour);
            }

            public void OnExit(Vertex<VertexId> vertex)
            {
                int? childLowValue = dfsChildren[vertex].Min(child => (int?)lowValues[child]);
                int minimalLowValue = childLowValue ?? int.MaxValue;

                lowValues[vertex] = Math.Min(lowValues[vertex], minimalLowValue);
                --depth;
            }

            public void OnEdgeToVisited(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
            {
                if(!neighbour.Equals(dfsParents[vertex]))
                    lowValues[vertex] = Math.Min(lowValues[vertex], dfsDepths[neighbour]);
            }

            internal bool HasBridge(Vertex<VertexId> vertex) =>
                !IsDfsRoot(vertex) && lowValues[vertex] == dfsDepths[vertex];

            internal bool IsSeparator(Vertex<VertexId> vertex) =>
                IsDfsRoot(vertex) ? dfsChildren[vertex].Count > 1
                                  : dfsChildren[vertex].Any(
                                          child => lowValues[child] >= dfsDepths[vertex]);

            internal bool IsDfsRoot(Vertex<VertexId> vertex) => dfsDepths[vertex] == 0;
        }
    }
}
