// Algorithms for graph searching
using System.Collections.Generic;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Searching
    {
        /// <summary>Breadth-first-search algorithm.</summary>
        /// <param name="graph">a graph.</param>
        /// <param name="strategy">a searching strategy.</param>
        /// <param name="roots">starting vertices.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>enumerable of visited vertices.</returns>
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
        /// <param name="graph">a graph.</param>
        /// <param name="strategy">a searching strategy.</param>
        /// <param name="roots">starting vertices.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>enumerable of visited vertices.</returns>
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
        /// <param name="graph">a graph.</param>
        /// <param name="strategy">a searching strategy.</param>
        /// <param name="roots">starting vertices.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>enumerable of visited vertices.</returns>
        public static IEnumerable<Vertex<TVertexId>> DfsRecursive<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            IDfsStrategy<TVertexId> strategy,
            IEnumerable<Vertex<TVertexId>> roots)
        {
            var state = new DfsRecursiveState<TVertexId>();

            foreach(Vertex<TVertexId> root in roots)
                if(!state.Reached.ContainsKey(root))
                {
                    strategy.ForRoot(root);
                    state.Vertex = root;
                    graph.dfsRecursiveStep(strategy, state);
                    ++state.Iteration;
                }

            return state.Reached.Keys;
        }

        // Single step of recursive DFS.
        private static void dfsRecursiveStep<TVertexId, TVertexProperty, TEdgeProperty>(
            this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            IDfsStrategy<TVertexId> strategy,
            DfsRecursiveState<TVertexId> state)
        {
            Vertex<TVertexId> vertex = state.Vertex;
            state.onEntry(vertex);
            strategy.OnEntry(vertex);

            foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
                if(!state.Reached.ContainsKey(neighbour))
                {
                    strategy.OnNextVertex(vertex, neighbour);
                    state.Vertex = neighbour;
                    graph.dfsRecursiveStep(strategy, state);
                }
                else if(state.Reached[neighbour] == state.Iteration)
                    strategy.OnEdgeToVisited(vertex, neighbour);

            strategy.OnExit(vertex);
            state.onExit(vertex);
        }

        private class DfsRecursiveState<TVertexId>
        {
            public Dictionary<Vertex<TVertexId>, int> Reached { get; } = new();

            public Vertex<TVertexId> Vertex { get; set; }

            public int Iteration { get; set; } = 1;

            public void onEntry(Vertex<TVertexId> vertex) => Reached[vertex] = Iteration;

            public void onExit(Vertex<TVertexId> vertex) => Reached[vertex] = -Iteration;
        }
    }
}
