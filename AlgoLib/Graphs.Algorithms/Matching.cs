// Hopcroft-Karp algorithm for matching in bipartite graph
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Matching
    {
        /// <summary>Finds maximal matching in given bipartite graph.</summary>
        /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
        /// <param name="graph">The bipartite graph.</param>
        /// <returns>The dictionary of matched vertices.</returns>
        public static Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Match<TVertexId, TVertexProperty, TEdgeProperty>(
            this MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var augmenter = new MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>(graph);
            bool wasAugmented = true;

            while(wasAugmented)
                wasAugmented = augmenter.augmentMatch();

            return augmenter.Matching;
        }

        private class MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>
        {
            private static readonly double Infinity = double.PositiveInfinity;
            private readonly MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

            public Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Matching { get; } = new();

            public MatchAugmenter(MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
            {
                if(graph.GroupsCount != 2)
                    throw new ArgumentException("Graph is not bipartite");

                this.graph = graph;
            }

            public bool augmentMatch()
            {
                var visited = new HashSet<Vertex<TVertexId>>();
                var distances = graph.Vertices.ToDictionary(v => v, v => Infinity);

                bfs(distances);
                return unmatchedVertices().Aggregate(false, (acc, v) => dfs(v, visited, distances) || acc);
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

                        if(isMatched && distances[matched] == Infinity)
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

                    if(!isMatched)
                    {
                        Matching[vertex] = neighbour;
                        Matching[neighbour] = vertex;
                        return true;
                    }

                    if(!visited.Contains(matched) && distances[matched] == distances[vertex] + 1
                            && dfs(matched, visited, distances))
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
}
