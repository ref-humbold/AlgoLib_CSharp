// Algorithms for graph searching
using System.Collections.Generic;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Searching
    {
        /// <summary>Breadth-first-search algorithm.</summary>
        /// <param name="graph">a graph</param>
        /// <param name="strategy">a searching strategy</param>
        /// <param name="roots">starting vertices</param>
        /// <returns>enumerable of visited vertices</returns>
        public static IEnumerable<Vertex<TVertexId>> Bfs<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            IBfsStrategy<TVertexId> strategy,
            IEnumerable<Vertex<TVertexId>> roots)
        {
            var reached = new HashSet<Vertex<TVertexId>>();
            var vertexQueue = new Queue<Vertex<TVertexId>>();

            foreach(Vertex<TVertexId> root in roots)
                if(!reached.Contains(root))
                {
                    strategy.ForRoot(root);
                    vertexQueue.Enqueue(root);
                    reached.Add(root);

                    while(vertexQueue.Count != 0)
                    {
                        Vertex<TVertexId> vertex = vertexQueue.Dequeue();

                        strategy.OnEntry(vertex);

                        foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
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
        public static IEnumerable<Vertex<TVertexId>> DfsIterative<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            IDfsStrategy<TVertexId> strategy,
            IEnumerable<Vertex<TVertexId>> roots)
        {
            var reached = new Dictionary<Vertex<TVertexId>, int>();
            var vertexStack = new Stack<Vertex<TVertexId>>();
            int iteration = 1;

            foreach(Vertex<TVertexId> root in roots)
                if(!reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    vertexStack.Push(root);

                    while(vertexStack.Count != 0)
                    {
                        Vertex<TVertexId> vertex = vertexStack.Pop();

                        if(!reached.ContainsKey(vertex))
                        {
                            reached[vertex] = iteration;
                            strategy.OnEntry(vertex);

                            foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
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
        public static IEnumerable<Vertex<TVertexId>> DfsRecursive<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, IDfsStrategy<TVertexId> strategy,
            IEnumerable<Vertex<TVertexId>> roots)
        {
            var state = new DfsRecursiveState<TVertexId>();

            foreach(Vertex<TVertexId> root in roots)
                if(!state.reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    state.vertex = root;
                    graph.dfsRecursiveStep(strategy, state);
                    ++state.iteration;
                }

            return state.reached.Keys;
        }

        // Single step of recursive DFS.
        private static void dfsRecursiveStep<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, IDfsStrategy<TVertexId> strategy,
            DfsRecursiveState<TVertexId> state)
        {
            Vertex<TVertexId> vertex = state.vertex;
            state.onEntry(vertex);
            strategy.OnEntry(vertex);

            foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
                if(!state.reached.ContainsKey(neighbour))
                {
                    strategy.OnNextVertex(vertex, neighbour);
                    state.vertex = neighbour;
                    graph.dfsRecursiveStep(strategy, state);
                }
                else if(state.reached[neighbour] == state.iteration)
                    strategy.OnEdgeToVisited(vertex, neighbour);

            strategy.OnExit(vertex);
            state.onExit(vertex);
        }

        private class DfsRecursiveState<TVertexId>
        {
            internal readonly Dictionary<Vertex<TVertexId>, int> reached = new Dictionary<Vertex<TVertexId>, int>();
            internal Vertex<TVertexId> vertex;
            internal int iteration = 1;

            internal void onEntry(Vertex<TVertexId> vertex) => reached[vertex] = iteration;

            internal void onExit(Vertex<TVertexId> vertex) => reached[vertex] = -iteration;
        }
    }
}
