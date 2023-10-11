// Algorithms for topological sorting of a graph.
using System;
using System.Collections.Generic;
using System.Linq;
using AlgoLib.Structures;

namespace AlgoLib.Graphs.Algorithms;

public static class TopologicalSorting
{
    /// <summary>Topologically sorts the vertices of given directed acyclic graph using predecessors counting.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex property.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge property.</typeparam>
    /// <param name="graph">The directed acyclic graph.</param>
    /// <returns>The topological order of vertices.</returns>
    /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
    public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
        this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph) =>
        InputsTopologicalSort(graph, Comparer<TVertexId>.Default.Compare);

    /// <summary>Topologically sorts the vertices of given directed acyclic graph using predecessors counting.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex property.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge property.</typeparam>
    /// <param name="graph">The directed acyclic graph.</param>
    /// <param name="comparer">The comparer of vertex identifiers.</param>
    /// <returns>The topological order of vertices.</returns>
    /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
    public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
        this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
        IComparer<TVertexId> comparer) =>
        InputsTopologicalSort(graph, comparer.Compare);

    /// <summary>Topologically sorts the vertices of given directed acyclic graph using predecessors counting.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex property.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge property.</typeparam>
    /// <param name="graph">The directed acyclic graph.</param>
    /// <param name="comparison">The comparison function of vertex identifiers.</param>
    /// <returns>The topological order of vertices.</returns>
    /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
    public static IEnumerable<Vertex<TVertexId>> InputsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
        this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph,
        Comparison<TVertexId> comparison)
    {
        if(graph.EdgesCount == 0)
            return graph.Vertices;

        var order = new List<Vertex<TVertexId>>();
        var inputDegrees = new Dictionary<Vertex<TVertexId>, int>();
        var vertexHeap = new Heap<Vertex<TVertexId>>(
                (vertex1, vertex2) => comparison(vertex1.Id, vertex2.Id));

        foreach(Vertex<TVertexId> vertex in graph.Vertices)
        {
            int degree = graph.GetInputDegree(vertex);

            inputDegrees.Add(vertex, degree);

            if(degree == 0)
                vertexHeap.Push(vertex);
        }

        while(vertexHeap.Count > 0)
        {
            Vertex<TVertexId> vertex = vertexHeap.Pop();

            order.Add(vertex);
            inputDegrees.Remove(vertex);

            foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
            {
                --inputDegrees[neighbour];

                if(inputDegrees[neighbour] == 0)
                    vertexHeap.Push(neighbour);
            }
        }

        return order.Count == graph.VerticesCount
            ? order
            : throw new DirectedCyclicGraphException("Given graph contains a cycle");
    }

    /// <summary>Topologically sorts the vertices of given directed acyclic graph using depth-first search.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex property.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge property.</typeparam>
    /// <param name="graph">The directed acyclic graph.</param>
    /// <returns>The topological order of vertices.</returns>
    /// <exception cref="DirectedCyclicGraphException">If given graph contains a cycle.</exception>
    public static IEnumerable<Vertex<TVertexId>> DfsTopologicalSort<TVertexId, TVertexProperty, TEdgeProperty>(
        this IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
    {
        if(graph.EdgesCount == 0)
            return graph.Vertices;

        var strategy = new TopologicalStrategy<TVertexId>();

        graph.DfsRecursive(strategy, graph.Vertices);
        return strategy.Order.Reverse<Vertex<TVertexId>>();
    }

    private class TopologicalStrategy<TVertexId> : IDfsStrategy<TVertexId>
    {
        public List<Vertex<TVertexId>> Order { get; set; } = new();

        public void ForRoot(Vertex<TVertexId> root)
        {
        }

        public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }

        public void OnEntry(Vertex<TVertexId> vertex)
        {
        }

        public void OnExit(Vertex<TVertexId> vertex) => Order.Add(vertex);

        public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour) =>
            throw new DirectedCyclicGraphException("Given graph contains a cycle");
    }
}
