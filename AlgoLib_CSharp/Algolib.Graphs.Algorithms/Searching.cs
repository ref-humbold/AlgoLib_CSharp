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
                                                       ISearchingStrategy<V> strategy,
                                                       IEnumerable<Vertex<V>> roots)
        {
            Dictionary<Vertex<V>, int> reached = new Dictionary<Vertex<V>, int>();
            Queue<Vertex<V>> vertexQueue = new Queue<Vertex<V>>();
            int iter = 1;

            foreach(Vertex<V> root in roots)
                if(!reached.ContainsKey(root))
                {
                    vertexQueue.Enqueue(root);
                    reached[root] = iter;

                    while(vertexQueue.Count != 0)
                    {
                        Vertex<V> vertex = vertexQueue.Dequeue();

                        strategy.PreProcess(vertex);

                        foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                            if(!reached.ContainsKey(neighbour))
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                reached[neighbour] = iter;
                                vertexQueue.Enqueue(neighbour);
                            }

                        strategy.PostProcess(vertex);
                        reached[root] = -iter;
                    }

                    ++iter;
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
            IGraph<V, E> graph, ISearchingStrategy<V> strategy, IEnumerable<Vertex<V>> roots)
        {
            Dictionary<Vertex<V>, int> reached = new Dictionary<Vertex<V>, int>();
            Stack<Vertex<V>> vertexStack = new Stack<Vertex<V>>();
            int iter = 1;

            foreach(Vertex<V> root in roots)
                if(!reached.ContainsKey(root))
                {
                    vertexStack.Push(root);
                    reached[root] = iter;

                    while(vertexStack.Count != 0)
                    {
                        Vertex<V> vertex = vertexStack.Pop();

                        reached[vertex] = iter;
                        strategy.PreProcess(vertex);

                        foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                            if(!reached.ContainsKey(neighbour))
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                vertexStack.Push(neighbour);
                            }

                        strategy.PostProcess(vertex);
                        reached[root] = -iter;
                    }

                    ++iter;
                }

            return reached.Keys;
        }

        /// <summary>Recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static IEnumerable<Vertex<V>> DFSRecursive<V, E>(
            IGraph<V, E> graph, ISearchingStrategy<V> strategy, IEnumerable<Vertex<V>> roots)
        {
            DfsrState<V> state = new DfsrState<V>();

            foreach(Vertex<V> root in roots)
                if(state.reached[root] != 0)
                {
                    dfsStep(graph, strategy, root, state);
                    state.AddIteration();
                }

            return state.reached.Keys;
        }

        /// <summary>Step of recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="vertex">current vertex</param>
        /// <param name="state">current searching state</param>
        private static void dfsStep<V, E>(IGraph<V, E> graph, ISearchingStrategy<V> strategy,
                                          Vertex<V> vertex, DfsrState<V> state)
        {
            state.OnEntry(vertex);
            strategy.PreProcess(vertex);

            foreach(Vertex<V> neighbour in graph.GetNeighbours(vertex))
                if(state.reached[neighbour] == 0)
                {
                    strategy.ForNeighbour(vertex, neighbour);
                    dfsStep(graph, strategy, neighbour, state);
                }

            strategy.PostProcess(vertex);
            state.OnExit(vertex);
        }

        private class DfsrState<V>
        {
            internal Dictionary<Vertex<V>, int> reached;

            internal int Iteration { get; private set; }

            internal DfsrState()
            {
                Iteration = 1;
            }

            internal void AddIteration() => ++Iteration;

            internal void OnEntry(Vertex<V> vertex) => reached[vertex] = Iteration;

            internal void OnExit(Vertex<V> vertex) => reached[vertex] = -Iteration;
        }
    }
}
