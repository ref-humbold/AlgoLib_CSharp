using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs;

/// <summary>Structure of simple graph.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
/// <typeparam name="TVertexProperty">Type of vertex property.</typeparam>
/// <typeparam name="TEdgeProperty">Type of edge property.</typeparam>
public abstract class SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> :
    IGraph<TVertexId, TVertexProperty, TEdgeProperty>
{
    public IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties Properties =>
        new GraphPropertiesImpl(this);

    public int VerticesCount => Representation.Count;

    public abstract int EdgesCount { get; }

    public IEnumerable<Vertex<TVertexId>> Vertices => Representation.Vertices;

    public abstract IEnumerable<Edge<TVertexId>> Edges { get; }

    internal GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty> Representation
    {
        get; set;
    }

    public SimpleGraph() =>
        Representation = new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>();

    public SimpleGraph(IEnumerable<TVertexId> vertexIds) =>
        Representation = new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>(vertexIds);

    public Vertex<TVertexId> this[TVertexId vertexId] => Representation[vertexId];

    public Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId] =>
        Representation[sourceId, destinationId];

    public Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
        this[source.Id, destination.Id];

    public IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex) =>
        Representation.getAdjacentEdges(vertex).Select(e => e.GetNeighbour(vertex));

    public IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex) =>
        Representation.getAdjacentEdges(vertex);

    public abstract int GetOutputDegree(Vertex<TVertexId> vertex);

    public abstract int GetInputDegree(Vertex<TVertexId> vertex);

    /// <summary>Adds new vertex with given property to this graph.</summary>
    /// <param name="vertexId">The identifier of new vertex.</param>
    /// <param name="property">The vertex property.</param>
    /// <returns>The created vertex.</returns>
    /// <exception cref="ArgumentException">If the vertex already exists.</exception>
    public Vertex<TVertexId> AddVertex(TVertexId vertexId, TVertexProperty property = default) =>
        AddVertex(new Vertex<TVertexId>(vertexId), property);

    /// <summary>Adds new vertex with given property to this graph.</summary>
    /// <param name="vertex">The new vertex.</param>
    /// <param name="property">The vertex property.</param>
    /// <returns>The created vertex.</returns>
    /// <exception cref="ArgumentException">If the vertex already exists.</exception>
    public Vertex<TVertexId> AddVertex(
        Vertex<TVertexId> vertex, TVertexProperty property = default)
    {
        bool wasAdded = Representation.addVertex(vertex);

        if(wasAdded)
        {
            Properties[vertex] = property;
            return vertex;
        }

        throw new ArgumentException($"Vertex {vertex} already exists");
    }

    /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
    /// <param name="source">The source vertex.</param>
    /// <param name="destination">The destination vertex.</param>
    /// <param name="property">The edge property.</param>
    /// <returns>The created edge.</returns>
    /// <exception cref="ArgumentException">If the edge already exists.</exception>
    public Edge<TVertexId> AddEdgeBetween(
            Vertex<TVertexId> source,
            Vertex<TVertexId> destination,
            TEdgeProperty property = default) =>
        AddEdge(new Edge<TVertexId>(source, destination), property);

    /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
    /// <param name="edge">The new edge.</param>
    /// <param name="property">The edge property.</param>
    /// <returns>The created edge.</returns>
    /// <exception cref="ArgumentException">If the edge already exists.</exception>
    public abstract Edge<TVertexId> AddEdge(
        Edge<TVertexId> edge, TEdgeProperty property = default);

    private class GraphPropertiesImpl :
        IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties
    {
        private readonly SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

        public GraphPropertiesImpl(SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph) =>
            this.graph = graph;

        public TVertexProperty this[Vertex<TVertexId> vertex]
        {
            get => graph.Representation.getProperty(vertex);
            set => graph.Representation.setProperty(vertex, value);
        }

        public TEdgeProperty this[Edge<TVertexId> edge]
        {
            get => graph.Representation.getProperty(edge);
            set => graph.Representation.setProperty(edge, value);
        }
    }
}
