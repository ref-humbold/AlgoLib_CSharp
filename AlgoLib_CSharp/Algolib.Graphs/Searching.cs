// Graph searching algorithms.
using System.Linq;
using System.Collections.Generic;
using Algolib.Graphs.Searching;
using Algolib.Graphs.Properties;

namespace Algolib.Graphs
{
    public class BreadthFirstSearch
    {
        /// <summary>BFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static List<bool> BFS<V, E>(IGraph<V, E> graph, ISearchingStrategy<V> strategy,
                                           IEnumerable<Vertex<V>> roots)
            where V : IVertexProperties where E : IEdgeProperties
        {
            List<int> reached = Enumerable.Repeat(0, graph.VerticesCount).ToList();
            Queue<Vertex<V>> vertexQueue = new Queue<Vertex<V>>();
            int iter = 1;

            foreach(var root in roots)
                if(reached[root.Id] == 0)
                {
                    vertexQueue.Enqueue(root);
                    reached[root.Id] = iter;

                    while(vertexQueue.Count != 0)
                    {
                        Vertex<V> vertex = vertexQueue.Dequeue();

                        strategy.Preprocess(vertex);

                        foreach(var neighbour in graph.GetNeighbours(vertex))
                            if(reached[neighbour.Id] == 0)
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                reached[neighbour.Id] = iter;
                                vertexQueue.Enqueue(neighbour);
                            }
                            else if(reached[neighbour.Id] == iter)
                                strategy.OnCycle(vertex, neighbour);

                        strategy.Postprocess(vertex);
                        reached[root.Id] = -iter;
                    }

                    ++iter;
                }

            return reached.Select(i => i != 0).ToList();
        }
    }

    public class DepthFirstSearch
    {
        /// <summary>Iterative DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static List<bool> DFSI<V, E>(IGraph<V, E> graph, ISearchingStrategy<V> strategy,
                                            IEnumerable<Vertex<V>> roots)
            where V : IVertexProperties where E : IEdgeProperties
        {
            List<int> reached = Enumerable.Repeat(0, graph.VerticesCount).ToList();
            Stack<Vertex<V>> vertexStack = new Stack<Vertex<V>>();
            int iter = 1;

            foreach(var root in roots)
                if(reached[root.Id] == 0)
                {
                    vertexStack.Push(root);
                    reached[root.Id] = iter;

                    while(vertexStack.Count != 0)
                    {
                        Vertex<V> vertex = vertexStack.Pop();

                        reached[vertex.Id] = iter;
                        strategy.Preprocess(vertex);

                        foreach(var neighbour in graph.GetNeighbours(vertex))
                            if(reached[neighbour.Id] == 0)
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                vertexStack.Push(neighbour);
                            }
                            else if(reached[neighbour.Id] == iter)
                                strategy.OnCycle(vertex, neighbour);

                        strategy.Postprocess(vertex);
                        reached[root.Id] = -iter;
                    }

                    ++iter;
                }

            return reached.Select(i => i != 0).ToList();
        }

        /// <summary>Recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertices</param>
        public static List<bool> DFSR<V, E>(IGraph<V, E> graph, ISearchingStrategy<V> strategy,
                                            IEnumerable<Vertex<V>> roots)
            where V : IVertexProperties where E : IEdgeProperties
        {
            DfsrState state = new DfsrState(graph.VerticesCount);

            foreach(var root in roots)
                if(state.reached[root.Id] != 0)
                {
                    dfsrStep(graph, strategy, root, state);
                    state.AddIteration();
                }

            return state.reached.Select(i => i != 0).ToList();
        }

        /// <summary>Step of recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="vertex">current vertex</param>
        /// <param name="state">current searching state</param>
        private static void dfsrStep<V, E>(IGraph<V, E> graph, ISearchingStrategy<V> strategy,
                                           Vertex<V> vertex, DfsrState state)
            where V : IVertexProperties where E : IEdgeProperties
        {
            state.OnEntry(vertex.Id);
            strategy.Preprocess(vertex);

            foreach(var neighbour in graph.GetNeighbours(vertex))
                if(state.reached[neighbour.Id] == 0)
                {
                    strategy.ForNeighbour(vertex, neighbour);
                    dfsrStep(graph, strategy, neighbour, state);
                }
                else if(state.reached[neighbour.Id] == state.Iteration)
                    strategy.OnCycle(vertex, neighbour);

            strategy.Postprocess(vertex);
            state.OnExit(vertex.Id);
        }

        private class DfsrState
        {
            internal int Iteration { get; private set; }
            internal List<int> reached;

            internal DfsrState(int verticesNumber)
            {
                Iteration = 1;
                reached = Enumerable.Repeat(0, verticesNumber).ToList();
            }

            internal void AddIteration()
            {
                ++Iteration;
            }

            internal void OnEntry(int vertex)
            {
                reached[vertex] = Iteration;
            }

            internal void OnExit(int vertex)
            {
                reached[vertex] = -Iteration;
            }
        }
    }
}
