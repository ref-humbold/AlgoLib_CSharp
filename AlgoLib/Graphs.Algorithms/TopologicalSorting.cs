using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Structures;

namespace AlgoLib.Graphs.Algorithms
{
    public static class TopologicalSorting
    {
        public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph) =>
            InputsTopologicalSort(graph, Comparer<TVertexId>.Default);

        public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
            Comparison<TVertexId> vertexIdComparison) =>
            InputsTopologicalSort(graph, Comparer<TVertexId>.Create(vertexIdComparison));

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
