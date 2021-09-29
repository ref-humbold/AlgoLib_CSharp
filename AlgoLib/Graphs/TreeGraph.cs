// Structure of tree graph
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public class TreeGraph<VertexId, VertexProperty, EdgeProperty> :
        IUndirectedGraph<VertexId, VertexProperty, EdgeProperty>
    {
        private readonly UndirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> graph;

        public int VerticesCount => graph.VerticesCount;

        public int EdgesCount => graph.EdgesCount;

        public IEnumerable<Vertex<VertexId>> Vertices => graph.Vertices;

        public IEnumerable<Edge<VertexId>> Edges => graph.Edges;

        public TreeGraph(VertexId vertexId) =>
            graph = new UndirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty>(new[] { vertexId });

        public VertexProperty this[Vertex<VertexId> vertex]
        {
            get => graph[vertex];
            set => graph[vertex] = value;
        }

        public EdgeProperty this[Edge<VertexId> edge]
        {
            get => graph[edge];
            set => graph[edge] = value;
        }

        public Vertex<VertexId> GetVertex(VertexId vertexId) => graph.GetVertex(vertexId);

        public Edge<VertexId> GetEdge(Vertex<VertexId> source, Vertex<VertexId> destination) => graph.GetEdge(source, destination);

        public IEnumerable<Edge<VertexId>> GetAdjacentEdges(Vertex<VertexId> vertex) => graph.GetAdjacentEdges(vertex);

        public IEnumerable<Vertex<VertexId>> GetNeighbours(Vertex<VertexId> vertex) => graph.GetNeighbours(vertex);

        public int GetOutputDegree(Vertex<VertexId> vertex) => graph.GetOutputDegree(vertex);

        public int GetInputDegree(Vertex<VertexId> vertex) => graph.GetInputDegree(vertex);

        /// <summary>Adds a new vertex to this graph and creates an edge to an existing vertex.</summary>
        /// <param name="vertexId">Vertex identifier</param>
        /// <param name="neighbour">Existing vertex</param>
        /// <param name="vertexProperty">Vertex property</param>
        /// <param name="edgeProperty">Edge property</param>
        /// <returns>Edge between the vertices</returns>
        public Edge<VertexId> AddVertex(VertexId vertexId,
                                        Vertex<VertexId> neighbour,
                                        VertexProperty vertexProperty = default,
                                        EdgeProperty edgeProperty = default)
        {
            Vertex<VertexId> vertex = graph.AddVertex(vertexId, vertexProperty);
            return vertex != null ? graph.AddEdgeBetween(vertex, neighbour, edgeProperty) : null;
        }
    }
}
