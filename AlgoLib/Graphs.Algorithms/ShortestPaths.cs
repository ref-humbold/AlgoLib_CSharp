using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Structures;

namespace AlgoLib.Graphs.Algorithms;

/// <summary>Algorithms for shortest paths in a graph.</summary>
public static class ShortestPaths
{
    /// <summary>
    /// Computes shortest paths in given directed graph from given vertex using Bellman-Ford algorithm.
    /// </summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The directed weighted graph.</param>
    /// <param name="source">The source vertex.</param>
    /// <returns>The dictionary of distances to each vertex.</returns>
    /// <exception cref="InvalidOperationException">If the graph contains a negative cycle.</exception>
    public static Dictionary<Vertex<TVertexId>, double> BellmanFord<TVertexId, TVertexProperty, TEdgeProperty>(
        this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
        Vertex<TVertexId> source)
        where TEdgeProperty : IWeighted
    {
        var distances = new Dictionary<Vertex<TVertexId>, double>(
            graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity)))
        {
            [source] = 0.0
        };

        for(int i = 0; i < graph.VerticesCount - 1; ++i)
            foreach(Vertex<TVertexId> vertex in graph.Vertices)
                foreach(Edge<TVertexId> edge in graph.GetAdjacentEdges(vertex))
                    distances[edge.Destination] =
                        Math.Min(
                            distances[edge.Destination],
                            distances[vertex] + graph.Properties[edge].Weight);

        foreach(Vertex<TVertexId> vertex in graph.Vertices)
            foreach(Edge<TVertexId> edge in graph.GetAdjacentEdges(vertex))
                if(distances[vertex] < IWeighted.Infinity
                   && distances[vertex] + graph.Properties[edge].Weight < distances[edge.Destination])
                    throw new InvalidOperationException("Graph contains a cycle with negative weight");

        return distances;
    }

    /// <summary>Computes shortest paths in given graph from given vertex using Dijkstra algorithm.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The weighted graph with non-negative weights.</param>
    /// <param name="source">The source vertex.</param>
    /// <returns>The dictionary of distances to each vertex.</returns>
    /// <exception cref="InvalidOperationException">
    /// If the graph contains an edge with negative weight.
    /// </exception>
    public static Dictionary<Vertex<TVertexId>, double> Dijkstra<TVertexId, TVertexProperty, TEdgeProperty>(
        this IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, Vertex<TVertexId> source)
        where TEdgeProperty : IWeighted
    {
        foreach(Edge<TVertexId> edge in graph.Edges)
            if(graph.Properties[edge].Weight < 0.0)
                throw new InvalidOperationException("Graph contains an edge with negative weight");

        var distances = new Dictionary<Vertex<TVertexId>, double>(
            graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity)))
        {
            [source] = 0.0
        };
        var visited = new HashSet<Vertex<TVertexId>>();
        var vertexHeap = new Heap<(double Distance, Vertex<TVertexId> Vertex)>(
                    (pair1, pair2) => pair1.Distance.CompareTo(pair2.Distance));

        vertexHeap.Push((0.0, source));

        while(vertexHeap.Count > 0)
        {
            Vertex<TVertexId> vertex = vertexHeap.Pop().Vertex;

            if(!visited.Contains(vertex))
            {
                visited.Add(vertex);

                foreach(Edge<TVertexId> edge in graph.GetAdjacentEdges(vertex))
                {
                    Vertex<TVertexId> neighbour = edge.GetNeighbour(vertex);
                    double weight = graph.Properties[edge].Weight;

                    if(distances[vertex] + weight < distances[neighbour])
                    {
                        distances[neighbour] = distances[vertex] + weight;
                        vertexHeap.Push((distances[neighbour], neighbour));
                    }
                }
            }
        }

        return distances;
    }

    /// <summary>
    /// Computes shortest paths in given directed graph between all vertices using Floyd-Warshall algorithm.
    /// </summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The directed weighted graph.</param>
    /// <returns>The dictionary of distances between each pair of vertices.</returns>
    public static Dictionary<(Vertex<TVertexId> Source, Vertex<TVertexId> Destination), double>
        FloydWarshall<TVertexId, TVertexProperty, TEdgeProperty>(
            this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
            where TEdgeProperty : IWeighted
    {
        var distances = new Dictionary<(Vertex<TVertexId> Source, Vertex<TVertexId> Destination), double>();

        foreach(Vertex<TVertexId> v in graph.Vertices)
            foreach(Vertex<TVertexId> u in graph.Vertices)
                distances[(v, u)] = v.Equals(u) ? 0.0 : IWeighted.Infinity;

        foreach(Edge<TVertexId> edge in graph.Edges)
            distances[(edge.Source, edge.Destination)] = graph.Properties[edge].Weight;

        foreach(Vertex<TVertexId> w in graph.Vertices)
            foreach(Vertex<TVertexId> v in graph.Vertices)
                foreach(Vertex<TVertexId> u in graph.Vertices)
                    distances[(v, u)] = Math.Min(
                        distances[(v, u)], distances[(v, w)] + distances[(w, u)]);

        return distances;
    }
}
