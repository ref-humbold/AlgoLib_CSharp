namespace AlgoLib.Graphs.Algorithms;

/// <summary>Strategy for BFS searching.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
public interface IBfsStrategy<TVertexId>
{
    void ForRoot(Vertex<TVertexId> root);

    void OnEntry(Vertex<TVertexId> vertex);

    void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour);

    void OnExit(Vertex<TVertexId> vertex);
}

/// <summary>Strategy for DFS searching.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
public interface IDfsStrategy<TVertexId> : IBfsStrategy<TVertexId>
{
    void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour);
}

public struct EmptyStrategy<TVertexId> : IDfsStrategy<TVertexId>
{
    public readonly void ForRoot(Vertex<TVertexId> root)
    {
    }

    public readonly void OnEntry(Vertex<TVertexId> vertex)
    {
    }

    public readonly void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
    {
    }

    public readonly void OnExit(Vertex<TVertexId> vertex)
    {
    }

    public readonly void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
    {
    }
}
