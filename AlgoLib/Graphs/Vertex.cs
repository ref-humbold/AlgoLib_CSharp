namespace AlgoLib.Graphs;

/// <summary>Structure of graph vertex.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
public record Vertex<TVertexId>(TVertexId Id)
{
    public override string ToString() => $"Vertex({Id})";
}
