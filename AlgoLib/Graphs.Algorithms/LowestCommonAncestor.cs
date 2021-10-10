using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public sealed class LowestCommonAncestor<TVertexId, TVertexProperty, TEdgeProperty>
    {
        private readonly Dictionary<Vertex<TVertexId>, List<Vertex<TVertexId>>> paths =
            new Dictionary<Vertex<TVertexId>, List<Vertex<TVertexId>>>();

        private readonly LcaStrategy strategy = new LcaStrategy();
        private bool empty = true;

        public TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> Graph
        {
            get;
        }

        public Vertex<TVertexId> Root
        {
            get;
        }

        public LowestCommonAncestor(TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
                                    Vertex<TVertexId> root)
        {
            Graph = graph;
            Root = root;
        }

        public Vertex<TVertexId> Find(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if(empty)
                initialize();

            return doFind(vertex1, vertex2);
        }

        /// <summary>Finds a lowest common ancestor of two vertices in a rooted tree.</summary>
        /// <param name="vertex1">first vertex</param>
        /// <param name="vertex2">second vertex</param>
        /// <returns>lowest common ancestor of given vertices</returns>
        private Vertex<TVertexId> doFind(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if(isOffspring(vertex1, vertex2))
                return vertex2;

            if(isOffspring(vertex2, vertex1))
                return vertex1;

            List<Vertex<TVertexId>> candidates =
                paths[vertex1].Reverse<Vertex<TVertexId>>()
                              .Where(candidate => !isOffspring(vertex2, candidate))
                              .ToList();

            return candidates.Count > 0 ? doFind(candidates[0], vertex2)
                                        : doFind(paths[vertex1][0], vertex2);
        }

        private void initialize()
        {
            Searching.DfsRecursive(Graph, strategy, new List<Vertex<TVertexId>> { Root });

            foreach(Vertex<TVertexId> vertex in Graph.Vertices)
                paths[vertex] = new List<Vertex<TVertexId>> { strategy.parents[vertex] };

            for(int i = 0; i < Math.Log2(Graph.VerticesCount) + 3; ++i)
                foreach(Vertex<TVertexId> vertex in Graph.Vertices)
                    paths[vertex].Add(paths[paths[vertex][i]][i]);

            empty = false;
        }

        private bool isOffspring(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2) =>
            strategy.preTimes[vertex1] >= strategy.preTimes[vertex2]
                && strategy.postTimes[vertex1] <= strategy.postTimes[vertex2];

        private class LcaStrategy : IDfsStrategy<TVertexId>
        {
            internal readonly Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> parents =
                new Dictionary<Vertex<TVertexId>, Vertex<TVertexId>>();

            internal readonly Dictionary<Vertex<TVertexId>, int> preTimes =
                new Dictionary<Vertex<TVertexId>, int>();

            internal readonly Dictionary<Vertex<TVertexId>, int> postTimes =
                new Dictionary<Vertex<TVertexId>, int>();

            private int timer = 0;

            public void ForRoot(Vertex<TVertexId> root) => parents[root] = root;

            public void OnEntry(Vertex<TVertexId> vertex)
            {
                preTimes[vertex] = timer;
                ++timer;
            }

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour) => parents[neighbour] = vertex;

            public void OnExit(Vertex<TVertexId> vertex)
            {
                postTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }
        }
    }
}
