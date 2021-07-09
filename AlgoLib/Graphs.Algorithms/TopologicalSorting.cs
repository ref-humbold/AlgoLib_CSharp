using System;
using System.Collections.Generic;
using System.Linq;
using Algolib.Structures;

namespace Algolib.Graphs.Algorithms
{
    [Serializable]
    public class DirectedCyclicGraphException : Exception
    {
        public DirectedCyclicGraphException(string message) : base(message)
        {
        }
    }

    public sealed class TopologicalSorting
    {
        public static IEnumerable<V> SortUsingInputs<V, VP, EP>(IDirectedGraph<V, VP, EP> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            List<V> order = new List<V>();
            Dictionary<V, int> inputDegrees = new Dictionary<V, int>();
            Heap<V> vertexHeap = new Heap<V>();

            foreach(V vertex in graph.Vertices)
            {
                int degree = graph.GetInputDegree(vertex);

                inputDegrees.Add(vertex, degree);

                if(degree == 0)
                    vertexHeap.Push(vertex);
            }

            while(vertexHeap.Count > 0)
            {
                V vertex = vertexHeap.Pop();

                order.Add(vertex);
                inputDegrees.Remove(vertex);

                foreach(V neighbour in graph.GetNeighbours(vertex))
                {
                    --inputDegrees[neighbour];

                    if(inputDegrees[neighbour] == 0)
                        vertexHeap.Push(neighbour);
                }
            }

            if(order.Count != graph.VerticesCount)
                throw new DirectedCyclicGraphException("Given graph contains a cycle");

            return order;
        }

        public static IEnumerable<V> SortUsingDfs<V, VP, EP>(IDirectedGraph<V, VP, EP> graph)
        {
            if(graph.EdgesCount == 0)
                return graph.Vertices;

            TopologicalStrategy<V> strategy = new TopologicalStrategy<V>();
            Searching.DfsRecursive(graph, strategy, graph.Vertices);

            return strategy.order.Reverse<V>();
        }

        private class TopologicalStrategy<V> : IDfsStrategy<V>
        {
            internal List<V> order = new List<V>();

            public void ForRoot(V root)
            {
            }

            public void OnEdgeToVisited(V vertex, V neighbour)
            {
            }

            public void OnEntry(V vertex)
            {
            }

            public void OnExit(V vertex)
            {
                order.Add(vertex);
            }

            public void OnNextVertex(V vertex, V neighbour)
            {
                throw new DirectedCyclicGraphException("Given graph contains a cycle");
            }
        }
    }
}
