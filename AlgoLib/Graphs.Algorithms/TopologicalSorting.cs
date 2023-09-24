using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Structures;

namespace AlgoLib.Graphs.Algorithms
{
    public static class TopologicalSorting
    {
        /// <summary>Topological sorting algorithm using predecessors counting.</summary>
        /// <typeparam name="TVertexId">The type of the vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">The type of the vertex property.</typeparam>
        /// <typeparam name="TEdgeProperty">The type of the edge property.</typeparam>
        /// <param name="graph">The directed graph.</param>
        /// <returns>The topological order of vertices.</returns>
        /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
        public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph) =>
            InputsTopologicalSort(graph, Comparer<TVertexId>.Default);

        /// <summary>Inputses the topological sort.</summary>
        /// <typeparam name="TVertexId">The type of the vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">The type of the vertex property.</typeparam>
        /// <typeparam name="TEdgeProperty">The type of the edge property.</typeparam>
        /// <param name="graph">The directed graph.</param>
        /// <param name="vertexIdComparison">The comparison function of vertex identifiers.</param>
        /// <returns>The topological order of vertices.</returns>
        /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
        public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            Comparison<TVertexId> vertexIdComparison) =>
            InputsTopologicalSort(graph, Comparer<TVertexId>.Create(vertexIdComparison));

        /// <summary>Inputses the topological sort.</summary>
        /// <typeparam name="TVertexId">The type of the vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">The type of the vertex property.</typeparam>
        /// <typeparam name="TEdgeProperty">The type of the edge property.</typeparam>
        /// <param name="graph">The directed graph.</param>
        /// <param name="vertexIdComparer">The vertex identifier comparer.</param>
        /// <returns>The topological order of vertices.</returns>
        /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
        public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            IComparer<TVertexId> vertexIdComparer)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            var order = new List<Vertex<TVertexId>>();
            var inputDegrees = new Dictionary<Vertex<TVertexId>, int>();
            var vertexHeap = new Heap<Vertex<TVertexId>>(
                    (vertex1, vertex2) => vertexIdComparer.Compare(vertex1.Id, vertex2.Id));

            foreach(Vertex<TVertexId> vertex in graph.Vertices)
            {
                int degree = graph.GetInputDegree(vertex);

                inputDegrees.Add(vertex, degree);

                if(degree == 0)
                    vertexHeap.Push(vertex);
            }

            while(vertexHeap.Count > 0)
            {
                Vertex<TVertexId> vertex = vertexHeap.Pop();

                order.Add(vertex);
                inputDegrees.Remove(vertex);

                foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
                {
                    --inputDegrees[neighbour];

                    if(inputDegrees[neighbour] == 0)
                        vertexHeap.Push(neighbour);
                }
            }

            return order.Count == graph.VerticesCount
                ? order
                : throw new DirectedCyclicGraphException("Given graph contains a cycle");
        }

        public static IEnumerable<Vertex<TVertexId>> DfsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            var strategy = new TopologicalStrategy<TVertexId>();

            graph.DfsRecursive(strategy, graph.Vertices);
            return strategy.Order.Reverse<Vertex<TVertexId>>();
        }

        private class TopologicalStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            public List<Vertex<TVertexId>> Order { get; set; } = new();

            public void ForRoot(Vertex<TVertexId> root)
            {
            }

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }

            public void OnEntry(Vertex<TVertexId> vertex)
            {
            }

            public void OnExit(Vertex<TVertexId> vertex) => Order.Add(vertex);

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour) =>
                throw new DirectedCyclicGraphException("Given graph contains a cycle");
        }
    }
}
