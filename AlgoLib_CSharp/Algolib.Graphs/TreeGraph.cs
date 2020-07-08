// Structure of tree graph
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public class TreeGraph<V, VP, EP> : IUndirectedGraph<V, VP, EP>
    {
        private readonly UndirectedSimpleGraph<V, VP, EP> graph;

        public int VerticesCount => graph.VerticesCount;

        public int EdgesCount => graph.EdgesCount;

        public IEnumerable<V> Vertices => graph.Vertices;

        public IEnumerable<Edge<V>> Edges => graph.Edges;

        public TreeGraph(V vertex)
        {
            graph = new UndirectedSimpleGraph<V, VP, EP>(Enumerable.Repeat(vertex, 1));
        }

        public VP this[V vertex]
        {
            get { return graph[vertex]; }
            set { graph[vertex] = value; }
        }

        public EP this[Edge<V> edge]
        {
            get { return graph[edge]; }
            set { graph[edge] = value; }
        }

        public Edge<V> GetEdge(V source, V destination) => graph.GetEdge(source, destination);

        public IEnumerable<Edge<V>> GetAdjacentEdges(V vertex) => graph.GetAdjacentEdges(vertex);

        public IEnumerable<V> GetNeighbours(V vertex) => graph.GetNeighbours(vertex);

        public int GetOutputDegree(V vertex) => graph.GetOutputDegree(vertex);

        public int GetInputDegree(V vertex) => graph.GetInputDegree(vertex);

        /// <summary>Adds a new vertex to this graph and creates an edge to an existing vertex.</summary>
        /// <param name="vertex">a new vertex</param>
        /// <param name="neighbour">an existing vertex</param>
        /// <param name="vertexProperty">a vertex property</param>
        /// <param name="edgeProperty">an edge property</param>
        /// <returns>the edge between the vertices, or <c>null</c> if vertex already exists</returns>
        public Edge<V> AddVertex(V vertex, V neighbour, VP vertexProperty = default,
                                 EP edgeProperty = default)
        {
            bool wasAdded = graph.AddVertex(vertex, vertexProperty);

            return wasAdded ? graph.AddEdgeBetween(vertex, neighbour, edgeProperty) : null;
        }
    }
}
