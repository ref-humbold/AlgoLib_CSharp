// Structure of graph representation
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    internal class GraphRepresentation<V, VP, EP>
    {
        // Adjacency list
        private readonly Dictionary<V, HashSet<Edge<V>>> graphDict =
            new Dictionary<V, HashSet<Edge<V>>>();

        // Vertex properties
        private readonly Dictionary<V, VP> vertexProperties = new Dictionary<V, VP>();

        // Edge properties
        private readonly Dictionary<Edge<V>, EP> edgeProperties = new Dictionary<Edge<V>, EP>();

        internal int Count => graphDict.Count;

        internal IEnumerable<V> Vertices => graphDict.Keys;

        internal IEnumerable<Edge<V>> Edges => graphDict.Values.SelectMany(edges => edges);

        internal IEnumerable<HashSet<Edge<V>>> EdgesSet => graphDict.Values;

        internal GraphRepresentation()
        {
        }

        internal GraphRepresentation(IEnumerable<V> vertices)
        {
            foreach(V vertex in vertices)
                graphDict[vertex] = new HashSet<Edge<V>>();
        }

        internal VP this[V vertex]
        {
            get
            {
                validate(vertex);
                vertexProperties.TryGetValue(vertex, out VP val);
                return val;
            }

            set
            {
                validate(vertex);
                vertexProperties[vertex] = value;
            }
        }

        internal EP this[Edge<V> edge]
        {
            get
            {
                validate(edge, true);
                edgeProperties.TryGetValue(edge, out EP val);
                return val;
            }

            set
            {
                validate(edge, true);
                edgeProperties[edge] = value;
            }
        }

        internal HashSet<Edge<V>> GetAdjacentEdges(V vertex)
        {
            validate(vertex);
            return graphDict[vertex];
        }

        internal bool AddVertex(V vertex) => graphDict.TryAdd(vertex, new HashSet<Edge<V>>());

        internal void AddEdgeToSource(Edge<V> edge)
        {
            validate(edge, false);
            graphDict[edge.Source].Add(edge);
        }

        internal void AddEdgeToDestination(Edge<V> edge)
        {
            validate(edge, false);
            graphDict[edge.Destination].Add(edge);
        }

        private void validate(V vertex)
        {
            if(!graphDict.ContainsKey(vertex))
                throw new ArgumentException($"Vertex {vertex} does not belong to this graph");
        }

        private void validate(Edge<V> edge, bool existing)
        {
            if(!graphDict.ContainsKey(edge.Source) || !graphDict.ContainsKey(edge.Destination))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");

            if(existing && !graphDict[edge.Source].Contains(edge)
               && !graphDict[edge.Destination].Contains(edge))
                throw new ArgumentException($"Edge {edge} does not belong to this graph");
        }
    }
}
