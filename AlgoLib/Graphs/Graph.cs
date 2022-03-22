// Structure of basic graph
using System;
using System.Collections.Generic;

namespace AlgoLib.Graphs
{
    public interface IGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        IGraphProperties Properties
        {
            get;
        }

        /// <summary>Gets the number of vertices in this graph.</summary>
        /// <value>Number of vertices</value>
        int VerticesCount
        {
            get;
        }

        /// <summary>Gets the number of edges in this graph.</summary>
        /// <value>Number of edges</value>
        int EdgesCount
        {
            get;
        }

        /// <summary>Gets all vertices in this graph.</summary>
        /// <value>Enumerable of all vertices</value>
        IEnumerable<Vertex<TVertexId>> Vertices
        {
            get;
        }

        /// <summary>Gets all edges in this graph.</summary>
        /// <value>Enumerable of all edges</value>
        IEnumerable<Edge<TVertexId>> Edges
        {
            get;
        }

        /// <summary>Gets the vertex from this graph with given identifier.</summary>
        /// <value>Vertex with the identifier</value>
        /// <param name="vertexId">Vertex identifier.</param>
        /// <exception cref="KeyNotFoundException">If no such vertex</exception>
        Vertex<TVertexId> this[TVertexId vertexId]
        {
            get;
        }

        /// <summary>Gets the edge between given vertices.</summary>
        /// <value>Edge between the vertices</value>
        /// <param name="sourceId">Source vertex identifier</param>
        /// <param name="destinationId">Destination vertex identifier</param>
        /// <exception cref="KeyNotFoundException">If no such edge</exception>
        Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId]
        {
            get;
        }

        /// <summary>Gets the edge between given vertices.</summary>
        /// <value>Edge between the vertices</value>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        /// <exception cref="KeyNotFoundException">If no such edge</exception>
        Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
            this[source.Id, destination.Id];

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Enumerable of neighbouring vertices</returns>
        IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Enumerable of edges adjacent to the vertex</returns>
        IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Output degree of the vertex</returns>
        int GetOutputDegree(Vertex<TVertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Input degree of the vertex</returns>
        int GetInputDegree(Vertex<TVertexId> vertex);

        public interface IGraphProperties
        {
            /// <summary>Gets or sets property for given vertex.</summary>
            /// <value>Property of the vertex</value>
            /// <param name="vertex">Vertex from this graph</param>
            TVertexProperty this[Vertex<TVertexId> vertex]
            {
                get;
                set;
            }

            /// <summary>Gets or sets property for given edge.</summary>
            /// <value>Property of the edge</value>
            /// <param name="edge">Edge from this graph</param>
            TEdgeProperty this[Edge<TVertexId> edge]
            {
                get;
                set;
            }
        }
    }

    public class Vertex<TVertexId> : IEquatable<Vertex<TVertexId>>
    {
        public readonly TVertexId Id;

        public Vertex(TVertexId id) => Id = id;

        public bool Equals(Vertex<TVertexId> other) => other != null && Id.Equals(other.Id);

        public override bool Equals(object obj) => obj is Vertex<TVertexId> other && Equals(other);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Vertex({Id})";
    }

    public class Edge<TVertexId> : IEquatable<Edge<TVertexId>>
    {
        public readonly Vertex<TVertexId> Source;
        public readonly Vertex<TVertexId> Destination;

        public Edge(Vertex<TVertexId> source, Vertex<TVertexId> destination)
        {
            Source = source;
            Destination = destination;
        }

        /// <param name="vertex">Vertex adjacent to this edge</param>
        /// <returns>Neighbour of the vertex along this edge</returns>
        /// <exception cref="ArgumentException">If the vertex is not adjacent to this edge</exception>
        public Vertex<TVertexId> GetNeighbour(Vertex<TVertexId> vertex)
        {
            if(Source.Equals(vertex))
                return Destination;

            if(Destination.Equals(vertex))
                return Source;

            throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");
        }

        /// <returns>Edge with reversed direction</returns>
        public Edge<TVertexId> Reversed() => new Edge<TVertexId>(Destination, Source);

        public bool Equals(Edge<TVertexId> other) =>
            other != null && Source.Equals(other.Source) && Destination.Equals(other.Destination);

        public override bool Equals(object obj) => obj is Edge<TVertexId> other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Source, Destination);

        public override string ToString() => $"Edge{{{Source} -- {Destination}}}";

        public void Deconstruct(out Vertex<TVertexId> source, out Vertex<TVertexId> destination)
        {
            source = Source;
            destination = Destination;
        }
    }
}
