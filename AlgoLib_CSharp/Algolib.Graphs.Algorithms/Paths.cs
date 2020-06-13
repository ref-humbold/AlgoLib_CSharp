using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    internal class Paths<V, VP, EP> where EP : IWeighted
    {
        public static double Infinity = double.PositiveInfinity;

        /// <summary>Bellman-Ford algorithm</summary>
        /// <param name="graph">a directed weighted graph</param>
        /// <param name="source">a source vertex</param>
        /// <returns>dictionary of distances for vertices</returns>
        public static Dictionary<V, double> BellmanFord(IDirectedGraph<V, VP, EP> graph, V source)
        {
            Dictionary<V, double> distances = new Dictionary<V, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, Infinity))) { [source] = 0.0 };

            for(int i = 0; i < graph.VerticesCount - 1; ++i)
                foreach(V vertex in graph.Vertices)
                    foreach(Edge<V> edge in graph.GetAdjacentEdges(vertex))
                        distances[edge.Destination] = Math.Min(distances[edge.Destination],
                                                               distances[vertex] + graph[edge].Weight);

            foreach(V vertex in graph.Vertices)
                foreach(Edge<V> edge in graph.GetAdjacentEdges(vertex))
                    if(distances[vertex] < Infinity
                       && distances[vertex] + graph[edge].Weight < distances[edge.Destination])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Floyd-Warshall algorithm</summary>
        /// <param name="graph">a directed weighted graph</param>
        /// <returns>dictionary of distances for each pair of vertices</returns>
        public static Dictionary<Tuple<V, V>, double> FloydWarshall(IDirectedGraph<V, VP, EP> graph)
        {
            Dictionary<Tuple<V, V>, double> distances = new Dictionary<Tuple<V, V>, double>();

            foreach(V v in graph.Vertices)
                foreach(V u in graph.Vertices)
                    distances[Tuple.Create(v, u)] = v.Equals(u) ? 0.0 : Infinity;

            foreach(Edge<V> edge in graph.Edges)
                distances[Tuple.Create(edge.Source, edge.Destination)] = graph[edge].Weight;

            foreach(V w in graph.Vertices)
                foreach(V v in graph.Vertices)
                    foreach(V u in graph.Vertices)
                        distances[Tuple.Create(v, u)] =
                            Math.Min(distances[Tuple.Create(v, u)],
                                     distances[Tuple.Create(v, w)] + distances[Tuple.Create(w, u)]);

            return distances;
        }
    }
}
