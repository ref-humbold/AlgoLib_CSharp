// Graphs structures.
using System;
using System.Linq;
using System.Collections.Generic;
using Algolib.Graphs.Properties;

namespace Algolib.Graphs
{
    public class Vertex<T> where T : IVertexProperties
    {
        public readonly int Id;
        public readonly T Properties;

        public Vertex(int number, T properties)
        {
            this.Id = number;
            this.Properties = properties;
        }

        public bool Equals(Vertex<T> v)
        {
            return Id == v.Id;
        }
    }

    public class Edge<T, V> where T : IEdgeProperties where V : IVertexProperties
    {
        public readonly Vertex<V> From;
        public readonly Vertex<V> To;
        public readonly T Properties;

        public Edge(Vertex<V> from, Vertex<V> to, T properties)
        {
            this.From = from;
            this.To = to;
            this.Properties = properties;
        }
        public bool Equals(Edge<T, V> e)
        {
            return From.Equals(e.From) && To.Equals(e.To);
        }
    }

    public interface IGraph<V, E> where V : IVertexProperties where E : IEdgeProperties
    {
        /// <summary>Oznaczenie nieskończoności</summary>
        double Inf { get; }

        /// <summary>Liczba wierzchołków</summary>
        int VerticesCount { get; }

        /// <summary>Liczba krawędzi</summary>
        int EdgesCount { get; }

        /// <summary>Lista wierzchołków</summary>
        IEnumerable<Vertex<V>> Vertices { get; }

        /// <summary>Lista krawędzi</summary>
        IEnumerable<Edge<E, V>> Edges { get; }

        /// <summary>Dodawanie nowego wierzchołka</summary>
        /// <param name="properties">właściwości wierzchołka</param>
        /// <returns>nowy wierzchołek</returns>
        Vertex<V> AddVertex(V properties);

        /// <summary>Dodawanie nowej krawędzi</summary>
        /// <param name="from">początkowy wierzchołek</param>
        /// <param name="to">końcowy wierzchołek</param>
        /// <param name="properties">właściwości krawędzi</param>
        /// <returns>nowa krawędź</returns>
        Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E properties);

        /// <param name="v">wierzchołek</param>
        /// <returns>lista sąsiadów wierzchołka</returns>
        IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> v);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>stopień wyjściowy wierzchołka</returns>
        int GetOutdegree(Vertex<V> v);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>stopień wejściowy wierzchołka</returns>
        int GetIndegree(Vertex<V> v);
    }

    public abstract class SimpleGraph<V, E> : IGraph<V, E> where V : IVertexProperties
                                                           where E : IEdgeProperties
    {
        /// <summary>Lista sąsiedztwa grafu</summary>
        protected Dictionary<Vertex<V>, HashSet<Edge<E, V>>> Graphrepr;

        public SimpleGraph(int n, V vertexProperties)
        {
            Graphrepr = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            for(int i = 0; i < n; ++i)
                AddVertex(vertexProperties);
        }

        public double Inf => double.PositiveInfinity;

        public int VerticesCount => Graphrepr.Count;

        public abstract int EdgesCount { get; }

        public IEnumerable<Vertex<V>> Vertices => Graphrepr.Keys;

        public abstract IEnumerable<Edge<E, V>> Edges { get; }

        public Vertex<V> AddVertex(V properties)
        {
            Vertex<V> vertex = new Vertex<V>(Graphrepr.Count, properties);

            Graphrepr.Add(vertex, new HashSet<Edge<E, V>>());

            return vertex;
        }

        public abstract Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E properties);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> v) => Graphrepr[v].Select(e => e.To);

        public int GetOutdegree(Vertex<V> v) => Graphrepr[v].Count;

        public abstract int GetIndegree(Vertex<V> v);
    }
}
