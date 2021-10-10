// Structure of tree graph
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public class TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        private readonly UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

        public IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties Properties => graph.Properties;

        public int VerticesCount => graph.VerticesCount;

        public int EdgesCount => graph.EdgesCount;

        public IEnumerable<Vertex<TVertexId>> Vertices => graph.Vertices;

        public IEnumerable<Edge<TVertexId>> Edges => graph.Edges;

        public TreeGraph(TVertexId vertexId) =>
            graph = new UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(new[] { vertexId });

        public Vertex<TVertexId> this[TVertexId vertexId] => graph[vertexId];

        public Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId] =>
            graph[sourceId, destinationId];

        public Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
            this[source.Id, destination.Id];

        public IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex) =>
            graph.GetAdjacentEdges(vertex);

        public IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex) =>
            graph.GetNeighbours(vertex);

        public int GetOutputDegree(Vertex<TVertexId> vertex) => graph.GetOutputDegree(vertex);

        public int GetInputDegree(Vertex<TVertexId> vertex) => graph.GetInputDegree(vertex);

        /// <summary>Adds a new vertex to this graph and creates an edge to an existing vertex.</summary>
        /// <param name="vertexId">Vertex identifier</param>
        /// <param name="neighbour">Existing vertex</param>
        /// <param name="vertexProperty">Vertex property</param>
        /// <param name="edgeProperty">Edge property</param>
        /// <returns>Edge between the vertices</returns>
        public Edge<TVertexId> AddVertex(TVertexId vertexId,
                                        Vertex<TVertexId> neighbour,
                                        TVertexProperty vertexProperty = default,
                                        TEdgeProperty edgeProperty = default)
        {
            Vertex<TVertexId> vertex = graph.AddVertex(vertexId, vertexProperty);
            return vertex != null ? graph.AddEdgeBetween(vertex, neighbour, edgeProperty) : null;
        }
    }
}
