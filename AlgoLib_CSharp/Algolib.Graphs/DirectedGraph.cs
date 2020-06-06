// Structures of directed graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IDirectedGraph<V, E> : IGraph<V, E>
    {
        /// <summary>Reverses direction of edges in this graph.</summary>
        void Reverse();
    }

    public class DirectedSimpleGraph<V, E> : SimpleGraph<V, E>, IDirectedGraph<V, E>
    {
        public override int EdgesCount => graphRepresentation.EdgesSet.Sum(edges => edges.Count);

        public DirectedSimpleGraph() : base()
        {
        }

        public DirectedSimpleGraph(IEnumerable<V> properties) : base(properties)
        {
        }

        public override Edge<E, V> AddEdge(Vertex<V> source, Vertex<V> destination, E property)
        {
            Vertex<V> newSource = source.CompareTo(destination) < 0 ? source : destination;
            Vertex<V> newDestination = source.CompareTo(destination) >= 0 ? source : destination;
            Edge<E, V> edge = new Edge<E, V>(newSource, newDestination, property);

            graphRepresentation.AddEdgeToSource(edge);
            return edge;
        }

        public override int GetOutputDegree(Vertex<V> vertex) => graphRepresentation[vertex].Count;

        public override int GetInputDegree(Vertex<V> vertex) =>
            graphRepresentation.EdgesSet.Aggregate(0,
                (acc, edges) => acc + edges.Where(e => vertex.Equals(e.Destination)).Count());

        public void Reverse()
        {
            IEnumerable<Edge<E, V>> edges = Edges;

            graphRepresentation = new GraphRepresentation<V, E>(Vertices);
            foreach(Edge<E, V> edge in edges)
                graphRepresentation.AddEdgeToSource(new Edge<E, V>(edge.Destination, edge.Source,
                                                                   edge.Property));
        }
    }
}
