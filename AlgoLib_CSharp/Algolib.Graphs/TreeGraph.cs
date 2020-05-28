// Structure of tree graph
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public class TreeGraph<V, E> : IUndirectedGraph<V, E>
    {
        protected UndirectedSimpleGraph<V, E> Graph;

        public int VerticesCount => Graph.VerticesCount;

        public int EdgesCount => Graph.EdgesCount;

        public IEnumerable<Vertex<V>> Vertices => Graph.Vertices;

        public IEnumerable<Edge<E, V>> Edges => Graph.Edges;

        public TreeGraph(V property)
        {
            Graph = new UndirectedSimpleGraph<V, E>(Enumerable.Repeat(property, 1));
        }

        public Vertex<V> AddVertex(V vertexProperty, E edgeProperty, Vertex<V> neighbour)
        {
            Vertex<V> vertex = Graph.AddVertex(vertexProperty);

            Graph.AddEdge(vertex, neighbour, edgeProperty);
            return vertex;
        }

        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex) => Graph.GetAdjacentEdges(vertex);

        public int GetInputDegree(Vertex<V> vertex) => Graph.GetInputDegree(vertex);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex) => Graph.GetNeighbours(vertex);

        public int GetOutputDegree(Vertex<V> vertex) => Graph.GetOutputDegree(vertex);
    }
}
