using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    internal class Paths<V, E> where E : WeightProperties
    {
        /// <summary>Bellman-Ford algorithm</summary>
        /// <param name="digraph">directed weighted graph</param>
        /// <param name="source">source vertex</param>
        /// <returns>dictionary of distances for vertices</returns>
        public static Dictionary<Vertex<V>, double> BellmanFord(IDirectedGraph<V, E> digraph,
            Vertex<V> source)
        {
            Dictionary<Vertex<V>, double> distances =
                new Dictionary<Vertex<V>, double>(
                    digraph.Vertices.Select(v => KeyValuePair.Create(v, digraph.Inf))) {
                    [source] = 0.0
                };

            for(int u = 0; u < digraph.VerticesCount - 1; ++u)
                foreach(Vertex<V> v in digraph.Vertices)
                    foreach(Edge<E, V> e in digraph.GetAdjacentEdges(v))
                        distances[e.To] = Math.Min(distances[e.To], distances[v] + e.Property.Weight);

            foreach(Vertex<V> v in digraph.Vertices)
                foreach(Edge<E, V> e in digraph.GetAdjacentEdges(v))
                    if(distances[v] < digraph.Inf && distances[v] + e.Property.Weight < distances[e.To])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Floyd-Warshall algorithm</summary>
        /// <param name="digraph">directed weighted graph</param>
        /// <returns>dictionary of distances for each pair of vertices</returns>
        public static Dictionary<Tuple<Vertex<V>, Vertex<V>>, double> FloydWarshall(IDirectedGraph<V, E> digraph)
        {
            Dictionary<Tuple<Vertex<V>, Vertex<V>>, double> distances =
                new Dictionary<Tuple<Vertex<V>, Vertex<V>>, double>();

            foreach(Vertex<V> v in digraph.Vertices)
                foreach(Vertex<V> u in digraph.Vertices)
                    distances[Tuple.Create(v, u)] = v.Equals(u) ? 0.0 : digraph.Inf;

            foreach(Edge<E, V> e in digraph.Edges)
                distances[Tuple.Create(e.From, e.To)] = e.Property.Weight;

            foreach(Vertex<V> w in digraph.Vertices)
                foreach(Vertex<V> v in digraph.Vertices)
                    foreach(Vertex<V> u in digraph.Vertices)
                        distances[Tuple.Create(v, u)] =
                            Math.Min(distances[Tuple.Create(v, u)],
                                     distances[Tuple.Create(v, w)] + distances[Tuple.Create(w, u)]);

            return distances;
        }
    }
}
