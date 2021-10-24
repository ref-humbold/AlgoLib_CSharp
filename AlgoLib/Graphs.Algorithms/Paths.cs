using System;
using System.Collections.Generic;
using System.Linq;
using Algolib.Structures;

namespace AlgoLib.Graphs.Algorithms
{
    public static class Paths
    {
        /// <summary>Bellman-Ford algorithm.</summary>
        /// <param name="graph">A directed weighted graph</param>
        /// <param name="source">A source vertex</param>
        /// <returns>Dictionary of distances for vertices</returns>
        public static Dictionary<Vertex<TVertexId>, double> BellmanFord<TVertexId, TVertexProperty, TEdgeProperty>(
            IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, Vertex<TVertexId> source)
                where TEdgeProperty : IWeighted
        {
            Dictionary<Vertex<TVertexId>, double> distances = new Dictionary<Vertex<TVertexId>, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };

            for(int i = 0; i < graph.VerticesCount - 1; ++i)
                foreach(Vertex<TVertexId> vertex in graph.Vertices)
                    foreach(Edge<TVertexId> edge in graph.GetAdjacentEdges(vertex))
                        distances[edge.Destination] = Math.Min(distances[edge.Destination],
                                                               distances[vertex] + graph.Properties[edge].Weight);

            foreach(Vertex<TVertexId> vertex in graph.Vertices)
                foreach(Edge<TVertexId> edge in graph.GetAdjacentEdges(vertex))
                    if(distances[vertex] < IWeighted.Infinity
                       && distances[vertex] + graph.Properties[edge].Weight < distances[edge.Destination])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Dijkstra algorithm.</summary>
        /// <param name="graph">A graph with weighted edges (weights are not negative)</param>
        /// <param name="source">Source vertex</param>
        /// <returns>Map of vertices' distances</returns>
        public static Dictionary<Vertex<TVertexId>, double> Dijkstra<TVertexId, TVertexProperty, TEdgeProperty>(
            IGraph<TVertexId, TVertexProperty, TEdgeProperty> graph, Vertex<TVertexId> source)
                where TEdgeProperty : IWeighted
        {
            foreach(Edge<TVertexId> edge in graph.Edges)
                if(graph.Properties[edge].Weight < 0.0)
                    throw new InvalidOperationException("Graph contains an edge with negative weight");

            Dictionary<Vertex<TVertexId>, double> distances = new Dictionary<Vertex<TVertexId>, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };
            HashSet<Vertex<TVertexId>> visited = new HashSet<Vertex<TVertexId>>();
            Heap<(double Distance, Vertex<TVertexId> Vertex)> vertexHeap =
                new Heap<(double Distance, Vertex<TVertexId> Vertex)>(
                        (pair1, pair2) => pair1.Distance.CompareTo(pair2.Vertex));

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

        /// <summary>Floyd-Warshall algorithm.</summary>
        /// <param name="graph">A directed weighted graph</param>
        /// <returns>Dictionary of distances for each pair of vertices</returns>
        public static Dictionary<(Vertex<TVertexId> Source, Vertex<TVertexId> Destination), double>
            FloydWarshall<TVertexId, TVertexProperty, TEdgeProperty>(
                IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> graph)
                    where TEdgeProperty : IWeighted
        {
            Dictionary<(Vertex<TVertexId> Source, Vertex<TVertexId> Destination), double> distances =
                new Dictionary<(Vertex<TVertexId> Source, Vertex<TVertexId> Destination), double>();

            foreach(Vertex<TVertexId> v in graph.Vertices)
                foreach(Vertex<TVertexId> u in graph.Vertices)
                    distances[(v, u)] = v.Equals(u) ? 0.0 : IWeighted.Infinity;

            foreach(Edge<TVertexId> edge in graph.Edges)
                distances[(edge.Source, edge.Destination)] = graph.Properties[edge].Weight;

            foreach(Vertex<TVertexId> w in graph.Vertices)
                foreach(Vertex<TVertexId> v in graph.Vertices)
                    foreach(Vertex<TVertexId> u in graph.Vertices)
                        distances[(v, u)] = Math.Min(distances[(v, u)],
                                                     distances[(v, w)] + distances[(w, u)]);

            return distances;
        }
    }
}
