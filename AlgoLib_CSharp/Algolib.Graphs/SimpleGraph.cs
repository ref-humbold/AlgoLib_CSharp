// Structure of simple graph
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public abstract class SimpleGraph<V, E> : IGraph<V, E>
    {
        // Adjacency list of graph
        internal GraphRepresentation<V, E> graphRepresentation;

        public int VerticesCount => graphRepresentation.Count;

        public abstract int EdgesCount { get; }

        public IList<Vertex<V>> Vertices
        {
            get
            {
                List<Vertex<V>> vertices = graphRepresentation.Vertices.ToList();

                vertices.Sort();
                return vertices;
            }
        }

        public IList<Edge<E, V>> Edges
        {
            get
            {
                List<Edge<E, V>> edges = graphRepresentation.Edges.ToList();

                edges.Sort();
                return edges;
            }
        }

        public SimpleGraph() : this(Enumerable.Empty<V>())
        {
        }

        public SimpleGraph(IEnumerable<V> properties)
        {
            graphRepresentation = new GraphRepresentation<V, E>();

            foreach(V property in properties)
                AddVertex(property);
        }

        /// <summary>Adds new vertex with given property.</summary>
        /// <param name="property">property of new vertex</param>
        /// <returns>new vertex</returns>
        public Vertex<V> AddVertex(V property)
        {
            return graphRepresentation.AddVertex(property);
        }

        /// <summary>Adds new edge with given property.</summary>
        /// <param name="source">source vertex</param>
        /// <param name="destination">destination vertex</param>
        /// <param name="property">property of new edge</param>
        /// <returns>new edge</returns>
        public abstract Edge<E, V> AddEdge(Vertex<V> source, Vertex<V> destination, E property);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex) =>
            graphRepresentation[vertex].Select(e => vertex.Equals(e.Source) ? e.Destination : e.Source);

        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex) => graphRepresentation[vertex];

        public abstract int GetOutputDegree(Vertex<V> vertex);

        public abstract int GetInputDegree(Vertex<V> vertex);
    }
}
