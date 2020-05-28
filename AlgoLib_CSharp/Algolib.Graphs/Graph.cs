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

        /// <summary>List of all vertices sorted by index</summary>
        public IList<Vertex<V>> Vertices { get; }

        /// <summary>List of all edges sorted first by source then by destination</summary>
        public IList<Edge<E, V>> Edges { get; }

        /// <param name="vertex">vertex</param>
        /// <returns>list of adjacent edges</returns>
        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>list of neighbouring vertices</returns>
        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>input degree of vertex</returns>
        public int GetOutputDegree(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>output degree of vertex</returns>
        public int GetInputDegree(Vertex<V> vertex);
    }

    public struct Vertex<V> : IComparable<Vertex<V>>
    {
        public readonly int Id;
        public V Property;

        internal Vertex(int number, V property)
        {
            this.Id = number;
            this.Property = property;
        }

        public int CompareTo(Vertex<V> other) => Id.CompareTo(other.Id);

        public bool Equals(Vertex<V> v) => Id == v.Id;

        public override int GetHashCode() => Id.GetHashCode();
    }

    public struct Edge<E, V> : IComparable<Edge<E, V>>
    {
        public readonly Vertex<V> Source;
        public readonly Vertex<V> Destination;
        public E Property;

        internal Edge(Vertex<V> source, Vertex<V> destination, E property)
        {
            this.Source = source;
            this.Destination = destination;
            this.Property = property;
        }

        /// <summary>Reverses direction of this edge.</summary>
        /// <param name="propertyMapper">
        /// mapping function that creates property of reversed edge using current edge property
        /// </param>
        /// <returns>reversed edge</returns>
        public Edge<E, V> Reverse(Func<E, E> propertyMapper) =>
            new Edge<E, V>(Destination, Source, propertyMapper.Invoke(Property));

        public int CompareTo(Edge<E, V> other)
        {
            int firstComp = Source.CompareTo(other.Source);

            return firstComp != 0 ? firstComp : Destination.CompareTo(other.Destination);
        }

        public bool Equals(Edge<E, V> e) => Source.Equals(e.Source) && Destination.Equals(e.Destination);

        public override int GetHashCode() => Tuple.Create(Source.Id, Destination.Id).GetHashCode();
    }
}
