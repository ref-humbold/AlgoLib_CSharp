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
        public static Dictionary<Vertex<VertexId>, double> BellmanFord<VertexId, VertexProperty, EdgeProperty>(
            IDirectedGraph<VertexId, VertexProperty, EdgeProperty> graph, Vertex<VertexId> source)
                where EdgeProperty : IWeighted
        {
            Dictionary<Vertex<VertexId>, double> distances = new Dictionary<Vertex<VertexId>, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };

            for(int i = 0; i < graph.VerticesCount - 1; ++i)
                foreach(Vertex<VertexId> vertex in graph.Vertices)
                    foreach(Edge<VertexId> edge in graph.GetAdjacentEdges(vertex))
                        distances[edge.Destination] = Math.Min(distances[edge.Destination],
                                                               distances[vertex] + graph[edge].Weight);

            foreach(Vertex<VertexId> vertex in graph.Vertices)
                foreach(Edge<VertexId> edge in graph.GetAdjacentEdges(vertex))
                    if(distances[vertex] < IWeighted.Infinity
                       && distances[vertex] + graph[edge].Weight < distances[edge.Destination])
                        throw new InvalidOperationException("Graph contains a negative cycle");

            return distances;
        }

        /// <summary>Dijkstra algorithm.</summary>
        /// <param name="graph">A graph with weighted edges (weights are not negative)</param>
        /// <param name="source">Source vertex</param>
        /// <returns>Map of vertices' distances</returns>
        public static Dictionary<Vertex<VertexId>, double> Dijkstra<VertexId, VertexProperty, EdgeProperty>(
            IGraph<VertexId, VertexProperty, EdgeProperty> graph, Vertex<VertexId> source)
                where EdgeProperty : IWeighted
        {
            foreach(Edge<VertexId> edge in graph.Edges)
                if(graph[edge].Weight < 0.0)
                    throw new InvalidOperationException("Graph contains an edge with negative weight");

            Dictionary<Vertex<VertexId>, double> distances = new Dictionary<Vertex<VertexId>, double>(
                graph.Vertices.Select(v => KeyValuePair.Create(v, IWeighted.Infinity))) {
                [source] = 0.0
            };
            HashSet<Vertex<VertexId>> visited = new HashSet<Vertex<VertexId>>();
            Heap<(double Distance, Vertex<VertexId> Vertex)> vertexHeap =
                new Heap<(double Distance, Vertex<VertexId> Vertex)>(
                        (pair1, pair2) => pair1.Distance.CompareTo(pair2.Vertex));

            vertexHeap.Push((0.0, source));

            while(vertexHeap.Count > 0)
            {
                Vertex<VertexId> v = vertexHeap.Pop().Vertex;

                if(!visited.Contains(v))
                {
                    visited.Add(v);

                    foreach(Edge<VertexId> e in graph.GetAdjacentEdges(v))
                    {
                        Vertex<VertexId> neighbour = e.GetNeighbour(v);
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
        public static Dictionary<(Vertex<VertexId> Source, Vertex<VertexId> Destination), double>
            FloydWarshall<VertexId, VertexProperty, EdgeProperty>(
                IDirectedGraph<VertexId, VertexProperty, EdgeProperty> graph)
                    where EdgeProperty : IWeighted
        {
            Dictionary<(Vertex<VertexId> Source, Vertex<VertexId> Destination), double> distances =
                new Dictionary<(Vertex<VertexId> Source, Vertex<VertexId> Destination), double>();

            foreach(Vertex<VertexId> v in graph.Vertices)
                foreach(Vertex<VertexId> u in graph.Vertices)
                    distances[(v, u)] = v.Equals(u) ? 0.0 : IWeighted.Infinity;

            foreach(Edge<VertexId> edge in graph.Edges)
                distances[(edge.Source, edge.Destination)] = graph[edge].Weight;

            foreach(Vertex<VertexId> w in graph.Vertices)
                foreach(Vertex<VertexId> v in graph.Vertices)
                    foreach(Vertex<VertexId> u in graph.Vertices)
                        distances[(v, u)] = Math.Min(distances[(v, u)],
                                                     distances[(v, w)] + distances[(w, u)]);

            return distances;
        }
    }
}
