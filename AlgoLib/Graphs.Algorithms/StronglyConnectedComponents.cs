using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class StronglyConnectedComponents
    {
        /// <summary>Finds strongly connected components in given directed graph.</summary>
        /// <param name="graph">A directed graph</param>
        /// <returns>List of vertices in strongly connected components</returns>
        public static List<HashSet<Vertex<TVertexId>>> FindScc<TVertexId, TVertexProperty, TEdgeProperty>(
                this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var postOrderStrategy = new PostOrderStrategy<TVertexId>();

            Searching.DfsRecursive(graph, postOrderStrategy, graph.Vertices);

            var vertices = postOrderStrategy.postTimes
                                            .OrderByDescending(kv => kv.Value)
                                            .Select(kv => kv.Key)
                                            .ToList();
            IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> reversedGraph = graph.ReversedCopy();
            var sccStrategy = new SCCStrategy<TVertexId>();

            Searching.DfsRecursive(reversedGraph, sccStrategy, vertices);
            return sccStrategy.components;
        }

        private class PostOrderStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            internal readonly Dictionary<Vertex<TVertexId>, int> postTimes =
                new Dictionary<Vertex<TVertexId>, int>();

            private int timer = 0;

            public void ForRoot(Vertex<TVertexId> root)
            {
            }

            public void OnEntry(Vertex<TVertexId> vertex)
            {
            }

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }

            public void OnExit(Vertex<TVertexId> vertex)
            {
                postTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }
        }

        private class SCCStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            internal readonly List<HashSet<Vertex<TVertexId>>> components =
                new List<HashSet<Vertex<TVertexId>>>();

            public void ForRoot(Vertex<TVertexId> root) =>
                components.Add(new HashSet<Vertex<TVertexId>>());

            public void OnEntry(Vertex<TVertexId> vertex) => components[^1].Add(vertex);

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }

            public void OnExit(Vertex<TVertexId> vertex)
            {
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }
        }
    }
}
