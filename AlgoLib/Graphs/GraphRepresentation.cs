using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs;

internal class GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>
{
    private readonly Dictionary<Vertex<TVertexId>, HashSet<Edge<TVertexId>>> graphDict = new();
    private readonly Dictionary<Vertex<TVertexId>, TVertexProperty> vertexProperties = new();
    private readonly Dictionary<Edge<TVertexId>, TEdgeProperty> edgeProperties = new();

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

            graphDict[vertex] = new HashSet<Edge<TVertexId>>();
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

    public TVertexProperty getProperty(Vertex<TVertexId> vertex)
    {
        validateVertex(vertex);
        vertexProperties.TryGetValue(vertex, out TVertexProperty val);
        return val;
    }

    public void setProperty(Vertex<TVertexId> vertex, TVertexProperty property)
    {
        validateVertex(vertex);
        vertexProperties[vertex] = property;
    }

    public TEdgeProperty getProperty(Edge<TVertexId> edge)
    {
        validateEdge(edge);
        edgeProperties.TryGetValue(edge, out TEdgeProperty val);
        return val;
    }

    public void setProperty(Edge<TVertexId> edge, TEdgeProperty property)
    {
        validateEdge(edge);
        edgeProperties[edge] = property;
    }

    public IEnumerable<Edge<TVertexId>> getAdjacentEdges(Vertex<TVertexId> vertex)
    {
        validateVertex(vertex);
        return graphDict[vertex];
    }

    public void addVertex(Vertex<TVertexId> vertex) =>
        graphDict.TryAdd(vertex, new HashSet<Edge<TVertexId>>());

    public void addEdgeToSource(Edge<TVertexId> edge)
    {
        validateEdgeVertices(edge);
        graphDict[edge.Source].Add(edge);
    }

    public void addEdgeToDestination(Edge<TVertexId> edge)
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
