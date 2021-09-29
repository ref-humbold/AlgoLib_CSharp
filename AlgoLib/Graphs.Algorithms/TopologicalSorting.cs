using System;
using System.Collections.Generic;
using System.Linq;
using Algolib.Structures;

namespace Algolib.Graphs.Algorithms
{
    public static class TopologicalSorting
    {
        public static IEnumerable<Vertex<VertexId>> SortUsingInputs<VertexId, VertexProperty, EdgeProperty>(
            IDirectedGraph<VertexId, VertexProperty, EdgeProperty> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            List<Vertex<VertexId>> order = new List<Vertex<VertexId>>();
            Dictionary<Vertex<VertexId>, int> inputDegrees = new Dictionary<Vertex<VertexId>, int>();
            Heap<Vertex<VertexId>> vertexHeap = new Heap<Vertex<VertexId>>();

            foreach(Vertex<VertexId> vertex in graph.Vertices)
            {
                int degree = graph.GetInputDegree(vertex);

                inputDegrees.Add(vertex, degree);

                if(degree == 0)
                    vertexHeap.Push(vertex);
            }

            while(vertexHeap.Count > 0)
            {
                Vertex<VertexId> vertex = vertexHeap.Pop();

                order.Add(vertex);
                inputDegrees.Remove(vertex);

                foreach(Vertex<VertexId> neighbour in graph.GetNeighbours(vertex))
                {
                    --inputDegrees[neighbour];

                    if(inputDegrees[neighbour] == 0)
                        vertexHeap.Push(neighbour);
                }
            }

            return order.Count == graph.VerticesCount ? order : throw new DirectedCyclicGraphException("Given graph contains a cycle");
        }

        public static IEnumerable<Vertex<VertexId>> SortUsingDfs<VertexId, VertexProperty, EdgeProperty>(
            IDirectedGraph<VertexId, VertexProperty, EdgeProperty> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            TopologicalStrategy<VertexId> strategy = new TopologicalStrategy<VertexId>();
            Searching.DfsRecursive(graph, strategy, graph.Vertices);

            return strategy.order.Reverse<Vertex<VertexId>>();
        }

        private class TopologicalStrategy<VertexId> : IDfsStrategy<VertexId>
        {
            internal List<Vertex<VertexId>> order = new List<Vertex<VertexId>>();

            public void ForRoot(Vertex<VertexId> root)
            {
            }

            public void OnEdgeToVisited(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
            {
            }

            public void OnEntry(Vertex<VertexId> vertex)
            {
            }

            public void OnExit(Vertex<VertexId> vertex) => order.Add(vertex);

            public void OnNextVertex(Vertex<VertexId> vertex, Vertex<VertexId> neighbour) =>
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
