using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class StronglyConnectedComponents
    {
        /// <summary>Finds strongly connected components in given directed graph.</summary>
        /// <param name="graph">The directed graph.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>List of vertices in strongly connected components.</returns>
        public static List<HashSet<Vertex<TVertexId>>> FindScc<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var postOrderStrategy = new PostOrderStrategy<TVertexId>();

            Searching.DfsRecursive(graph, postOrderStrategy, graph.Vertices);

            var vertices = postOrderStrategy.PostTimes
                                            .OrderByDescending(kv => kv.Value)
                                            .Select(kv => kv.Key)
                                            .ToList();
            IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> reversedGraph = graph.ReversedCopy();
            var sccStrategy = new SCCStrategy<TVertexId>();

            Searching.DfsRecursive(reversedGraph, sccStrategy, vertices);
            return sccStrategy.Components;
        }

        private class PostOrderStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            private int timer = 0;

            public Dictionary<Vertex<TVertexId>, int> PostTimes { get; } = new();

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
                PostTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }
        }

        private class SCCStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            public List<HashSet<Vertex<TVertexId>>> Components { get; } = new();

            public void ForRoot(Vertex<TVertexId> root) =>
                Components.Add(new HashSet<Vertex<TVertexId>>());

            public void OnEntry(Vertex<TVertexId> vertex) => Components[^1].Add(vertex);

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
