// Undirected graph structures
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    interface IUndirectedGraph
    {
    }

    public class UndirectedSimpleGraph<V, E> : SimpleGraph<V, E>, IUndirectedGraph
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
            Graphrepr[to].Add(edge);

            return edge;
        }

        public override int GetIndegree(Vertex<V> v) => GetOutdegree(v);

    }
}
