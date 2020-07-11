// Algorithms for graph searching
using System.Collections.Generic;

namespace Algolib.Graphs.Algorithms
{
    public sealed class Searching
    {
        /// <summary>Breadth-first-search algorithm.</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<V> Bfs<V, VP, EP>(
            IGraph<V, VP, EP> graph, IBfsStrategy<V> strategy, IEnumerable<V> roots)
        {
            HashSet<V> reached = new HashSet<V>();
            Queue<V> vertexQueue = new Queue<V>();

            foreach(V root in roots)
                if(!reached.Contains(root))
                {
                    strategy.ForRoot(root);
                    vertexQueue.Enqueue(root);
                    reached.Add(root);

                    while(vertexQueue.Count != 0)
                    {
                        V vertex = vertexQueue.Dequeue();

                        strategy.OnEnter(vertex);

                        foreach(V neighbour in graph.GetNeighbours(vertex))
                            if(!reached.Contains(neighbour))
                            {
                                strategy.OnNextVertex(vertex, neighbour);
                                reached.Add(neighbour);
                                vertexQueue.Enqueue(neighbour);
                            }

                        strategy.OnExit(vertex);
                    }
                }

            return reached;
        }

        /// <summary>Iterative deph-first-search algorithm.</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<V> DfsIterative<V, VP, EP>(
            IGraph<V, VP, EP> graph, IDfsStrategy<V> strategy, IEnumerable<V> roots)
        {
            Dictionary<V, int> reached = new Dictionary<V, int>();
            Stack<V> vertexStack = new Stack<V>();
            int iteration = 1;

            foreach(V root in roots)
                if(!reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    vertexStack.Push(root);

                    while(vertexStack.Count != 0)
                    {
                        V vertex = vertexStack.Pop();

                        if(!reached.ContainsKey(vertex))
                        {
                            reached[vertex] = iteration;
                            strategy.OnEnter(vertex);

                            foreach(V neighbour in graph.GetNeighbours(vertex))
                                if(!reached.ContainsKey(neighbour))
                                {
                                    strategy.OnNextVertex(vertex, neighbour);
                                    vertexStack.Push(neighbour);
                                }
                                else if(reached[neighbour] == iteration)
                                    strategy.OnEdgeToVisited(vertex, neighbour);

                            strategy.OnExit(vertex);
                            reached[root] = -iteration;
                        }
                    }

                    ++iteration;
                }

            return reached.Keys;
        }

        /// <summary>Recursive deph-first-search algorithm</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<V> DfsRecursive<V, VP, EP>(
            IGraph<V, VP, EP> graph, IDfsStrategy<V> strategy, IEnumerable<V> roots)
        {
            DfsRecursiveState<V> state = new DfsRecursiveState<V>();

            foreach(V root in roots)
                if(!state.reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    state.vertex = root;
                    dfsRecursiveStep(graph, strategy, state);
                    ++state.iteration;
                }

            return state.reached.Keys;
        }

        // Single step of recursive DFS.
        private static void dfsRecursiveStep<V, VP, EP>(
            IGraph<V, VP, EP> graph, IDfsStrategy<V> strategy, DfsRecursiveState<V> state)
        {
            V vertex = state.vertex;
            state.OnEntry(vertex);
            strategy.OnEnter(vertex);

            foreach(V neighbour in graph.GetNeighbours(vertex))
                if(!state.reached.ContainsKey(neighbour))
                {
                    strategy.OnNextVertex(vertex, neighbour);
                    state.vertex = neighbour;
                    dfsRecursiveStep(graph, strategy, state);
                }
                else if(state.reached[neighbour] == state.iteration)
                    strategy.OnEdgeToVisited(vertex, neighbour);

            strategy.OnExit(vertex);
            state.OnExit(vertex);
        }

        private class DfsRecursiveState<V>
        {
            internal readonly Dictionary<V, int> reached = new Dictionary<V, int>();
            internal V vertex;
            internal int iteration = 1;

            internal void OnEntry(V vertex) => reached[vertex] = iteration;

            internal void OnExit(V vertex) => reached[vertex] = -iteration;
        }
    }
}
