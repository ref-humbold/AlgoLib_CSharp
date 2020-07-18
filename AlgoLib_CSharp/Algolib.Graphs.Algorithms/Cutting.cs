using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public sealed class Cutting
    {
        /// <summary>Finds an edge cut of given graph.</summary>
        /// <param name="graph">an undirected graph</param>
        /// <returns>edges in the edge cut</returns>
        public static IEnumerable<Edge<V>> FindEdgeCut<V, VP, EP>(IUndirectedGraph<V, VP, EP> graph)
        {
            CuttingStrategy<V> strategy = new CuttingStrategy<V>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices
                        .Where(vertex => strategy.HasBridge(vertex))
                        .Select(vertex => graph.GetEdge(vertex, strategy.dfsParents[vertex]));
        }

        /// <summary>Finds a vertex cut of given graph.</summary>
        /// <param name="graph">an undirected graph</param>
        /// <returns>vertices in the vertex cut</returns>
        public static IEnumerable<V> FindVertexCut<V, VP, EP>(IUndirectedGraph<V, VP, EP> graph)
        {
            CuttingStrategy<V> strategy = new CuttingStrategy<V>();

            Searching.DfsRecursive(graph, strategy, graph.Vertices);
            return graph.Vertices.Where(vertex => strategy.IsSeparator(vertex));
        }

        private class CuttingStrategy<V> : IDfsStrategy<V>
        {
            internal readonly Dictionary<V, V> dfsParents = new Dictionary<V, V>();
            internal readonly Dictionary<V, List<V>> dfsChildren = new Dictionary<V, List<V>>();
            internal readonly Dictionary<V, int> dfsDepths = new Dictionary<V, int>();
            internal readonly Dictionary<V, int> lowValues = new Dictionary<V, int>();
            private int depth = 0;

            public void ForRoot(V root)
            {
            }

            public void OnEntry(V vertex)
            {
                dfsDepths[vertex] = depth;
                lowValues[vertex] = depth;
                dfsChildren.TryAdd(vertex, new List<V>());
                ++depth;
            }

            public void OnNextVertex(V vertex, V neighbour)
            {
                dfsParents[neighbour] = vertex;
                dfsChildren[vertex].Add(neighbour);
            }

            public void OnExit(V vertex)
            {
                int? childLowValue = dfsChildren[vertex].Min(child => (int?)lowValues[child]);
                int minimalLowValue = childLowValue ?? int.MaxValue;

                lowValues[vertex] = Math.Min(lowValues[vertex], minimalLowValue);
                --depth;
            }

            public void OnEdgeToVisited(V vertex, V neighbour)
            {
                if(!neighbour.Equals(dfsParents[vertex]))
                    lowValues[vertex] = Math.Min(lowValues[vertex], dfsDepths[neighbour]);
            }

            internal bool HasBridge(V vertex)
            {
                return !IsDfsRoot(vertex) && lowValues[vertex] == dfsDepths[vertex];
            }

            internal bool IsSeparator(V vertex) =>
                IsDfsRoot(vertex) ? dfsChildren[vertex].Count > 1
                                  : dfsChildren[vertex].Any(
                                          child => lowValues[child] >= dfsDepths[vertex]);

            internal bool IsDfsRoot(V vertex) => dfsDepths[vertex] == 0;
        }
    }
}
