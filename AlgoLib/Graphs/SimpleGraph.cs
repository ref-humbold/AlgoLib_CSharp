// Structure of simple graph
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public abstract class SimpleGraph<VertexId, VertexProperty, EdgeProperty> :
        IGraph<VertexId, VertexProperty, EdgeProperty>
    {
        internal GraphRepresentation<VertexId, VertexProperty, EdgeProperty> representation;

        public int VerticesCount => representation.Count;

        public abstract int EdgesCount
        {
            get;
        }

        public IEnumerable<Vertex<VertexId>> Vertices => representation.Vertices;

        public abstract IEnumerable<Edge<VertexId>> Edges
        {
            get;
        }

        public SimpleGraph() => representation =
            new GraphRepresentation<VertexId, VertexProperty, EdgeProperty>();

        public SimpleGraph(IEnumerable<VertexId> vertexIds) =>
            representation = new GraphRepresentation<VertexId, VertexProperty, EdgeProperty>(vertexIds);

        public Vertex<VertexId> this[VertexId vertexId] => representation[vertexId];

        public Edge<VertexId> this[Vertex<VertexId> source, Vertex<VertexId> destination] =>
            representation[source, destination];

        public VertexProperty GetProperty(Vertex<VertexId> vertex) => representation.getProperty(vertex);

        public void SetProperty(Vertex<VertexId> vertex, VertexProperty property) =>
            representation.setProperty(vertex, property);

        public EdgeProperty GetProperty(Edge<VertexId> edge) => representation.getProperty(edge);

        public void SetProperty(Edge<VertexId> edge, EdgeProperty property) =>
            representation.setProperty(edge, property);

        public IEnumerable<Vertex<VertexId>> GetNeighbours(Vertex<VertexId> vertex) =>
            representation.getAdjacentEdges(vertex).Select(e => e.GetNeighbour(vertex));

        public IEnumerable<Edge<VertexId>> GetAdjacentEdges(Vertex<VertexId> vertex) =>
            representation.getAdjacentEdges(vertex);

        public abstract int GetOutputDegree(Vertex<VertexId> vertex);

        public abstract int GetInputDegree(Vertex<VertexId> vertex);

        /// <summary>Adds new vertex with given property to this graph.</summary>
        /// <param name="vertexId">Vertex identifier</param>
        /// <param name="property">Vertex property</param>
        /// <returns>New vertex</returns>
        /// <exception cref="ArgumentException">If</exception>
        public Vertex<VertexId> AddVertex(VertexId vertexId, VertexProperty property = default)
        {
            Vertex<VertexId> vertex = new Vertex<VertexId>(vertexId);
            bool wasAdded = representation.addVertex(vertex);

            if(wasAdded)
                SetProperty(vertex, property);

            return wasAdded ? vertex : null;
        }

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="source">source vertex</param>
        /// <param name="destination">destination vertex</param>
        /// <param name="property">an edge property</param>
        /// <returns>the new edge if added, otherwise existing edge</returns>
        public Edge<VertexId> AddEdgeBetween(Vertex<VertexId> source,
                                             Vertex<VertexId> destination,
                                             EdgeProperty property = default) =>
            AddEdge(new Edge<VertexId>(source, destination), property);

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="edge">a new edge</param>
        /// <param name="property">an edge property</param>
        /// <returns>the new edge if added, otherwise existing edge</returns>
        public abstract Edge<VertexId> AddEdge(Edge<VertexId> edge, EdgeProperty property = default);
    }
}
