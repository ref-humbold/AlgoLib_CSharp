using System;

namespace AlgoLib.Graphs;

/// <summary>Structure of graph edge.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
public record Edge<TVertexId>(Vertex<TVertexId> Source, Vertex<TVertexId> Destination)
{
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

    public override string ToString() => $"Edge{{{Source} -- {Destination}}}";
}
