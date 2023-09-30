// Structure of graph edge
using System;

namespace AlgoLib.Graphs
{
    public class Edge<TVertexId> : IEquatable<Edge<TVertexId>>
    {
        public Vertex<TVertexId> Source { get; }

        public Vertex<TVertexId> Destination { get; }

        public Edge(Vertex<TVertexId> source, Vertex<TVertexId> destination)
        {
            Source = source;
            Destination = destination;
        }

        /// <summary>Gets the neighbour of given adjacent vertex.</summary>
        /// <param name="vertex">The vertex adjacent to this edge.</param>
        /// <returns>The neighbour of the vertex along this edge.</returns>
        /// <exception cref="ArgumentException">If the vertex is not adjacent to this edge.</exception>
        public Vertex<TVertexId> GetNeighbour(Vertex<TVertexId> vertex) =>
            Source.Equals(vertex)
                ? Destination
                : Destination.Equals(vertex)
                    ? Source
                    : throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");

        /// <summary>Gets the reversed copy of this edge.</summary>
        /// <returns>The edge with reversed direction.</returns>
        public Edge<TVertexId> Reversed() => new(Destination, Source);

        public bool Equals(Edge<TVertexId> other) =>
            other != null && Source.Equals(other.Source) && Destination.Equals(other.Destination);

        public override bool Equals(object obj) => obj is Edge<TVertexId> other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Source, Destination);

        public override string ToString() => $"Edge{{{Source} -- {Destination}}}";

        public void Deconstruct(out Vertex<TVertexId> source, out Vertex<TVertexId> destination)
        {
            source = Source;
            destination = Destination;
        }
    }
}
