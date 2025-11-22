using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms;

/// <summary>Hopcroft-Karp algorithm for matching in a bipartite graph.</summary>
public static class Matching
{
    /// <summary>Computes maximal matching in given bipartite graph.</summary>
    /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
    /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
    /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
    /// <param name="graph">The bipartite graph.</param>
    /// <returns>The dictionary of matched vertices.</returns>
    public static Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Match<TVertexId, TVertexProperty,
        TEdgeProperty>(this MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
    {
        var augmenter = new MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>(graph);
        var wasAugmented = true;

        while(wasAugmented)
            wasAugmented = augmenter.AugmentMatch();

        return augmenter.Matching;
    }

    private class MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>
    {
        private const double Infinity = double.PositiveInfinity;
        private readonly MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

        public Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Matching { get; } = new();

        public MatchAugmenter(MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            if(graph.GroupsCount != 2)
                throw new ArgumentException("Graph is not bipartite");

            this.graph = graph;
        }

        public bool AugmentMatch()
        {
            HashSet<Vertex<TVertexId>> visited = [];
            Dictionary<Vertex<TVertexId>, double> distances =
                graph.Vertices.ToDictionary(v => v, _ => Infinity);

            bfs(distances);
            return unmatchedVertices().Aggregate(
                false, (acc, v) => dfs(v, visited, distances) || acc);
        }

        private IEnumerable<Vertex<TVertexId>> unmatchedVertices() =>
            graph.GetVerticesFromGroup(1).Where(v => !Matching.ContainsKey(v));

        private void bfs(Dictionary<Vertex<TVertexId>, double> distances)
        {
            var vertexDeque = new Queue<Vertex<TVertexId>>();

            foreach(Vertex<TVertexId> vertex in unmatchedVertices())
            {
                distances[vertex] = 0.0;
                vertexDeque.Enqueue(vertex);
            }

            while(vertexDeque.Count > 0)
            {
                Vertex<TVertexId> vertex = vertexDeque.Dequeue();

                foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
                {
                    bool isMatched = Matching.TryGetValue(neighbour, out Vertex<TVertexId> matched);

                    if(isMatched && double.IsPositiveInfinity(distances[matched]))
                    {
                        distances[matched] = distances[vertex] + 1;
                        vertexDeque.Enqueue(matched);
                    }
                }
            }
        }

        private bool dfs(
            Vertex<TVertexId> vertex,
            HashSet<Vertex<TVertexId>> visited,
            Dictionary<Vertex<TVertexId>, double> distances)
        {
            visited.Add(vertex);

            foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
            {
                bool isMatched = Matching.TryGetValue(neighbour, out Vertex<TVertexId> matched);

                if(!isMatched || (!visited.Contains(matched) &&
                       distances[matched] == distances[vertex] + 1
                       && dfs(matched, visited, distances)))
                {
                    Matching[vertex] = neighbour;
                    Matching[neighbour] = vertex;
                    return true;
                }
            }

            return false;
        }
    }
}
