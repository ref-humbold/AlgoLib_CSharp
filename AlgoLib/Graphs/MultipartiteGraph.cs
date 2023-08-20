// Structure of multipartite graph
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs
{
    public class MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty>
        : IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty>

    {
        public readonly int GroupsCount;
        private readonly UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph = new();
        private readonly Dictionary<Vertex<TVertexId>, int> vertexGroupDict = new();

        public IGraph<TVertexId, TVertexProperty, TEdgeProperty>.IGraphProperties Properties => graph.Properties;

        public int VerticesCount => graph.VerticesCount;

        public int EdgesCount => graph.EdgesCount;

        public IEnumerable<Vertex<TVertexId>> Vertices => graph.Vertices;

        public IEnumerable<Edge<TVertexId>> Edges => graph.Edges;

        public MultipartiteGraph(int groupsCount)
        {
            if(groupsCount <= 0)
                throw new ArgumentException("Number of groups cannot be negative nor zero");

            GroupsCount = groupsCount;
        }

        public MultipartiteGraph(int groupsCount, IEnumerable<IEnumerable<TVertexId>> vertexIds)
            : this(groupsCount)
        {
            if(vertexIds.Count() > GroupsCount)
                throw new ArgumentException(
                    $"Cannot add vertices to group {vertexIds.Count()}, graph contains only {GroupsCount} groups");

            foreach((IEnumerable<TVertexId> groupVertexIds, int i) in vertexIds.Select((id, i) => (id, i)))
            {
                foreach(TVertexId vertexId in groupVertexIds)
                    AddVertex(i, vertexId);
            }
        }

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

        public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> AsDirected() =>
            graph.AsDirected();

        /// <param name="groupNumber">Group number.</param>
        /// <returns>Vertices that belong to the group.</returns>
        public IEnumerable<Vertex<TVertexId>> GetVerticesFromGroup(int groupNumber)
        {
            validateGroup(groupNumber);
            return vertexGroupDict.Where(entry => entry.Value == groupNumber)
                                  .Select(entry => entry.Key)
                                  .ToList();
        }

        /// <summary>Adds new vertex with given property to given group in this graph.</summary>
        /// <param name="groupNumber">Group number.</param>
        /// <param name="vertexId">Identifier of new vertex.</param>
        /// <param name="property">Vertex property.</param>
        /// <returns>New vertex.</returns>
        /// <exception cref="ArgumentException">If vertex already exists.</exception>
        public Vertex<TVertexId> AddVertex(int groupNumber,
                                           TVertexId vertexId,
                                           TVertexProperty property = default) =>
            AddVertex(groupNumber, new Vertex<TVertexId>(vertexId), property);

        /// <summary>Adds new vertex with given property to given group in this graph.</summary>
        /// <param name="groupNumber">Group number.</param>
        /// <param name="vertex">New vertex.</param>
        /// <param name="property">Vertex property.</param>
        /// <returns>New vertex.</returns>
        /// <exception cref="ArgumentException">If vertex already exists.</exception>
        public Vertex<TVertexId> AddVertex(int groupNumber,
                                           Vertex<TVertexId> vertex,
                                           TVertexProperty property = default)
        {
            validateGroup(groupNumber);

            Vertex<TVertexId> newVertex = graph.AddVertex(vertex, property);

            vertexGroupDict[newVertex] = groupNumber;
            return newVertex;
        }

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="source">Source vertex.</param>
        /// <param name="destination">Destination vertex.</param>
        /// <param name="property">Edge property.</param>
        /// <returns>New edge.</returns>
        /// <exception cref="ArgumentException">If edge already exists.</exception>
        /// <exception cref="GraphPartitionException">If vertices belong to same group.</exception>
        public Edge<TVertexId> AddEdgeBetween(Vertex<TVertexId> source,
                                              Vertex<TVertexId> destination,
                                              TEdgeProperty property = default) =>
            AddEdge(new Edge<TVertexId>(source, destination), property);

        /// <summary>Adds new edge with given property to this graph.</summary>
        /// <param name="edge">New edge.</param>
        /// <param name="property">Edge property.</param>
        /// <returns>New edge.</returns>
        /// <exception cref="ArgumentException">If edge already exists.</exception>
        /// <exception cref="GraphPartitionException">If vertices belong to same group.</exception>
        public Edge<TVertexId> AddEdge(Edge<TVertexId> edge, TEdgeProperty property = default) =>
            areInSameGroup(edge.Source, edge.Destination)
                ? throw new GraphPartitionException("Cannot create an edge between vertices in the same group")
                : graph.AddEdge(edge, property);

        private bool areInSameGroup(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            bool exists1 = vertexGroupDict.TryGetValue(vertex1, out int group1);
            bool exists2 = vertexGroupDict.TryGetValue(vertex2, out int group2);

            return exists1 && exists2 && group1 == group2;
        }

        private void validateGroup(int groupNumber)
        {
            if(groupNumber < 0 || groupNumber >= GroupsCount)
                throw new IndexOutOfRangeException(
                    $"Invalid group number {groupNumber}, graph contains only {GroupsCount} groups");
        }
    }

    [Serializable]
    public class GraphPartitionException : Exception
    {
        public GraphPartitionException(string message)
            : base(message)
        {
        }
    }
}
