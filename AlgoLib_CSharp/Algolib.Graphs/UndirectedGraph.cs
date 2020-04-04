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
        public UndirectedSimpleGraph(IEnumerable<V> properties) : base(properties)
        {
        }

        public override int EdgesCount => Vertices.Aggregate(0, (acc, v) => acc + GetOutdegree(v)) / 2;

        public override IEnumerable<Edge<E, V>> Edges =>
            Graphrepr.Aggregate(new List<Edge<E, V>>(),
                                (acc, entry) => {
                                    acc.AddRange(entry.Value.Where(
                                        e => entry.Key.Equals(e.From) ? e.To.Id >= e.From.Id
                                                                      : e.From.Id >= e.To.Id)
                                    );
                                    return acc;
                                });

        public override Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E properties)
        {
            Edge<E, V> edge = new Edge<E, V>(from, to, properties);

            Graphrepr[from].Add(edge);
            Graphrepr[to].Add(edge.Reversed);

            return edge;
        }

        public override int GetIndegree(Vertex<V> v) => GetOutdegree(v);
    }
}
