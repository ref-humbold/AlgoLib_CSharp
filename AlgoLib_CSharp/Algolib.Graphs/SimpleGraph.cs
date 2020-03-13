// Simple graph structure
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public abstract class SimpleGraph<V, E> : IGraph<V, E>
    {
        /// <summary>Adjacency list of graph</summary>
        protected Dictionary<Vertex<V>, HashSet<Edge<E, V>>> Graphrepr;

        public SimpleGraph(IEnumerable<V> properties)
        {
            Graphrepr = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            foreach (var prop in properties)
                AddVertex(prop);
        }

        public double Inf => double.PositiveInfinity;

        public int VerticesCount => Graphrepr.Count;

        public abstract int EdgesCount { get; }

        public IEnumerable<Vertex<V>> Vertices => Graphrepr.Keys;

        public abstract IEnumerable<Edge<E, V>> Edges { get; }

        /// <summary>Adds new vertex with given property</summary>
        /// <param name="property">property of new vertex</param>
        /// <returns>new vertex</returns>
        public Vertex<V> AddVertex(V property)
        {
            Vertex<V> vertex = new Vertex<V>(Graphrepr.Count, property);

            Graphrepr.Add(vertex, new HashSet<Edge<E, V>>());

            return vertex;
        }

        /// <summary>Adds new edge with given property</summary>
        /// <param name="from">beginning vertex</param>
        /// <param name="to">ending vertex</param>
        /// <param name="property">property of new edge</param>
        /// <returns>new edge</returns>
        public abstract Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E property);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> v) => Graphrepr[v].Select(e => e.To);

        public int GetOutdegree(Vertex<V> v) => Graphrepr[v].Count;

        public abstract int GetIndegree(Vertex<V> v);
    }
}
