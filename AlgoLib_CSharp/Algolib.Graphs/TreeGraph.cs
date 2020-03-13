using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    class TreeGraph<V, E> : IUndirectedGraph<V, E>
    {
        protected UndirectedSimpleGraph<V, E> Graph;

        public TreeGraph(V property)
        {
            Graph = new UndirectedSimpleGraph<V, E>(Enumerable.Repeat(property, 1));
        }

        public double Inf => double.PositiveInfinity;

        public int VerticesCount => Graph.VerticesCount;

        public int EdgesCount => Graph.EdgesCount;

        public IEnumerable<Vertex<V>> Vertices => Graph.Vertices;
        
        public IEnumerable<Edge<E, V>> Edges => Graph.Edges;

        public Vertex<V> AddVertex(V vertexProperty, Vertex<V> neighbour, E edgeProperty)
        {
            Vertex<V> vertex = Graph.AddVertex(vertexProperty);

            Graph.AddEdge(vertex, neighbour, edgeProperty);
            return vertex;
        }

        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex) => Graph.GetAdjacentEdges(vertex);

        public int GetIndegree(Vertex<V> vertex) => Graph.GetIndegree(vertex);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex) => Graph.GetNeighbours(vertex);

        public int GetOutdegree(Vertex<V> vertex) => Graph.GetOutdegree(vertex);
    }
}
