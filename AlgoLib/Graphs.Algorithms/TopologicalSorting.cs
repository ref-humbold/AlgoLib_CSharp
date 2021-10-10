using System;
using System.Collections.Generic;
using System.Linq;
using Algolib.Structures;

namespace Algolib.Graphs.Algorithms
{
    public static class TopologicalSorting
    {
        public static IEnumerable<Vertex<TVertexId>> SortUsingInputs<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            List<Vertex<TVertexId>> order = new List<Vertex<TVertexId>>();
            Dictionary<Vertex<TVertexId>, int> inputDegrees = new Dictionary<Vertex<TVertexId>, int>();
            Heap<Vertex<TVertexId>> vertexHeap = new Heap<Vertex<TVertexId>>();

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

            return order.Count == graph.VerticesCount ? order : throw new DirectedCyclicGraphException("Given graph contains a cycle");
        }

        public static IEnumerable<Vertex<TVertexId>> SortUsingDfs<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            TopologicalStrategy<TVertexId> strategy = new TopologicalStrategy<TVertexId>();
            Searching.DfsRecursive(graph, strategy, graph.Vertices);

            return strategy.order.Reverse<Vertex<TVertexId>>();
        }

        private class TopologicalStrategy<TVertexId> : IDfsStrategy<TVertexId>
        {
            internal List<Vertex<TVertexId>> order = new List<Vertex<TVertexId>>();

            public void ForRoot(Vertex<TVertexId> root)
            {
            }

            public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
            {
            }

            public void OnEntry(Vertex<TVertexId> vertex)
            {
            }

            public void OnExit(Vertex<TVertexId> vertex) => order.Add(vertex);

            public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour) =>
                throw new DirectedCyclicGraphException("Given graph contains a cycle");
        }
    }

    [Serializable]
    public class DirectedCyclicGraphException : Exception
    {
        public DirectedCyclicGraphException(string message) : base(message)
        {
        }
    }
}
