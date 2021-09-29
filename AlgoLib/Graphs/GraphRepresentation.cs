// Structure of graph representation
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    internal class GraphRepresentation<VertexId, VertexProperty, EdgeProperty>
    {
        // Adjacency list
        private readonly Dictionary<Vertex<VertexId>, HashSet<Edge<VertexId>>> graphDict =
            new Dictionary<Vertex<VertexId>, HashSet<Edge<VertexId>>>();

        // Vertex properties
        private readonly Dictionary<Vertex<VertexId>, VertexProperty> vertexProperties =
            new Dictionary<Vertex<VertexId>, VertexProperty>();

        // Edge properties
        private readonly Dictionary<Edge<VertexId>, EdgeProperty> edgeProperties =
            new Dictionary<Edge<VertexId>, EdgeProperty>();

        internal int Count => graphDict.Count;

        internal IEnumerable<Vertex<VertexId>> Vertices => graphDict.Keys;

        internal IEnumerable<Edge<VertexId>> Edges => graphDict.Values.SelectMany(edges => edges);

        internal IEnumerable<HashSet<Edge<VertexId>>> EdgesSet => graphDict.Values;

        internal GraphRepresentation()
        {
        }

        internal GraphRepresentation(IEnumerable<VertexId> vertexIds)
        {
            foreach(VertexId vertexId in vertexIds)
            {
                Vertex<VertexId> vertex = new Vertex<VertexId>(vertexId);

                graphDict[vertex] = new HashSet<Edge<VertexId>>();
            }
        }

        internal Vertex<VertexId> this[VertexId vertexId]
        {
            get
            {
                try
                {
                    return graphDict.Keys.First(v => v.Id.Equals(vertexId));
                }
                catch(InvalidOperationException)
                {
                    throw new KeyNotFoundException($"No vertex with ID {vertexId}");
                }
            }
        }

        internal Edge<VertexId> this[Vertex<VertexId> vertexId1, Vertex<VertexId> vertexId2]
        {
            get
            {
                try
                {
                    return graphDict.First(pair => pair.Key.Equals(vertexId1))
                                    .Value
                                    .First(edge => edge.Destination.Equals(vertexId2));
                }
                catch(InvalidOperationException)
                {
                    throw new KeyNotFoundException($"No edge between vertices {vertexId1} and {vertexId2}");
                }
            }
        }

        internal VertexProperty this[Vertex<VertexId> vertex]
        {
            get
            {
                validate(vertex);
                vertexProperties.TryGetValue(vertex, out VertexProperty val);
                return val;
            }

            set
            {
                validate(vertex);
                vertexProperties[vertex] = value;
            }
        }

        internal EdgeProperty this[Edge<VertexId> edge]
        {
            get
            {
                validate(edge, true);
                edgeProperties.TryGetValue(edge, out EdgeProperty val);
                return val;
            }

            set
            {
                validate(edge, true);
                edgeProperties[edge] = value;
            }
        }

        internal HashSet<Edge<VertexId>> getAdjacentEdges(Vertex<VertexId> vertex)
        {
            validate(vertex);
            return graphDict[vertex];
        }

        internal bool addVertex(Vertex<VertexId> vertex) =>
            graphDict.TryAdd(vertex, new HashSet<Edge<VertexId>>());

        internal void addEdgeToSource(Edge<VertexId> edge)
        {
            validate(edge, false);
            graphDict[edge.Source].Add(edge);
        }

        internal void addEdgeToDestination(Edge<VertexId> edge)
        {
            validate(edge, false);
            graphDict[edge.Destination].Add(edge);
        }

        private void validate(Vertex<VertexId> vertex)
        {
            if(!graphDict.ContainsKey(vertex))
                throw new ArgumentException($"Vertex {vertex} does not belong to this graph");
        }

        private void validate(Edge<VertexId> edge, bool existing)
        {
            if(!graphDict.ContainsKey(edge.Source) || !graphDict.ContainsKey(edge.Destination))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");

            if(existing && !graphDict[edge.Source].Contains(edge)
               && !graphDict[edge.Destination].Contains(edge))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");
        }
    }
}
