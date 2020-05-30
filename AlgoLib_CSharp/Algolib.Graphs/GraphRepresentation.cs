// Structure of graph representation
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    internal class GraphRepresentation<V, E>
    {
        // Adjacency list of graph
        private readonly Dictionary<Vertex<V>, HashSet<Edge<E, V>>> graphDict;

        internal int Count => graphDict.Count;

        internal IEnumerable<Vertex<V>> Vertices => graphDict.Keys;

        internal IEnumerable<Edge<E, V>> Edges => graphDict.Values.SelectMany(edges => edges);

        internal IEnumerable<HashSet<Edge<E, V>>> EdgesSet => graphDict.Values;

        internal GraphRepresentation()
        {
        }

        internal GraphRepresentation(GraphRepresentation<V, E> representation)
        {
            foreach(Vertex<V> vertex in representation.graphDict.Keys)
                graphDict[vertex] = new HashSet<Edge<E, V>>();
        }

        internal HashSet<Edge<E, V>> this[Vertex<V> vertex]
        {
            get
            {
                validateVertex(vertex);

                return graphDict[vertex];
            }
        }

        internal void ClearEdges()
        {
            foreach(Vertex<V> vertex in graphDict.Keys)
                graphDict[vertex] = new HashSet<Edge<E, V>>();
        }

        internal Vertex<V> AddVertex(V property)
        {
            Vertex<V> vertex = new Vertex<V>(graphDict.Count, property);

            graphDict[vertex] = new HashSet<Edge<E, V>>();
            return vertex;
        }

        internal void AddEdgeToSource(Edge<E, V> edge)
        {
            validateEdge(edge);
            graphDict[edge.Source].Add(edge);
        }

        internal void AddEdgeToDestination(Edge<E, V> edge)
        {
            validateEdge(edge);
            graphDict[edge.Destination].Add(edge);
        }

        private void validateVertex(Vertex<V> vertex)
        {
            if(graphDict.Keys.All(v => v != vertex))
                throw new ArgumentException("Vertex object does not belong to graph");
        }

        private void validateEdge(Edge<E, V> edge)
        {
            if(graphDict.Keys.All(v => v != edge.Source))
                throw new ArgumentException(
                        "Edge source or destination does not belong to graph");

            if(graphDict.Keys.All(v => v != edge.Destination))
                throw new ArgumentException(
                        "Edge source or destination does not belong to graph");
        }
    }
}
