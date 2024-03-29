﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs;

/// <summary>Structure of undirected graph.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
/// <typeparam name="TVertexProperty">Type of vertex property.</typeparam>
/// <typeparam name="TEdgeProperty">Type of edge property.</typeparam>
public interface IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> :
    IGraph<TVertexId, TVertexProperty, TEdgeProperty>
{
    /// <summary>Converts this graph to the directed graph with the same vertices.</summary>
    /// <returns>The directed copy of this graph.</returns>
    public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> AsDirected();
}

/// <summary>Structure of undirected simple graph.</summary>
/// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
/// <typeparam name="TVertexProperty">Type of vertex property.</typeparam>
/// <typeparam name="TEdgeProperty">Type of edge property.</typeparam>
public class UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>,
    IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty>
{
    public override int EdgesCount => Representation.Edges.Distinct().Count();

    public override IEnumerable<Edge<TVertexId>> Edges => Representation.Edges.Distinct();

    public UndirectedSimpleGraph()
        : base()
    {
    }

    public UndirectedSimpleGraph(IEnumerable<TVertexId> vertexIds)
        : base(vertexIds)
    {
    }

    public override int GetOutputDegree(Vertex<TVertexId> vertex) =>
        Representation.getAdjacentEdges(vertex).Count();

    public override int GetInputDegree(Vertex<TVertexId> vertex) =>
        Representation.getAdjacentEdges(vertex).Count();

    public override Edge<TVertexId> AddEdge(
        Edge<TVertexId> edge, TEdgeProperty property = default)
    {
        try
        {
            Edge<TVertexId> existingEdge = this[edge.Source, edge.Destination];

            throw new ArgumentException($"Edge {existingEdge} already exists in the graph");
        }
        catch(KeyNotFoundException)
        {
            Representation.addEdgeToSource(edge);
            Representation.addEdgeToDestination(edge);
            Representation.setProperty(edge, property);
            return edge;
        }
    }

    public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> AsDirected()
    {
        var directedSimpleGraph = new DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(
            Vertices.Select(v => v.Id));

        foreach(Vertex<TVertexId> vertex in Vertices)
            directedSimpleGraph.Properties[vertex] = Properties[vertex];

        foreach(Edge<TVertexId> edge in Edges)
        {
            directedSimpleGraph.AddEdge(edge, Properties[edge]);

            if(edge.Source != edge.Destination)
                directedSimpleGraph.AddEdge(edge.Reversed(), Properties[edge]);
        }

        return directedSimpleGraph;
    }
}
