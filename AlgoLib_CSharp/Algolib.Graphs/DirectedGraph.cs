// Directed graphs structures.
using System.Linq;
using System.Collections.Generic;
using Algolib.Graphs.Properties;

namespace Algolib.Graphs
{
    public interface IDirectedGraph
    {
        /// <summary>Odwracanie skierowania grafu</summary>
        void Reverse();
    }

    public class DirectedSimpleGraph<V, E> : SimpleGraph<V, E>, IDirectedGraph
        where V : IVertexProperties where E : IEdgeProperties
    {
        public DirectedSimpleGraph(IEnumerable<V> properties) : base(properties)
        {
        }

        public override int EdgesCount => Vertices.Aggregate(0, (acc, v) => acc + GetOutdegree(v));

        public override IEnumerable<Edge<E, V>> Edges =>
            Graphrepr.Values.Aggregate(new List<Edge<E, V>>(),
                                       (acc, neighbours) => {
                                           acc.AddRange(neighbours);
                                           return acc;
                                       });

        public override Edge<E, V> AddEdge(Vertex<V> from, Vertex<V> to, E properties)
        {
            Edge<E, V> edge = new Edge<E, V>(from, to, properties);

            Graphrepr[from].Add(edge);

            return edge;
        }

        public override int GetIndegree(Vertex<V> v) =>
            Graphrepr.Values.Aggregate(0, (acc, edges) => acc + edges.Where(e => v.Equals(e.To)).Count());

        public void Reverse()
        {
            Dictionary<Vertex<V>, HashSet<Edge<E, V>>> revgraphrepr = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            foreach(var v in Vertices)
                revgraphrepr.Add(v, new HashSet<Edge<E, V>>());

            foreach(var e in Edges)
                revgraphrepr[e.To].Add(new Edge<E, V>(e.To, e.From, e.Properties));

            Graphrepr = revgraphrepr;
        }
    }
}
