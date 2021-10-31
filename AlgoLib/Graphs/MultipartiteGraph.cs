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

        private readonly UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> graph =
                new UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>();

        private readonly Dictionary<Vertex<TVertexId>, int> vertexGroupDict =
            new Dictionary<Vertex<TVertexId>, int>();

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

            int i = 0;

            foreach(IEnumerable<TVertexId> groupVertexIds in vertexIds)
            {
                foreach(TVertexId vertexId in groupVertexIds)
                    AddVertex(i, vertexId);

                ++i;
            }
        }

        public Vertex<TVertexId> this[TVertexId vertexId] => graph[vertexId];

        public Edge<TVertexId> this[TVertexId sourceId, TVertexId destinationId] =>
            graph[sourceId, destinationId];

        public Edge<TVertexId> this[Vertex<TVertexId> source, Vertex<TVertexId> destination] =>
            this[source.Id, destination.Id];

        public IEnumerable<Edge<TVertexId>> GetAdjacentEdges(Vertex<TVertexId> vertex) =>
            graph.GetAdjacentEdges(vertex);

        public IEnumerable<Vertex<TVertexId>> GetNeighbours(Vertex<TVertexId> vertex)
        => graph.GetNeighbours(vertex);

        public int GetOutputDegree(Vertex<TVertexId> vertex) => graph.GetOutputDegree(vertex);

        public int GetInputDegree(Vertex<TVertexId> vertex) => graph.GetInputDegree(vertex);

        public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> AsDirected() =>
            graph.AsDirected();

        public IEnumerable<Vertex<TVertexId>> GetVerticesFromGroup(int groupNumber)
        {
            validateGroup(groupNumber);
            return vertexGroupDict.Where(entry => entry.Value == groupNumber)
                                  .Select(entry => entry.Key)
                                  .ToList();
        }

        public Vertex<TVertexId> AddVertex(int groupNumber, TVertexId vertexId,
                                           TVertexProperty property = default) =>
            AddVertex(groupNumber, new Vertex<TVertexId>(vertexId), property);

        public Vertex<TVertexId> AddVertex(int groupNumber, Vertex<TVertexId> vertex,
                                           TVertexProperty property = default)
        {
            validateGroup(groupNumber);

            Vertex<TVertexId> newVertex = graph.AddVertex(vertex, property);

            vertexGroupDict[newVertex] = groupNumber;
            return newVertex;
        }

        public Edge<TVertexId> AddEdgeBetween(Vertex<TVertexId> source, Vertex<TVertexId> destination,
                                              TEdgeProperty property = default) =>
            AddEdge(new Edge<TVertexId>(source, destination), property);

        public Edge<TVertexId> AddEdge(Edge<TVertexId> edge, TEdgeProperty property = default) =>
            areInSameGroup(edge.Source, edge.Destination)
                ? throw new GraphPartitionException("Cannot create an edge between vertices in the same group")
                : graph.AddEdge(edge, property);

        private bool areInSameGroup(Vertex<TVertexId> vertex1, Vertex<TVertexId> vertex2)
        {
            if(!vertexGroupDict.TryGetValue(vertex1, out int group1))
            {
                throw new ArgumentException($"Vertex {vertex1} does not belong to this graph");
            }

            if(!vertexGroupDict.TryGetValue(vertex2, out int group2))
            {
                throw new ArgumentException($"Vertex {vertex2} does not belong to this graph");
            }

            return group1 == group2;
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
        public GraphPartitionException(string message) : base(message)
        {
        }
    }
}
