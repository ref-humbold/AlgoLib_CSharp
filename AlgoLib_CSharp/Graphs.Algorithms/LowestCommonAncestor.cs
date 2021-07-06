using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public sealed class LowestCommonAncestor<V, VP, EP>
    {
        private readonly Dictionary<V, List<V>> paths = new Dictionary<V, List<V>>();
        private readonly LcaStrategy strategy = new LcaStrategy();
        private bool empty = true;

        public TreeGraph<V, VP, EP> Graph
        {
            get;
        }

        public V Root
        {
            get;
        }

        public LowestCommonAncestor(TreeGraph<V, VP, EP> graph, V root)
        {
            Graph = graph;
            Root = root;
        }

        public V Find(V vertex1, V vertex2)
        {
            if(empty)
                initialize();

            return doFind(vertex1, vertex2);
        }

        /// <summary>Finds a lowest common ancestor of two vertices in a rooted tree.</summary>
        /// <param name="vertex1">first vertex</param>
        /// <param name="vertex2">second vertex</param>
        /// <returns>lowest common ancestor of given vertices</returns>
        private V doFind(V vertex1, V vertex2)
        {
            if(isOffspring(vertex1, vertex2))
                return vertex2;

            if(isOffspring(vertex2, vertex1))
                return vertex1;

            List<V> candidates = paths[vertex1].Reverse<V>()
                                               .Where(candidate => !isOffspring(vertex2, candidate))
                                               .ToList();

            return candidates.Count > 0 ? doFind(candidates[0], vertex2)
                                        : doFind(paths[vertex1][0], vertex2);
        }

        private void initialize()
        {
            Searching.DfsRecursive(Graph, strategy, new List<V> { Root });

            foreach(V vertex in Graph.Vertices)
                paths[vertex] = new List<V> { strategy.parents[vertex] };

            for(int i = 0; i < Math.Log2(Graph.VerticesCount) + 3; ++i)
                foreach(V vertex in Graph.Vertices)
                    paths[vertex].Add(paths[paths[vertex][i]][i]);

            empty = false;
        }

        private bool isOffspring(V vertex1, V vertex2) =>
            strategy.preTimes[vertex1] >= strategy.preTimes[vertex2]
                    && strategy.postTimes[vertex1] <= strategy.postTimes[vertex2];

        private class LcaStrategy : IDfsStrategy<V>
        {
            internal readonly Dictionary<V, V> parents = new Dictionary<V, V>();
            internal readonly Dictionary<V, int> preTimes = new Dictionary<V, int>();
            internal readonly Dictionary<V, int> postTimes = new Dictionary<V, int>();
            private int timer = 0;

            public void ForRoot(V root) => parents[root] = root;

            public void OnEntry(V vertex)
            {
                preTimes[vertex] = timer;
                ++timer;
            }

            public void OnNextVertex(V vertex, V neighbour) => parents[neighbour] = vertex;

            public void OnExit(V vertex)
            {
                postTimes[vertex] = timer;
                ++timer;
            }

            public void OnEdgeToVisited(V vertex, V neighbour)
            {
            }
        }
    }
}
