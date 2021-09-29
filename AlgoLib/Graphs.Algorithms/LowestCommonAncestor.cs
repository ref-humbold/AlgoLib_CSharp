using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public sealed class LowestCommonAncestor<VertexId, VertexProperty, EdgeProperty>
    {
        private readonly Dictionary<Vertex<VertexId>, List<Vertex<VertexId>>> paths =
            new Dictionary<Vertex<VertexId>, List<Vertex<VertexId>>>();

        private readonly LcaStrategy strategy = new LcaStrategy();
        private bool empty = true;

        public TreeGraph<VertexId, VertexProperty, EdgeProperty> Graph
        {
            get;
        }

        public Vertex<VertexId> Root
        {
            get;
        }

        public LowestCommonAncestor(TreeGraph<VertexId, VertexProperty, EdgeProperty> graph,
                                    Vertex<VertexId> root)
        {
            Graph = graph;
            Root = root;
        }

        public Vertex<VertexId> Find(Vertex<VertexId> vertex1, Vertex<VertexId> vertex2)
        {
            if(empty)
                initialize();

            return doFind(vertex1, vertex2);
        }

        /// <summary>Finds a lowest common ancestor of two vertices in a rooted tree.</summary>
        /// <param name="vertex1">first vertex</param>
        /// <param name="vertex2">second vertex</param>
        /// <returns>lowest common ancestor of given vertices</returns>
        private Vertex<VertexId> doFind(Vertex<VertexId> vertex1, Vertex<VertexId> vertex2)
        {
            if(isOffspring(vertex1, vertex2))
                return vertex2;

            if(isOffspring(vertex2, vertex1))
                return vertex1;

            List<Vertex<VertexId>> candidates =
                paths[vertex1].Reverse<Vertex<VertexId>>()
                              .Where(candidate => !isOffspring(vertex2, candidate))
                              .ToList();

            return candidates.Count > 0 ? doFind(candidates[0], vertex2)
                                        : doFind(paths[vertex1][0], vertex2);
        }

        private void initialize()
        {
            Searching.DfsRecursive(Graph, strategy, new List<Vertex<VertexId>> { Root });

            foreach(Vertex<VertexId> vertex in Graph.Vertices)
                paths[vertex] = new List<Vertex<VertexId>> { strategy.parents[vertex] };

            for(int i = 0; i < Math.Log2(Graph.VerticesCount) + 3; ++i)
                foreach(Vertex<VertexId> vertex in Graph.Vertices)
                    paths[vertex].Add(paths[paths[vertex][i]][i]);

            empty = false;
        }

        private bool isOffspring(Vertex<VertexId> vertex1, Vertex<VertexId> vertex2) =>
            strategy.preTimes[vertex1] >= strategy.preTimes[vertex2]
                && strategy.postTimes[vertex1] <= strategy.postTimes[vertex2];

        private class LcaStrategy : IDfsStrategy<VertexId>
        {
            internal readonly Dictionary<Vertex<VertexId>, Vertex<VertexId>> parents =
                new Dictionary<Vertex<VertexId>, Vertex<VertexId>>();

            internal readonly Dictionary<Vertex<VertexId>, int> preTimes =
                new Dictionary<Vertex<VertexId>, int>();

            internal readonly Dictionary<Vertex<VertexId>, int> postTimes =
                new Dictionary<Vertex<VertexId>, int>();

            private int timer = 0;

            public void ForRoot(Vertex<VertexId> root) => parents[root] = root;

            public void OnEntry(Vertex<VertexId> vertex)
            {
                preTimes[vertex] = timer;
                ++timer;
            }

            public void OnNextVertex(Vertex<VertexId> vertex, Vertex<VertexId> neighbour) => parents[neighbour] = vertex;

            public void OnExit(Vertex<VertexId> vertex)
            {
                postTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
            {
            }
        }
    }
}
