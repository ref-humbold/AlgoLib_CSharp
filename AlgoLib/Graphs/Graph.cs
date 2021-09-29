// Structure of basic graph
using System;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IGraph<VertexId, VertexProperty, EdgeProperty>
    {
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
        IEnumerable<Vertex<VertexId>> Vertices
        {
            get;
        }

        /// <summary>Enumerable of all edges.</summary>
        IEnumerable<Edge<VertexId>> Edges
        {
            get;
        }

        /// <summary>Property of a vertex from this graph.</summary>
        VertexProperty this[Vertex<VertexId> vertex]
        {
            get;
            set;
        }

        /// <summary>Property of an edge from this graph.</summary>
        EdgeProperty this[Edge<VertexId> edge]
        {
            get;
            set;
        }

        /// <param name="vertexId">Vertex identifier.</param>
        /// <returns>Vertex with given identifier</returns>
        /// <exception cref="KeyNotFoundException">If no vertex with given identifier</exception>
        Vertex<VertexId> GetVertex(VertexId vertexId);

        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        /// <returns>Edge between the given vertices</returns>
        /// <exception cref="KeyNotFoundException">If no edge between given vertices</exception>
        Edge<VertexId> GetEdge(Vertex<VertexId> source, Vertex<VertexId> destination);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Enumerable of neighbouring vertices</returns>
        IEnumerable<Vertex<VertexId>> GetNeighbours(Vertex<VertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Enumerable of edges adjacent to the vertex</returns>
        IEnumerable<Edge<VertexId>> GetAdjacentEdges(Vertex<VertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Output degree of the vertex</returns>
        int GetOutputDegree(Vertex<VertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <returns>Input degree of the vertex</returns>
        int GetInputDegree(Vertex<VertexId> vertex);
    }

    public class Vertex<VertexId>
    {
        public readonly VertexId Id;

        internal Vertex(VertexId id) => Id = id;

        public override bool Equals(object obj) => obj is Vertex<VertexId> vertex && Id.Equals(vertex.Id);

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => $"Vertex({Id})";
    }

    public class Edge<VertexId>
    {
        public readonly Vertex<VertexId> Source;
        public readonly Vertex<VertexId> Destination;

        internal Edge(Vertex<VertexId> source, Vertex<VertexId> destination)
        {
            Source = source;
            Destination = destination;
        }

        public Vertex<VertexId> GetNeighbour(Vertex<VertexId> vertex)
        {
            if(Source.Equals(vertex))
                return Destination;

            if(Destination.Equals(vertex))
                return Source;

            throw new ArgumentException($"Edge {this} is not adjacent to given vertex {vertex}");
        }

        public Edge<VertexId> Reversed() => new Edge<VertexId>(Destination, Source);

        public override bool Equals(object obj) =>
            obj is Edge<VertexId> other && Source.Equals(other.Source) && Destination.Equals(other.Destination);

        public override int GetHashCode() => HashCode.Combine(Source, Destination);

        public override string ToString() => $"Edge{{{Source} -- {Destination}}}";

        public void Deconstruct(out Vertex<VertexId> source, out Vertex<VertexId> destination)
        {
            source = Source;
            destination = Destination;
        }
    }
}
