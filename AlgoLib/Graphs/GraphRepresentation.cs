// Structure of graph representation
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs
{
    internal class GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>
    {
        // Adjacency list
        private readonly Dictionary<Vertex<TVertexId>, HashSet<Edge<TVertexId>>> graphDict =
            new Dictionary<Vertex<TVertexId>, HashSet<Edge<TVertexId>>>();

        // Vertex properties
        private readonly Dictionary<Vertex<TVertexId>, TVertexProperty> vertexProperties =
            new Dictionary<Vertex<TVertexId>, TVertexProperty>();

        // Edge properties
        private readonly Dictionary<Edge<TVertexId>, TEdgeProperty> edgeProperties =
            new Dictionary<Edge<TVertexId>, TEdgeProperty>();

        internal int Count => graphDict.Count;

        internal IEnumerable<Vertex<TVertexId>> Vertices => graphDict.Keys;

        internal IEnumerable<Edge<TVertexId>> Edges => graphDict.Values.SelectMany(edges => edges);

        internal IEnumerable<HashSet<Edge<TVertexId>>> EdgesSet => graphDict.Values;

        internal GraphRepresentation()
        {
        }

        internal GraphRepresentation(IEnumerable<TVertexId> vertexIds)
        {
            foreach(TVertexId vertexId in vertexIds)
            {
                Vertex<TVertexId> vertex = new Vertex<TVertexId>(vertexId);

                graphDict[vertex] = new HashSet<Edge<TVertexId>>();
            }
        }

        internal Vertex<TVertexId> this[TVertexId vertexId]
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

        internal Edge<TVertexId> this[TVertexId vertexId1, TVertexId vertexId2]
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

        internal TVertexProperty getProperty(Vertex<TVertexId> vertex)
        {
            validate(vertex);
            vertexProperties.TryGetValue(vertex, out TVertexProperty val);
            return val;
        }

        internal void setProperty(Vertex<TVertexId> vertex, TVertexProperty property)
        {
            validate(vertex);
            vertexProperties[vertex] = property;
        }

        internal TEdgeProperty getProperty(Edge<TVertexId> edge)
        {
            validate(edge, true);
            edgeProperties.TryGetValue(edge, out TEdgeProperty val);
            return val;
        }

        internal void setProperty(Edge<TVertexId> edge, TEdgeProperty property)
        {
            validate(edge, true);
            edgeProperties[edge] = property;
        }

        internal IEnumerable<Edge<TVertexId>> getAdjacentEdges(Vertex<TVertexId> vertex)
        {
            validate(vertex);
            return graphDict[vertex];
        }

        internal bool addVertex(Vertex<TVertexId> vertex) =>
            graphDict.TryAdd(vertex, new HashSet<Edge<TVertexId>>());

        internal void addEdgeToSource(Edge<TVertexId> edge)
        {
            validate(edge, false);
            graphDict[edge.Source].Add(edge);
        }

        internal void addEdgeToDestination(Edge<TVertexId> edge)
        {
            validate(edge, false);
            graphDict[edge.Destination].Add(edge);
        }

        private void validate(Vertex<TVertexId> vertex)
        {
            if(!graphDict.ContainsKey(vertex))
                throw new ArgumentException($"Vertex {vertex} does not belong to this graph");
        }

        private void validate(Edge<TVertexId> edge, bool existing)
        {
            if(!graphDict.ContainsKey(edge.Source) || !graphDict.ContainsKey(edge.Destination))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");

            if(existing && !graphDict[edge.Source].Contains(edge)
               && !graphDict[edge.Destination].Contains(edge))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");
        }
    }
}
