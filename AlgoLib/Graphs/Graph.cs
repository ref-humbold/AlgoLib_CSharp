using System.Collections.Generic;

namespace AlgoLib.Graphs;

/// <summary>Structure of basic graph.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
/// <typeparam name="TVertexProperty">Type of vertex property.</typeparam>
/// <typeparam name="TEdgeProperty">Type of edge property.</typeparam>
public interface IGraph<TVertexId, TVertexProperty, TEdgeProperty>
{
    IGraphProperties Properties { get; }

    /// <summary>Gets the number of vertices in this graph.</summary>
    /// <value>The number of vertices.</value>
    int VerticesCount { get; }

    /// <summary>Gets the number of edges in this graph.</summary>
    /// <value>The number of edges.</value>
    int EdgesCount { get; }

    /// <summary>Gets all vertices in this graph.</summary>
    /// <value>All vertices.</value>
    IEnumerable<Vertex<TVertexId>> Vertices { get; }

    /// <summary>Gets all edges in this graph.</summary>
    /// <value>All edges.</value>
    IEnumerable<Edge<TVertexId>> Edges { get; }

    /// <summary>Gets the vertex from this graph with given identifier.</summary>
    /// <value>The vertex with the identifier.</value>
    /// <param name="vertexId">The vertex identifier.</param>
    /// <exception cref="KeyNotFoundException">If no such vertex exists.</exception>
    Vertex<TVertexId> this[TVertexId vertexId] { get; }

    /// <summary>Gets the edge between vertices with given identifiers.</summary>
    /// <value>The edge between the vertices.</value>
    /// <param name="sourceId">The source vertex identifier.</param>
    /// <param name="destinationId">The destination vertex identifier.</param>
    /// <exception cref="KeyNotFoundException">If no such edge exists.</exception>
    Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId] { get; }

    /// <summary>Gets the edge between given vertices.</summary>
    /// <value>The edge between the vertices.</value>
    /// <param name="source">The source vertex.</param>
    /// <param name="destination">The destination vertex.</param>
    /// <exception cref="KeyNotFoundException">If no such edge exists.</exception>
    Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
        this[source.Id, destination.Id];

    /// <summary>Gets the neighbours of given vertex.</summary>
    /// <param name="vertex">The vertex from this graph.</param>
    /// <returns>The neighbouring vertices.</returns>
    IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex);

    /// <summary>Gets the adjacent edges of given vertex.</summary>
    /// <param name="vertex">Vertex from this graph.</param>
    /// <returns>The edges adjacent to the vertex.</returns>
    IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex);

    /// <summary>Gets the output degree of given vertex.</summary>
    /// <param name="vertex">The vertex from this graph.</param>
    /// <returns>The output degree of the vertex.</returns>
    int GetOutputDegree(Vertex<TVertexId> vertex);

    /// <summary>Gets the input degree of given vertex.</summary>
    /// <param name="vertex">The vertex from this graph.</param>
    /// <returns>The input degree of the vertex.</returns>
    int GetInputDegree(Vertex<TVertexId> vertex);

    public interface IGraphProperties
    {
        /// <summary>Gets or sets the property of given vertex.</summary>
        /// <value>The property of the vertex.</value>
        /// <param name="vertex">The vertex from this graph.</param>
        TVertexProperty this[Vertex<TVertexId> vertex] { get; set; }

        /// <summary>Gets or sets the property of given edge.</summary>
        /// <value>The property of the edge.</value>
        /// <param name="edge">The edge from this graph.</param>
        TEdgeProperty this[Edge<TVertexId> edge] { get; set; }
    }
}
