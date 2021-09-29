// Algorithms for graph searching
using System.Collections.Generic;

namespace Algolib.Graphs.Algorithms
{
    public static class Searching
    {
        /// <summary>Breadth-first-search algorithm.</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<Vertex<VertexId>> Bfs<VertexId, VertexProperty, EdgeProperty>(
            IGraph<VertexId, VertexProperty, EdgeProperty> graph, IBfsStrategy<VertexId> strategy,
            IEnumerable<Vertex<VertexId>> roots)
        {
            HashSet<Vertex<VertexId>> reached = new HashSet<Vertex<VertexId>>();
            Queue<Vertex<VertexId>> vertexQueue = new Queue<Vertex<VertexId>>();

            foreach(Vertex<VertexId> root in roots)
                if(!reached.Contains(root))
                {
                    strategy.ForRoot(root);
                    vertexQueue.Enqueue(root);
                    reached.Add(root);

                    while(vertexQueue.Count != 0)
                    {
                        Vertex<VertexId> vertex = vertexQueue.Dequeue();

                        strategy.OnEntry(vertex);

                        foreach(Vertex<VertexId> neighbour in graph.GetNeighbours(vertex))
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
        public static IEnumerable<Vertex<VertexId>> DfsIterative<VertexId, VertexProperty, EdgeProperty>(
            IGraph<VertexId, VertexProperty, EdgeProperty> graph, IDfsStrategy<VertexId> strategy,
            IEnumerable<Vertex<VertexId>> roots)
        {
            Dictionary<Vertex<VertexId>, int> reached = new Dictionary<Vertex<VertexId>, int>();
            Stack<Vertex<VertexId>> vertexStack = new Stack<Vertex<VertexId>>();
            int iteration = 1;

            foreach(Vertex<VertexId> root in roots)
                if(!reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    vertexStack.Push(root);

                    while(vertexStack.Count != 0)
                    {
                        Vertex<VertexId> vertex = vertexStack.Pop();

                        if(!reached.ContainsKey(vertex))
                        {
                            reached[vertex] = iteration;
                            strategy.OnEntry(vertex);

                            foreach(Vertex<VertexId> neighbour in graph.GetNeighbours(vertex))
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

        /// <summary>Recursive deph-first-search algorithm.</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<Vertex<VertexId>> DfsRecursive<VertexId, VertexProperty, EdgeProperty>(
            IGraph<VertexId, VertexProperty, EdgeProperty> graph, IDfsStrategy<VertexId> strategy,
            IEnumerable<Vertex<VertexId>> roots)
        {
            DfsRecursiveState<VertexId> state = new DfsRecursiveState<VertexId>();

            foreach(Vertex<VertexId> root in roots)
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
        private static void dfsRecursiveStep<VertexId, VertexProperty, EdgeProperty>(
            IGraph<VertexId, VertexProperty, EdgeProperty> graph, IDfsStrategy<VertexId> strategy,
            DfsRecursiveState<VertexId> state)
        {
            Vertex<VertexId> vertex = state.vertex;
            state.OnEntry(vertex);
            strategy.OnEntry(vertex);

            foreach(Vertex<VertexId> neighbour in graph.GetNeighbours(vertex))
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

        private class DfsRecursiveState<VertexId>
        {
            internal readonly Dictionary<Vertex<VertexId>, int> reached = new Dictionary<Vertex<VertexId>, int>();
            internal Vertex<VertexId> vertex;
            internal int iteration = 1;

            internal void OnEntry(Vertex<VertexId> vertex) => reached[vertex] = iteration;

            internal void OnExit(Vertex<VertexId> vertex) => reached[vertex] = -iteration;
        }
    }
}
