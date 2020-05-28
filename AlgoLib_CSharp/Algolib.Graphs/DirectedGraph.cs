// Structures of directed graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IDirectedGraph<V, E> : IGraph<V, E>
    {
        /// <summary>Reverses direction of edges in this graph.</summary>
        public void Reverse();
    }

    public class DirectedSimpleGraph<V, E> : SimpleGraph<V, E>, IDirectedGraph<V, E>
    {
        public override int EdgesCount => GraphDict.Values.Sum(edges => edges.Count);

        public DirectedSimpleGraph()
        {
        }

        public DirectedSimpleGraph(IEnumerable<V> properties) : base(properties)
        {
        }

        public override Edge<E, V> AddEdge(Vertex<V> source, Vertex<V> destination, E property)
        {
            Edge<E, V> edge = new Edge<E, V>(source, destination, property);

            GraphDict[source].Add(edge);
            return edge;
        }

        public override int GetOutputDegree(Vertex<V> vertex) => GraphDict[vertex].Count;

        public override int GetInputDegree(Vertex<V> vertex) =>
            GraphDict.Values.Aggregate(0, (acc, edges) => acc + edges.Where(e => vertex.Equals(e.Destination)).Count());

        public void Reverse()
        {
            Dictionary<Vertex<V>, HashSet<Edge<E, V>>> revGraphDict = new Dictionary<Vertex<V>, HashSet<Edge<E, V>>>();

            foreach(var v in Vertices)
                revGraphDict.Add(v, new HashSet<Edge<E, V>>());

            foreach(var e in Edges)
                revGraphDict[e.Destination].Add(e.Reverse(prop => prop));

            GraphDict = revGraphDict;
        }

        protected override List<Edge<E, V>> DoGetEdges()
        {
            return GraphDict.Values.Aggregate(new List<Edge<E, V>>(), (acc, edges) => {
                acc.AddRange(edges);
                return acc;
            });
        }
    }
}
