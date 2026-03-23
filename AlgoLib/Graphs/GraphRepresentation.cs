using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs;

internal class GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>
{
    private readonly Dictionary<Vertex<TVertexId>, HashSet<Edge<TVertexId>>> graphDict = [];
    private readonly Dictionary<Vertex<TVertexId>, TVertexProperty> vertexProperties = [];
    private readonly Dictionary<Edge<TVertexId>, TEdgeProperty> edgeProperties = [];

    public int Count => graphDict.Count;

    public IEnumerable<Vertex<TVertexId>> Vertices => graphDict.Keys;

    public IEnumerable<Edge<TVertexId>> Edges => graphDict.Values.SelectMany(edges => edges);

    public IEnumerable<HashSet<Edge<TVertexId>>> EdgesSet => graphDict.Values;

    public GraphRepresentation()
    {
    }

    public GraphRepresentation(IEnumerable<TVertexId> vertexIds)
    {
        foreach(TVertexId vertexId in vertexIds)
        {
            var vertex = new Vertex<TVertexId>(vertexId);

            graphDict[vertex] = [];
        }
    }

    public Vertex<TVertexId> this[TVertexId vertexId]
    {
        get
        {
            try
            {
                return graphDict.Keys.First(v => v.Id.Equals(vertexId));
            }
            catch(InvalidOperationException)
            {
                throw new KeyNotFoundException($"Vertex not found for ID {vertexId}");
            }
        }
    }

    public Edge<TVertexId> this[TVertexId vertexId1, TVertexId vertexId2]
    {
        get
        {
            try
            {
                KeyValuePair<Vertex<TVertexId>, HashSet<Edge<TVertexId>>> entry =
                    graphDict.First(pair => pair.Key.Id.Equals(vertexId1));

                return entry.Value.First(edge => edge.GetNeighbour(entry.Key).Id.Equals(vertexId2));
            }
            catch(InvalidOperationException)
            {
                throw new KeyNotFoundException($"Edge not found for vertex IDs {vertexId1} and {vertexId2}");
            }
        }
    }

    public TVertexProperty GetProperty(Vertex<TVertexId> vertex)
    {
        validateVertex(vertex);
        vertexProperties.TryGetValue(vertex, out TVertexProperty val);
        return val;
    }

    public void SetProperty(Vertex<TVertexId> vertex, TVertexProperty property)
    {
        validateVertex(vertex);
        vertexProperties[vertex] = property;
    }

    public TEdgeProperty GetProperty(Edge<TVertexId> edge)
    {
        validateEdge(edge);
        edgeProperties.TryGetValue(edge, out TEdgeProperty val);
        return val;
    }

    public void SetProperty(Edge<TVertexId> edge, TEdgeProperty property)
    {
        validateEdge(edge);
        edgeProperties[edge] = property;
    }

    public IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex)
    {
        validateVertex(vertex);
        return graphDict[vertex];
    }

    public void AddVertex(Vertex<TVertexId> vertex) =>
        graphDict.TryAdd(vertex, []);

    public void AddEdgeToSource(Edge<TVertexId> edge)
    {
        validateEdgeVertices(edge);
        graphDict[edge.Source].Add(edge);
    }

    public void AddEdgeToDestination(Edge<TVertexId> edge)
    {
        validateEdgeVertices(edge);
        graphDict[edge.Destination].Add(edge);
    }

    private void validateVertex(Vertex<TVertexId> vertex)
    {
        if(!graphDict.ContainsKey(vertex))
            throw new ArgumentException($"Vertex {vertex} does not belong to this graph");
    }

    private void validateEdgeVertices(Edge<TVertexId> edge)
    {
        if(!graphDict.ContainsKey(edge.Source) || !graphDict.ContainsKey(edge.Destination))
            throw new ArgumentException($"Edge {edge} does not belong to this graph");
    }

    private void validateEdge(Edge<TVertexId> edge)
    {
        if(!graphDict[edge.Source].Contains(edge)
           && !graphDict[edge.Destination].Contains(edge))
            throw new ArgumentException($"Edge {edge} does not belong to this graph");
    }
}
