// Structure of simple graph
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs
{
    public abstract class SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        IGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        internal GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty> representation;

        public IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties Properties =>
            new GraphPropertiesImpl(this);

        public int VerticesCount => representation.Count;

        public abstract int EdgesCount
        {
            get;
        }

        public IEnumerable<Vertex<TVertexId>> Vertices => representation.Vertices;

        public abstract IEnumerable<Edge<TVertexId>> Edges
        {
            get;
        }

        public SimpleGraph() => representation =
            new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>();

        public SimpleGraph(IEnumerable<TVertexId> vertexIds) =>
            representation = new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>(vertexIds);

        public Vertex<TVertexId> this[TVertexId vertexId] => representation[vertexId];

        public Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId] =>
            representation[sourceId, destinationId];

        public Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
            this[source.Id, destination.Id];

        public IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex) =>
            representation.getAdjacentEdges(vertex).Select(e => e.GetNeighbour(vertex));

        public IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex) =>
            representation.getAdjacentEdges(vertex);

        public abstract int GetOutputDegree(Vertex<TVertexId> vertex);

        public abstract int GetInputDegree(Vertex<TVertexId> vertex);

        /// <summary>Adds new vertex with given property to this graph.</summary>
        /// <param name="vertexId">Vertex identifier</param>
        /// <param name="property">Vertex property</param>
        /// <returns>New vertex</returns>
        /// <exception cref="ArgumentException">If vertex exists in the graph</exception>
        public Vertex<TVertexId> AddVertex(TVertexId vertexId, TVertexProperty property = default) =>
            AddVertex(new Vertex<TVertexId>(vertexId), property);

        /// <summary>Adds new vertex with given property to this graph.</summary>
        /// <param name="vertex">New vertex</param>
        /// <param name="property">Vertex property</param>
        /// <returns>New vertex</returns>
        /// <exception cref="ArgumentException">If vertex exists in the graph</exception>
        public Vertex<TVertexId> AddVertex(Vertex<TVertexId> vertex, TVertexProperty property = default)
        {
            bool wasAdded = representation.addVertex(vertex);

            if(wasAdded)
            {
                Properties[vertex] = property;
                return vertex;
            }

            throw new ArgumentException($"Vertex {vertex} already exists");
        }

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="source">Source vertex</param>
        /// <param name="destination">Destination vertex</param>
        /// <param name="property">Edge property</param>
        /// <returns>New edge</returns>
        /// <exception cref="ArgumentException">If edge exists in the graph</exception>
        public Edge<TVertexId> AddEdgeBetween(Vertex<TVertexId> source,
                                              Vertex<TVertexId> destination,
                                              TEdgeProperty property = default) =>
            AddEdge(new Edge<TVertexId>(source, destination), property);

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="edge">New edge</param>
        /// <param name="property">Edge property</param>
        /// <returns>New edge</returns>
        /// <exception cref="ArgumentException">If edge exists in the graph</exception>
        public abstract Edge<TVertexId> AddEdge(Edge<TVertexId> edge, TEdgeProperty property = default);

        private class GraphPropertiesImpl : IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties
        {
            private readonly SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

            public GraphPropertiesImpl(SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph) =>
                this.graph = graph;

            public TVertexProperty this[Vertex<TVertexId> vertex]
            {
                get => graph.representation.getProperty(vertex);
                set => graph.representation.setProperty(vertex, value);
            }

            public TEdgeProperty this[Edge<TVertexId> edge]
            {
                get => graph.representation.getProperty(edge);
                set => graph.representation.setProperty(edge, value);
            }
        }
    }
}
