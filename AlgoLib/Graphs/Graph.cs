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

        /// <summary>Vertex with given identifier.</summary>
        /// <exception cref="KeyNotFoundException">If no such vertex in this graph</exception>
        Vertex<VertexId> this[VertexId vertexId]
        {
            get;
        }

        /// <summary>Edge between given vertices.</summary>
        /// <exception cref="KeyNotFoundException">If no such edge in this graph</exception>
        Edge<VertexId> this[Vertex<VertexId> source, Vertex<VertexId> destination]
        {
            get;
        }

        /// <param name="vertex">Vertex from this graph</param>
        /// <return>Property of the vertex</return>
        VertexProperty GetProperty(Vertex<VertexId> vertex);

        /// <param name="vertex">Vertex from this graph</param>
        /// <param name="property">Property for given vertex</param>
        /// <return>Property of the vertex</return>
        void SetProperty(Vertex<VertexId> vertex, VertexProperty property);

        /// <param name="edge">Edge from this graph</param>
        /// <return>Property of the edge</return>
        EdgeProperty GetProperty(Edge<VertexId> edge);

        /// <param name="edge">Edge from this graph</param>
        /// <param name="property">Property for given edge</param>
        /// <return>Property of the edge</return>
        void SetProperty(Edge<VertexId> edge, EdgeProperty property);

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
