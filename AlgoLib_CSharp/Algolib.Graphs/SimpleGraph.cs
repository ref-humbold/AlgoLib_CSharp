// Structure of simple graph
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public abstract class SimpleGraph<V, VP, EP> : IGraph<V, VP, EP>
    {
        internal GraphRepresentation<V, VP, EP> representation;

        public int VerticesCount => representation.Count;

        public abstract int EdgesCount
        {
            get;
        }

        public IEnumerable<V> Vertices => representation.Vertices;

        public abstract IEnumerable<Edge<V>> Edges
        {
            get;
        }

        public SimpleGraph()
        {
            representation = new GraphRepresentation<V, VP, EP>();
        }

        public SimpleGraph(IEnumerable<V> vertices)
        {
            representation = new GraphRepresentation<V, VP, EP>(vertices);
        }

        public VP this[V vertex]
        {
            get
            {
                return representation[vertex];
            }
            set
            {
                representation[vertex] = value;
            }
        }

        public EP this[Edge<V> edge]
        {
            get
            {
                return representation[edge];
            }
            set
            {
                representation[edge] = value;
            }
        }

        public Edge<V> GetEdge(V source, V destination) =>
            representation.GetAdjacentEdges(source)
                          .Where(edge => edge.GetNeighbour(source).Equals(destination))
                          .FirstOrDefault();

        public IEnumerable<V> GetNeighbours(V vertex) =>
            representation.GetAdjacentEdges(vertex).Select(e => e.GetNeighbour(vertex));

        public IEnumerable<Edge<V>> GetAdjacentEdges(V vertex) => representation.GetAdjacentEdges(vertex);

        public abstract int GetOutputDegree(V vertex);

        public abstract int GetInputDegree(V vertex);

        /// <summary>Adds new vertex with given property to this graph.</summary>
        /// <param name="vertex">a new vertex</param>
        /// <param name="property">a vertex property</param>
        /// <returns><c>true</c> if vertex was added, otherwise <c>false</c></returns>
        public bool AddVertex(V vertex, VP property = default)
        {
            bool wasAdded = representation.AddVertex(vertex);

            if(wasAdded)
                this[vertex] = property;

            return wasAdded;
        }

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="source">source vertex</param>
        /// <param name="destination">destination vertex</param>
        /// <param name="property">an edge property</param>
        /// <returns>the new edge if added, otherwise existing edge</returns>
        public Edge<V> AddEdgeBetween(V source, V destination, EP property = default)
        {
            return AddEdge(new Edge<V>(source, destination), property);
        }

        /// <summary>Adds new edge between given vertices with given property to this graph.</summary>
        /// <param name="edge">a new edge</param>
        /// <param name="property">an edge property</param>
        /// <returns>the new edge if added, otherwise existing edge</returns>
        public abstract Edge<V> AddEdge(Edge<V> edge, EP property = default);
    }
}
