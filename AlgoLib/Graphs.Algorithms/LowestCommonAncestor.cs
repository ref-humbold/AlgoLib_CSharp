// Algorithm for lowest common ancestors in a rooted tree.
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public sealed class LowestCommonAncestor<TVertexId, TVertexProperty, TEdgeProperty>
    {
        private readonly Dictionary<Vertex<TVertexId>, List<Vertex<TVertexId>>> paths = new();
        private readonly LcaStrategy strategy = new();
        private bool empty = true;

        public TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> Graph { get; }

        public Vertex<TVertexId> Root { get; }

        public LowestCommonAncestor(
            TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, Vertex<TVertexId> root)
        {
            Graph = graph;
            Root = root;
        }

        /// <summary>Searches for lowest common ancestor of given vertices in this rooted tree.</summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        /// <returns>The lowest common ancestor of the vertices.</returns>
        public Vertex<TVertexId> FindLca(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if(empty)
                initialize();

            return find(vertex1, vertex2);
        }

        private Vertex<TVertexId> find(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if(isOffspring(vertex1, vertex2))
                return vertex2;

            if(isOffspring(vertex2, vertex1))
                return vertex1;

            var candidates = paths[vertex1].Reverse<Vertex<TVertexId>>()
                                           .Where(candidate => !isOffspring(vertex2, candidate))
                                           .ToList();

            return candidates.Count > 0
                ? find(candidates[0], vertex2)
                : find(paths[vertex1][0], vertex2);
        }

        private void initialize()
        {
            Searching.DfsRecursive(Graph, strategy, new List<Vertex<TVertexId>> { Root });

            foreach(Vertex<TVertexId> vertex in Graph.Vertices)
                paths[vertex] = new List<Vertex<TVertexId>> { strategy.Parents[vertex] };

            for(int i = 0; i < Math.Log2(Graph.VerticesCount) + 3; ++i)
                foreach(Vertex<TVertexId> vertex in Graph.Vertices)
                    paths[vertex].Add(paths[paths[vertex][i]][i]);

            empty = false;
        }

        private bool isOffspring(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2) =>
            strategy.PreTimes[vertex1] >= strategy.PreTimes[vertex2]
                && strategy.PostTimes[vertex1] <= strategy.PostTimes[vertex2];

        private class LcaStrategy : IDfsStrategy<TVertexId>
        {
            private int timer = 0;

            public Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Parents { get; } = new();

            public Dictionary<Vertex<TVertexId>, int> PreTimes { get; } = new();

            public Dictionary<Vertex<TVertexId>, int> PostTimes { get; } = new();

            public void ForRoot(Vertex<TVertexId> root) => Parents[root] = root;

            public void OnEntry(Vertex<TVertexId> vertex)
            {
                PreTimes[vertex] = timer;
                ++timer;
            }

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour) =>
                Parents[neighbour] = vertex;

            public void OnExit(Vertex<TVertexId> vertex)
            {
                PostTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }
        }
    }
}
