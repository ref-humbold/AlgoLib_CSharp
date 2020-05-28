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
        public override int EdgesCount =>
            GraphDict.Sum(entry => entry.Value.Where(e => entry.Key.Equals(e.Source)).Count());

        public UndirectedSimpleGraph()
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

            GraphDict[source].Add(edge);
            GraphDict[destination].Add(edge);
            return edge;
        }

        public override int GetOutputDegree(Vertex<V> vertex) => GraphDict[vertex].Count;

        public override int GetInputDegree(Vertex<V> vertex) => GraphDict[vertex].Count;

        protected override List<Edge<E, V>> DoGetEdges()
        {
            return GraphDict.Aggregate(new List<Edge<E, V>>(),
                      (acc, entry) => {
                          acc.AddRange(entry.Value.Where(e => entry.Key.Equals(e.Source)));
                          return acc;
                      });
        }
    }
}
