// Structures of undirected graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IUndirectedGraph<V, E> : IGraph<V, E>
    {
    }

    public class UndirectedSimpleGraph<V, E> : SimpleGraph<V, E>, IUndirectedGraph<V, E>
    {
        public override int EdgesCount => graphRepresentation.Edges.Distinct().Count();

        public UndirectedSimpleGraph() : base()
        {
        }

        public UndirectedSimpleGraph(IEnumerable<V> properties) : base(properties)
        {
        }

        public override Edge<E, V> AddEdge(Vertex<V> source, Vertex<V> destination, E property)
        {
            Vertex<V> newSource = source.CompareTo(destination) < 0 ? source : destination;
            Vertex<V> newDestination = source.CompareTo(destination) >= 0 ? source : destination;
            Edge<E, V> edge = new Edge<E, V>(newSource, newDestination, property);

            graphRepresentation.AddEdgeToSource(edge);
            graphRepresentation.AddEdgeToDestination(edge);
            return edge;
        }

        public override int GetOutputDegree(Vertex<V> vertex) => graphRepresentation[vertex].Count;

        public override int GetInputDegree(Vertex<V> vertex) => graphRepresentation[vertex].Count;
    }
}
