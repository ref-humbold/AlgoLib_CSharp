// Structure of basic graph
using System;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IGraph<V, VP, EP>
    {
        /// <summary>number of vertices</summary>
        int VerticesCount { get; }

        /// <summary>number of edges</summary>
        int EdgesCount { get; }

        /// <summary>enumerable of all vertices sorted by index</summary>
        IEnumerable<V> Vertices { get; }

        /// <summary>enumerable of all edges sorted first by source then by destination</summary>
        IEnumerable<Edge<V>> Edges { get; }

        /// <summary>property of a vertex from this graph</summary>
        VP this[V vertex] { get; set; }

        /// <summary>property of an edge from this graph</summary>
        EP this[Edge<V> edge] { get; set; }

        /// <param name="source">source vertex</param>
        /// <param name="destination">destination vertex</param>
        /// <returns>the edge between the vertices, or <c>null</c> if no edge</returns>
        Edge<V> GetEdge(V source, V destination);

        /// <param name="vertex">a vertex from this graph</param>
        /// <returns>enumerable of neighbouring vertices</returns>
        IEnumerable<V> GetNeighbours(V vertex);

        /// <param name="vertex">a vertex from this graph</param>
        /// <returns>enumerable of edges adjacent to the vertex</returns>
        IEnumerable<Edge<V>> GetAdjacentEdges(V vertex);

        /// <param name="vertex">a vertex from this graph</param>
        /// <returns>the output degree of the vertex</returns>
        int GetOutputDegree(V vertex);

        /// <param name="vertex">a vertex from this graph</param>
        /// <returns>the input degree of the vertex</returns>
        int GetInputDegree(V vertex);
    }

    public class Edge<V>
    {
        public readonly V Source;
        public readonly V Destination;

        internal Edge(V source, V destination)
        {
            Source = source;
            Destination = destination;
        }

        public V GetNeighbour(V vertex)
        {
            if(Source.Equals(vertex))
                return Destination;

            if(Destination.Equals(vertex))
                return Source;

            throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");
        }

        public Edge<V> Reversed() => new Edge<V>(Destination, Source);

        public override bool Equals(object obj) =>
            obj is Edge<V> other && Source.Equals(other.Source) && Destination.Equals(other.Destination);

        public override int GetHashCode() => (Source, Destination).GetHashCode();

        public override string ToString() => $"Edge{{{Source} -- {Destination}}}";
    }
}
