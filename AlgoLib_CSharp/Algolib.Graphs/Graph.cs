// Basic graph structures
using System;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IGraph<V, E>
    {
        /// <summary>Infinity symbol</summary>
        public double Inf => double.PositiveInfinity;

        /// <summary>Number of vertices</summary>
        public int VerticesCount { get; }

        /// <summary>Number of edges</summary>
        public int EdgesCount { get; }

        /// <summary>List of all vertices</summary>
        public IEnumerable<Vertex<V>> Vertices { get; }

        /// <summary>List of all edges</summary>
        public IEnumerable<Edge<E, V>> Edges { get; }

        /// <param name="vertex">vertex</param>
        /// <returns>list of adjacent edges</returns>
        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>list of neighbouring vertices</returns>
        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>input degree of vertex</returns>
        public int GetOutdegree(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>output degree of vertex</returns>
        public int GetIndegree(Vertex<V> vertex);
    }

    public sealed class Vertex<V> : IComparable<Vertex<V>>
    {
        public readonly int Id;
        public V Property;

        internal Vertex(int number, V property)
        {
            this.Id = number;
            this.Property = property;
        }

        public int CompareTo(Vertex<V> other) => this.Id.CompareTo(other?.Id);

        public bool Equals(Vertex<V> v) => this.Id == v.Id;

        public override int GetHashCode() => this.Id.GetHashCode();
    }

    public sealed class Edge<E, V> : IComparable<Edge<E, V>>
    {
        public readonly Vertex<V> From;
        public readonly Vertex<V> To;
        public E Property;

        internal Edge(Vertex<V> from, Vertex<V> to, E property)
        {
            this.From = from;
            this.To = to;
            this.Property = property;
        }

        public Edge<E, V> Reversed
        {
            get { return new Edge<E, V>(To, From, Property); }
        }

        public int CompareTo(Edge<E, V> other)
        {
            int firstComp = this.From.CompareTo(other?.From);

            return firstComp != 0 ? firstComp : this.To.CompareTo(other?.To);
        }

        public bool Equals(Edge<E, V> e) => this.From.Equals(e.From) && this.To.Equals(e.To);

        public override int GetHashCode() => Tuple.Create(From.Id, To.Id).GetHashCode();
    }
}
