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

        /// <summary>Number of vertices.</summary>
        int VerticesCount
        {
            get;
        }

        /// <summary>Number of edges.</summary>
        int EdgesCount
        {
            get;
        }

        /// <summary>Enumerable of all vertices.</summary>
        IEnumerable<Vertex<TVertexId>> Vertices
        {
            get;
        }

        /// <summary>Enumerable of all edges.</summary>
        IEnumerable<Edge<TVertexId>> Edges
        {
            get;
        }

        /// <summary>Vertex with given identifier.</summary>
        /// <exception cref="KeyNotFoundException">If no such vertex in this graph</exception>
        Vertex<TVertexId> this[TVertexId vertexId]
        {
            get;
        }

        /// <summary>Edge between given vertices.</summary>
        /// <exception cref="KeyNotFoundException">If no such edge in this graph</exception>
        Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId]
        {
            get;
        }

        /// <summary>Edge between given vertices.</summary>
        /// <exception cref="KeyNotFoundException">If no such edge in this graph</exception>
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
            /// <summary>Property of a vertex from this graph.</summary>
            TVertexProperty this[Vertex<TVertexId> vertex]
            {
                get;
                set;
            }

            /// <summary>Property of an edge from this graph.</summary>
            TEdgeProperty this[Edge<TVertexId> edge]
            {
                get;
                set;
            }
        }
    }

    public class Vertex<TVertexId>
    {
        public readonly TVertexId Id;

        public Vertex(TVertexId id) => Id = id;

        public override bool Equals(object obj) => obj is Vertex<TVertexId> vertex && Id.Equals(vertex.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Vertex({Id})";
    }

    public class Edge<TVertexId>
    {
        public readonly Vertex<TVertexId> Source;
        public readonly Vertex<TVertexId> Destination;

        public Edge(Vertex<TVertexId> source, Vertex<TVertexId> destination)
        {
            Source = source;
            Destination = destination;
        }

        public Vertex<TVertexId> GetNeighbour(Vertex<TVertexId> vertex)
        {
            if(Source.Equals(vertex))
                return Destination;

            if(Destination.Equals(vertex))
                return Source;

            throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");
        }

        public Edge<TVertexId> Reversed() => new Edge<TVertexId>(Destination, Source);

        public override bool Equals(object obj) =>
            obj is Edge<TVertexId> other && Source.Equals(other.Source) && Destination.Equals(other.Destination);

        public override int GetHashCode() => HashCode.Combine(Source, Destination);

        public override string ToString() => $"Edge{{{Source} -- {Destination}}}";

        public void Deconstruct(out Vertex<TVertexId> source, out Vertex<TVertexId> destination)
        {
            source = Source;
            destination = Destination;
        }
    }
}
