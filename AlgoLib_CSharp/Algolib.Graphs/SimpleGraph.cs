// Structure of simple graph
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public abstract class SimpleGraph<V, E> : IGraph<V, E>
    {
        // Adjacency list of graph
        protected Dictionary<Vertex<V>, HashSet<Edge<E, V>>> GraphDict;

        public int VerticesCount => GraphDict.Count;

        public abstract int EdgesCount { get; }

        public IList<Vertex<V>> Vertices
        {
            get
            {
                List<Vertex<V>> vertices = GraphDict.Keys.ToList();

                vertices.Sort();
                return vertices;
            }
        }

        public IList<Edge<E, V>> Edges
        {
            get
            {
                List<Edge<E, V>> edges = DoGetEdges();

                edges.Sort();
                return edges;
            }
        }

        public SimpleGraph()
        {
        }

        public SimpleGraph(IEnumerable<V> properties)
        {
            GraphDict = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            foreach(var prop in properties)
                AddVertex(prop);
        }

        /// <summary>Adds new vertex with given property.</summary>
        /// <param name="property">property of new vertex</param>
        /// <returns>new vertex</returns>
        public Vertex<V> AddVertex(V property)
        {
            Vertex<V> vertex = new Vertex<V>(GraphDict.Count, property);

            GraphDict.Add(vertex, new HashSet<Edge<E, V>>());
            return vertex;
        }

        /// <summary>Adds new edge with given property.</summary>
        /// <param name="from">beginning vertex</param>
        /// <param name="to">ending vertex</param>
        /// <param name="property">property of new edge</param>
        /// <returns>new edge</returns>
        public abstract Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E property);

        public IEnumerable<Vertex<V>> GetNeighbours(Vertex<V> vertex) =>
            GraphDict[vertex].Select(e => vertex.Equals(e.Source) ? e.Destination : e.Source);

        public IEnumerable<Edge<E, V>> GetAdjacentEdges(Vertex<V> vertex) => GraphDict[vertex];

        public abstract int GetOutputDegree(Vertex<V> vertex);

        public abstract int GetInputDegree(Vertex<V> vertex);

        protected abstract List<Edge<E, V>> DoGetEdges();
    }
}
