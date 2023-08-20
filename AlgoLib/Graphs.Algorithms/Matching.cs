// Hopcroft-Karp algorithm for matching in bipartite graph
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Matching
    {
        /// <summary>Finds maximal matching in given bipartite graph.</summary>
        /// <param name="graph">Bipartite graph.</param>
        /// <returns>Dictionary of matched vertices.</returns>
        public static Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> Match<TVertexId, TVertexProperty, TEdgeProperty>(
            this MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
        {
            var augmenter = new MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>(graph);
            bool wasAugmented = true;

            while(wasAugmented)
                wasAugmented = augmenter.augmentMatch();

            return augmenter.matching;
        }

        private class MatchAugmenter<TVertexId, TVertexProperty, TEdgeProperty>
        {
            internal readonly Dictionary<Vertex<TVertexId>, Vertex<TVertexId>> matching = new();
            private static readonly double infinity = double.PositiveInfinity;
            private readonly MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph;

            internal MatchAugmenter(MultipartiteGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
            {
                if(graph.GroupsCount != 2)
                    throw new ArgumentException("Graph is not bipartite");

                this.graph = graph;
            }

            internal bool augmentMatch()
            {
                var visited = new HashSet<Vertex<TVertexId>>();
                var distances = graph.Vertices.ToDictionary(v => v, v => infinity);

                bfs(distances);
                return unmatchedVertices().Aggregate(false, (acc, v) => dfs(v, visited, distances) || acc);
            }

            private IEnumerable<Vertex<TVertexId>> unmatchedVertices() =>
                graph.GetVerticesFromGroup(1).Where(v => !matching.ContainsKey(v));

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
                        bool isMatched = matching.TryGetValue(neighbour, out Vertex<TVertexId> matched);

                        if(isMatched && distances[matched] == infinity)
                        {
                            distances[matched] = distances[vertex] + 1;
                            vertexDeque.Enqueue(matched);
                        }
                    }
                }
            }

            private bool dfs(Vertex<TVertexId> vertex,
                             HashSet<Vertex<TVertexId>> visited,
                             Dictionary<Vertex<TVertexId>, double> distances)
            {
                visited.Add(vertex);

                foreach(Vertex<TVertexId> neighbour in graph.GetNeighbours(vertex))
                {
                    bool isMatched = matching.TryGetValue(neighbour, out Vertex<TVertexId> matched);

                    if(!isMatched)
                    {
                        matching[vertex] = neighbour;
                        matching[neighbour] = vertex;
                        return true;
                    }

                    if(!visited.Contains(matched) && distances[matched] == distances[vertex] + 1
                            && dfs(matched, visited, distances))
                    {
                        matching[vertex] = neighbour;
                        matching[neighbour] = vertex;
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
