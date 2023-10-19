using System;

namespace AlgoLib.Graphs;

/// <summary>Structure of graph vertex.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
public class Vertex<TVertexId> : IEquatable<Vertex<TVertexId>>
{
    public TVertexId Id { get; }

    public Vertex(TVertexId id) => Id = id;

    public bool Equals(Vertex<TVertexId> other) => other != null && Id.Equals(other.Id);

    public override bool Equals(object obj) => obj is Vertex<TVertexId> other && Equals(other);

    public override int GetHashCode() => Id.GetHashCode();

    public override string ToString() => $"Vertex({Id})";
}
