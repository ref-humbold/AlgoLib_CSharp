// Graph searching algorithms.
using System.Linq;
using System.Collections.Generic;
using Algolib.Graphs.Searching;

namespace Algolib.Graphs
{
    public class BreadthFirstSearch
    {
        /// <summary>BFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertex</param>
        public static List<bool> BFS(IGraph graph, ISearchingStrategy strategy, params int[] roots)
        {
            List<int> reached = Enumerable.Repeat(0, graph.VerticesCount).ToList();
            Queue<int> vertexQueue = new Queue<int>();
            int iter = 1;

            foreach(int root in roots)
                if(reached[root] == 0)
                {
                    vertexQueue.Enqueue(root);
                    reached[root] = iter;

                    while(vertexQueue.Count != 0)
                    {
                        int vertex = vertexQueue.Dequeue();

                        strategy.Preprocess(vertex);

                        foreach(int neighbour in graph.GetNeighbours(vertex))
                            if(reached[neighbour] == 0)
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                reached[neighbour] = iter;
                                vertexQueue.Enqueue(neighbour);
                            }
                            else if(reached[neighbour] == iter)
                                strategy.OnCycle(vertex, neighbour);

                        strategy.Postprocess(vertex);
                        reached[root] = -iter;
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
        /// <param name="roots">starting vertex</param>
        public static List<bool> DFSI(IGraph graph, ISearchingStrategy strategy, params int[] roots)
        {
            List<int> reached = Enumerable.Repeat(0, graph.VerticesCount).ToList();
            Stack<int> vertexStack = new Stack<int>();
            int iter = 1;


            foreach(int root in roots)
                if(reached[root] == 0)
                {
                    vertexStack.Push(root);
                    reached[root] = iter;

                    while(vertexStack.Count != 0)
                    {
                        int vertex = vertexStack.Pop();

                        reached[vertex] = iter;
                        strategy.Preprocess(vertex);

                        foreach(int neighbour in graph.GetNeighbours(vertex))
                            if(reached[neighbour] == 0)
                            {
                                strategy.ForNeighbour(vertex, neighbour);
                                vertexStack.Push(neighbour);
                            }
                            else if(reached[neighbour] == iter)
                                strategy.OnCycle(vertex, neighbour);

                        strategy.Postprocess(vertex);
                        reached[root] = -iter;
                    }

                    ++iter;
                }

            return reached.Select(i => i != 0).ToList();
        }

        /// <summary>Recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="roots">starting vertex</param>
        public static List<bool> DFSR(IGraph graph, ISearchingStrategy strategy, params int[] roots)
        {
            DfsrState state = new DfsrState(graph.VerticesCount);

            foreach(int root in roots)
                if(state.reached[root] != 0)
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
        private static void dfsrStep(IGraph graph, ISearchingStrategy strategy, int vertex,
                                     DfsrState state)
        {
            state.OnEntry(vertex);
            strategy.Preprocess(vertex);

            foreach(int neighbour in graph.GetNeighbours(vertex))
                if(state.reached[neighbour] == 0)
                {
                    strategy.ForNeighbour(vertex, neighbour);
                    dfsrStep(graph, strategy, neighbour, state);
                }
                else if(state.reached[neighbour] == state.Iteration)
                    strategy.OnCycle(vertex, neighbour);

            strategy.Postprocess(vertex);
            state.OnExit(vertex);
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
