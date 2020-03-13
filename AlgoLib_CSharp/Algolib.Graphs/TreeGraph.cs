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

        public double Inf
        {
            get { return double.PositiveInfinity; }
        }
        public int VerticesCount
        {
            get { return Graph.VerticesCount; }
        }
        public int EdgesCount
        {
            get { return Graph.EdgesCount; }
        }
        public IEnumerable<Vertex<V>> Vertices
        {
            get { return Graph.Vertices; }
        }
        public IEnumerable<Edge<E, V>> Edges
        {
            get { return Graph.Edges; }
        }

        public Vertex<V> AddVertex(V vertexProperty, Vertex<V> neighbour, E edgeProperty)
        {
            Vertex<V> vertex = Graph.AddVertex(vertexProperty);

            Graph.AddEdge(vertex, neighbour, edgeProperty);
            return vertex;
        }

        public int GetIndegree(Vertex<V> vertex)
        {
            return Graph.GetIndegree(vertex);
        }
        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex)
        {
            return Graph.GetNeighbours(vertex);
        }

        public int GetOutdegree(Vertex<V> vertex)
        {
            return Graph.GetOutdegree(vertex);
        }
    }
}
