using System;
using System.Collections.Generic;
using System.Linq;
using Algolib.Structures;

namespace Algolib.Graphs.Algorithms
{
    public static class Paths
    {
        /// <summary>Bellman-Ford algorithm.</summary>
        /// <param name="graph">A directed weighted graph</param>
        /// <param name="source">A source vertex</param>
        /// <returns>Dictionary of distances for vertices</returns>
        public static Dictionary<V, double> BellmanFord<V, VP, EP>(
            IDirectedGraph<V, VP, EP> graph, V source) where EP : IWeighted
        {
            Dictionary<V, double> distances = new Dictionary<V, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };

            for(int i = 0; i < graph.VerticesCount - 1; ++i)
                foreach(V vertex in graph.Vertices)
                    foreach(Edge<V> edge in graph.GetAdjacentEdges(vertex))
                        distances[edge.Destination] = Math.Min(distances[edge.Destination],
                                                               distances[vertex] + graph[edge].Weight);

            foreach(V vertex in graph.Vertices)
                foreach(Edge<V> edge in graph.GetAdjacentEdges(vertex))
                    if(distances[vertex] < IWeighted.Infinity
                       && distances[vertex] + graph[edge].Weight < distances[edge.Destination])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Dijkstra algorithm.</summary>
        /// <param name="graph">A graph with weighted edges (weights are not negative)</param>
        /// <param name="source">Source vertex</param>
        /// <returns>Map of vertices' distances</returns>
        public static Dictionary<V, double> Dijkstra<V, VP, EP>(IGraph<V, VP, EP> graph, V source)
            where EP : IWeighted
        {
            foreach(Edge<V> edge in graph.Edges)
                if(graph[edge].Weight < 0.0)
                    throw new InvalidOperationException("Graph contains an edge with negative weight");

            Dictionary<V, double> distances = new Dictionary<V, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };
            HashSet<V> visited = new HashSet<V>();
            Heap<(double Distance, V Vertex)> vertexHeap = new Heap<(double Distance, V Vertex)>(
                        (pair1, pair2) => pair1.Distance.CompareTo(pair2.Vertex));

            vertexHeap.Push((0.0, source));

            while(vertexHeap.Count > 0)
            {
                V v = vertexHeap.Pop().Vertex;

                if(!visited.Contains(v))
                {
                    visited.Add(v);

                    foreach(Edge<V> e in graph.GetAdjacentEdges(v))
                    {
                        V neighbour = e.GetNeighbour(v);
                        double weight = graph[e].Weight;

                        if(distances[v] + weight < distances[neighbour])
                        {
                            distances[neighbour] = distances[v] + weight;
                            vertexHeap.Push((distances[neighbour], neighbour));
                        }
                    }
                }
            }

            return distances;
        }

        /// <summary>Floyd-Warshall algorithm.</summary>
        /// <param name="graph">A directed weighted graph</param>
        /// <returns>Dictionary of distances for each pair of vertices</returns>
        public static Dictionary<(V Source, V Destination), double> FloydWarshall<V, VP, EP>(
            IDirectedGraph<V, VP, EP> graph) where EP : IWeighted
        {
            Dictionary<(V Source, V Destination), double> distances =
                new Dictionary<(V Source, V Destination), double>();

            foreach(V v in graph.Vertices)
                foreach(V u in graph.Vertices)
                    distances[(v, u)] = v.Equals(u) ? 0.0 : IWeighted.Infinity;

            foreach(Edge<V> edge in graph.Edges)
                distances[(edge.Source, edge.Destination)] = graph[edge].Weight;

            foreach(V w in graph.Vertices)
                foreach(V v in graph.Vertices)
                    foreach(V u in graph.Vertices)
                        distances[(v, u)] = Math.Min(distances[(v, u)],
                                                     distances[(v, w)] + distances[(w, u)]);

            return distances;
        }
    }
}
