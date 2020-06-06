// Structure of basic graph
using System;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IGraph<V, E>
    {
        /// <summary>number of vertices</summary>
        int VerticesCount { get; }

        /// <summary>number of edges</summary>
        int EdgesCount { get; }

        /// <summary>list of all vertices sorted by index</summary>
        IList<Vertex<V>> Vertices { get; }

        /// <summary>list of all edges sorted first by source then by destination</summary>
        IList<Edge<E, V>> Edges { get; }

        /// <summary>vertex with given index</summary>
        Vertex<V> this[int index] { get; }

        /// <summary>edge between the vertices, or <c>null</c> if no edge</summary>
        Edge<E, V> this[Vertex<V> source, Vertex<V> destination] { get; }

        /// <param name="vertex">vertex</param>
        /// <returns>list of adjacent edges</returns>
        IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>list of neighbouring vertices</returns>
        IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>input degree of vertex</returns>
        int GetOutputDegree(Vertex<V> vertex);

        /// <param name="vertex">vertex</param>
        /// <returns>output degree of vertex</returns>
        int GetInputDegree(Vertex<V> vertex);
    }

    public class Vertex<V> : IComparable<Vertex<V>>
    {
        public readonly int Index;
        public V Property;

        internal Vertex(int number, V property)
        {
            Index = number;
            Property = property;
        }

        public int CompareTo(Vertex<V> other) => Index.CompareTo(other.Index);

        public bool Equals(Vertex<V> v) => Index == v.Index;

        public override int GetHashCode() => Index.GetHashCode();
    }

    public class Edge<E, V> : IComparable<Edge<E, V>>
    {
        public readonly Vertex<V> Source;
        public readonly Vertex<V> Destination;
        public E Property;

        internal Edge(Vertex<V> source, Vertex<V> destination, E property)
        {
            Source = source;
            Destination = destination;
            Property = property;
        }

        public Vertex<V> GetNeighbour(Vertex<V> vertex)
        {
            if(vertex == Source)
                return Destination;

            if(vertex == Destination)
                return Source;

            throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");
        }

        public int CompareTo(Edge<E, V> other)
        {
            int firstComp = Source.CompareTo(other.Source);

            return firstComp != 0 ? firstComp : Destination.CompareTo(other.Destination);
        }

        public bool Equals(Edge<E, V> e) => Source.Equals(e.Source) && Destination.Equals(e.Destination);

        public override int GetHashCode() => Tuple.Create(Source.Index, Destination.Index).GetHashCode();
    }
}
