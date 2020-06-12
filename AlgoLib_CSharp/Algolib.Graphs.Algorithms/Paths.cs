using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    internal class Paths<V, E> where E : IWeighted
    {
        public const double Infinity = double.PositiveInfinity;

        /// <summary>Bellman-Ford algorithm</summary>
        /// <param name="graph">a directed weighted graph</param>
        /// <param name="source">a source vertex</param>
        /// <returns>dictionary of distances for vertices</returns>
        public static Dictionary<Vertex<V>, double> BellmanFord(IDirectedGraph<V, E> graph,
            Vertex<V> source)
        {
            Dictionary<Vertex<V>, double> distances =
                new Dictionary<Vertex<V>, double>(
                    graph.Vertices.Select(v => KeyValuePair.Create(v, Infinity))) {
                    [source] = 0.0
                };

            for(int i = 0; i < graph.VerticesCount - 1; ++i)
                foreach(Vertex<V> vertex in graph.Vertices)
                    foreach(Edge<E, V> edge in graph.GetAdjacentEdges(vertex))
                        distances[edge.Destination] = Math.Min(distances[edge.Destination],
                                                               distances[vertex] + edge.Property.Weight);

            foreach(Vertex<V> vertex in graph.Vertices)
                foreach(Edge<E, V> edge in graph.GetAdjacentEdges(vertex))
                    if(distances[vertex] < Infinity
                        && distances[vertex] + edge.Property.Weight < distances[edge.Destination])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Floyd-Warshall algorithm</summary>
        /// <param name="graph">a directed weighted graph</param>
        /// <returns>dictionary of distances for each pair of vertices</returns>
        public static Dictionary<Tuple<Vertex<V>, Vertex<V>>, double> FloydWarshall(IDirectedGraph<V, E> graph)
        {
            Dictionary<Tuple<Vertex<V>, Vertex<V>>, double> distances =
                new Dictionary<Tuple<Vertex<V>, Vertex<V>>, double>();

            foreach(Vertex<V> v in graph.Vertices)
                foreach(Vertex<V> u in graph.Vertices)
                    distances[Tuple.Create(v, u)] = v.Equals(u) ? 0.0 : Infinity;

            foreach(Edge<E, V> e in graph.Edges)
                distances[Tuple.Create(e.Source, e.Destination)] = e.Property.Weight;

            foreach(Vertex<V> w in graph.Vertices)
                foreach(Vertex<V> v in graph.Vertices)
                    foreach(Vertex<V> u in graph.Vertices)
                        distances[Tuple.Create(v, u)] =
                            Math.Min(distances[Tuple.Create(v, u)],
                                     distances[Tuple.Create(v, w)] + distances[Tuple.Create(w, u)]);

            return distances;
        }
    }
}
