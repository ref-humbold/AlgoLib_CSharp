// Graphs structures.
using System;
using System.Linq;
using System.Collections.Generic;
using Algolib.Graphs.Properties;
using System.Diagnostics.CodeAnalysis;

namespace Algolib.Graphs
{
    public sealed class Vertex<V> : IComparable<Vertex<V>> where V : IVertexProperties
    {
        public readonly int Id;
        public readonly V Properties;

        internal Vertex(int number, V properties)
        {
            this.Id = number;
            this.Properties = properties;
        }

        public int CompareTo([AllowNull] Vertex<V> other) => this.Id.CompareTo(other?.Id);

        public bool Equals(Vertex<V> v) => this.Id == v.Id;

        public override int GetHashCode() => this.Id.GetHashCode();
    }

    public sealed class Edge<E, V> : IComparable<Edge<E, V>>
        where E : IEdgeProperties where V : IVertexProperties
    {
        public readonly Vertex<V> From;
        public readonly Vertex<V> To;
        public readonly E Properties;

        internal Edge(Vertex<V> from, Vertex<V> to, E properties)
        {
            this.From = from;
            this.To = to;
            this.Properties = properties;
        }

        public int CompareTo([AllowNull] Edge<E, V> other)
        {
            int firstComp = this.From.CompareTo(other?.From);

            return firstComp != 0 ? firstComp : this.To.CompareTo(other?.To);
        }

        public bool Equals(Edge<E, V> e) => this.From.Equals(e.From) && this.To.Equals(e.To);

        public override int GetHashCode() => Tuple.Create(From.Id, To.Id).GetHashCode();
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

        public SimpleGraph(IEnumerable<V> properties)
        {
            Graphrepr = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            foreach(var prop in properties)
                AddVertex(prop);
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
