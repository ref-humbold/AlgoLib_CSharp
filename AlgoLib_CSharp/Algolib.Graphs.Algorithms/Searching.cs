// Algorithms for graph searching
using System.Collections.Generic;

namespace Algolib.Graphs.Algorithms
{
    public class BreadthFirstSearch
    {
        /// <summary>BFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static IEnumerable<Vertex<V>> BFS<V, E>(IGraph<V, E> graph,
                                                       IBFSStrategy<V> strategy,
                                                       IEnumerable<Vertex<V>> roots)
        {
            Dictionary<Vertex<V>, int> reached = new Dictionary<Vertex<V>, int>();
            Queue<Vertex<V>> vertexQueue = new Queue<Vertex<V>>();
            int iteration = 1;

            foreach(Vertex<V> root in roots)
                if(!reached.ContainsKey(root))
                {
                    vertexQueue.Enqueue(root);
                    reached[root] = iteration;

                    while(vertexQueue.Count != 0)
                    {
                        Vertex<V> vertex = vertexQueue.Dequeue();

                        strategy.OnEnter(vertex);

                        foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                            if(!reached.ContainsKey(neighbour))
                            {
                                strategy.OnNextVertex(vertex, neighbour);
                                reached[neighbour] = iteration;
                                vertexQueue.Enqueue(neighbour);
                            }

                        strategy.OnExit(vertex);
                        reached[root] = -iteration;
                    }

                    ++iteration;
                }

            return reached.Keys;
        }
    }

    public class DepthFirstSearch
    {
        /// <summary>Iterative DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static IEnumerable<Vertex<V>> DFSIterative<V, E>(
            IGraph<V, E> graph, IDFSStrategy<V> strategy, IEnumerable<Vertex<V>> roots)
        {
            Dictionary<Vertex<V>, int> reached = new Dictionary<Vertex<V>, int>();
            Stack<Vertex<V>> vertexStack = new Stack<Vertex<V>>();
            int iteration = 1;

            foreach(Vertex<V> root in roots)
                if(!reached.ContainsKey(root))
                {
                    vertexStack.Push(root);
                    reached[root] = iteration;

                    while(vertexStack.Count != 0)
                    {
                        Vertex<V> vertex = vertexStack.Pop();

                        reached[vertex] = iteration;
                        strategy.OnEnter(vertex);

                        foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                            if(!reached.ContainsKey(neighbour))
                            {
                                strategy.OnNextVertex(vertex, neighbour);
                                vertexStack.Push(neighbour);
                            }
                            else
                                strategy.OnEdgeToVisited(vertex, neighbour);

                        strategy.OnExit(vertex);
                        reached[root] = -iteration;
                    }

                    ++iteration;
                }

            return reached.Keys;
        }

        /// <summary>Recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static IEnumerable<Vertex<V>> DFSRecursive<V, E>(
            IGraph<V, E> graph, IDFSStrategy<V> strategy, IEnumerable<Vertex<V>> roots)
        {
            DFSRecursiveState<V> state = new DFSRecursiveState<V>();

            foreach(Vertex<V> root in roots)
                if(state.reached[root] != 0)
                {
                    state.vertex = root;
                    dfsRecursiveStep(graph, strategy, state);
                    state.AddIteration();
                }

            return state.reached.Keys;
        }

        /// <summary>Step of recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="state">current searching state</param>
        private static void dfsRecursiveStep<V, E>(IGraph<V, E> graph, IDFSStrategy<V> strategy,
                                                   DFSRecursiveState<V> state)
        {
            Vertex<V> vertex = state.vertex;
            state.OnEntry(vertex);
            strategy.OnEnter(vertex);

            foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                if(state.reached[neighbour] == 0)
                {
                    strategy.OnNextVertex(vertex, neighbour);
                    state.vertex = neighbour;
                    dfsRecursiveStep(graph, strategy, state);
                }
                else
                    strategy.OnEdgeToVisited(vertex, neighbour);

            strategy.OnExit(vertex);
            state.OnExit(vertex);
        }

        private class DFSRecursiveState<V>
        {
            internal readonly Dictionary<Vertex<V>, int> reached = new Dictionary<Vertex<V>, int>();
            internal Vertex<V> vertex;
            internal int iteration = 1;

            internal void AddIteration() => ++iteration;

            internal void OnEntry(Vertex<V> vertex) => reached[vertex] = iteration;

            internal void OnExit(Vertex<V> vertex) => reached[vertex] = -iteration;
        }
    }
}
