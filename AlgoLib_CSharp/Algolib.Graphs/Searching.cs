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
            List<int> reached = Enumerable.Repeat(0, graph.VerticesNumber).ToList();
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
            List<int> reached = Enumerable.Repeat(0, graph.VerticesNumber).ToList();
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
                                vertexStack.Push(neighbour);
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
            List<int> reached = Enumerable.Repeat(0, graph.VerticesNumber).ToList();
            int iter = 1;

            foreach(int root in roots)
                if(reached[root] != 0)
                {
                    DFSRstep(graph, strategy, root, iter, ref reached);
                    ++iter;
                }

            return reached.Select(i => i != 0).ToList();
        }

        /// <summary>Step of recursive DFS algorithm</summary>
        /// <param name="graph">graph</param>
        /// <param name="strategy">vertex processing strategy</param>
        /// <param name="vertex">current vertex</param>
        /// <param name="iter">iteration number</param>
        private static void DFSRstep(IGraph graph, ISearchingStrategy strategy, int vertex, 
                                     int iter, ref List<int> reached)
        {
            reached[vertex] = iter;
            strategy.Preprocess(vertex);

            foreach(int neighbour in graph.GetNeighbours(vertex))
                if(reached[neighbour] == 0)
                    DFSRstep(graph, strategy, neighbour, iter, ref reached);
                else if(reached[neighbour] == iter)
                    strategy.OnCycle(vertex, neighbour);

            strategy.Postprocess(vertex);
            reached[vertex] = -iter;
        }
    }
}
