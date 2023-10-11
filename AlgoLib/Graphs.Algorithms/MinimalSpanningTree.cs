// Algorithms for minimal spanning tree.
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Structures;

namespace AlgoLib.Graphs.Algorithms;

public static class MinimalSpanningTree
{
    /// <summary>Computes minimal spanning tree of given undirected graph using Kruskal algorithm.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The undirected weighted graph.</param>
    /// <returns>The minimal spanning tree.</returns>
    public static IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> Kruskal<TVertexId, TVertexProperty, TEdgeProperty>(
        this IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        where TEdgeProperty : IWeighted
    {
        var mst = new UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(
                        graph.Vertices.Select(v => v.Id).ToArray());
        var vertexSets = new DisjointSets<Vertex<TVertexId>>(graph.Vertices);
        var edgeHeap = new Heap<Edge<TVertexId>>(
                (edge1, edge2) => graph.Properties[edge1].Weight.CompareTo(graph.Properties[edge2].Weight));

        foreach(Edge<TVertexId> edge in graph.Edges)
            edgeHeap.Push(edge);

        while(vertexSets.Count > 1 && edgeHeap.Count > 0)
        {
            Edge<TVertexId> edge = edgeHeap.Pop();

            if(!vertexSets.IsSameSet(edge.Source, edge.Destination))
                mst.AddEdge(edge, graph.Properties[edge]);

            vertexSets.UnionSet(edge.Source, edge.Destination);
        }

        return mst;
    }

    /// <summary>Computes minimal spanning tree of given undirected graph using Prim algorithm.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The undirected weighted graph.</param>
    /// <param name="source">The starting vertex.</param>
    /// <returns>The minimal spanning tree.</returns>
    public static IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> Prim<TVertexId, TVertexProperty, TEdgeProperty>(
        IUndirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
        Vertex<TVertexId> source)
        where TEdgeProperty : IWeighted
    {
        var mst = new UndirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(
                graph.Vertices.Select(v => v.Id).ToArray());
        var visited = new HashSet<Vertex<TVertexId>>();
        var heap = new Heap<(Edge<TVertexId>, Vertex<TVertexId>)>(
                (pair1, pair2) => graph.Properties[pair1.Item1].Weight.CompareTo(
                    graph.Properties[pair2.Item1].Weight));

        visited.Add(source);

        foreach(Edge<TVertexId> adjacentEdge in graph.GetAdjacentEdges(source))
        {
            Vertex<TVertexId> neighbour = adjacentEdge.GetNeighbour(source);

            if(neighbour != source)
                heap.Push((adjacentEdge, neighbour));
        }

        while(heap.Count > 0)
        {
            (Edge<TVertexId> edge, Vertex<TVertexId> vertex) = heap.Pop();

            if(!visited.Contains(vertex))
            {
                visited.Add(vertex);
                mst.AddEdge(edge, graph.Properties[edge]);

                foreach(Edge<TVertexId> adjacentEdge in graph.GetAdjacentEdges(vertex))
                {
                    Vertex<TVertexId> neighbour = adjacentEdge.GetNeighbour(vertex);

                    if(!visited.Contains(neighbour))
                        heap.Push((adjacentEdge, neighbour));
                }
            }
        }

        return mst;
    }
}
