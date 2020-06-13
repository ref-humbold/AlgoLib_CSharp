// Structures of undirected graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IUndirectedGraph<V, VP, EP> : IGraph<V, VP, EP>
    {
    }

    public class UndirectedSimpleGraph<V, VP, EP> : SimpleGraph<V, VP, EP>, IUndirectedGraph<V, VP, EP>
    {
        public override int EdgesCount => representation.Edges.Distinct().Count();

        public override IEnumerable<Edge<V>> Edges => representation.Edges.Distinct();

        public UndirectedSimpleGraph() : base()
        {
        }

        public UndirectedSimpleGraph(IEnumerable<V> vertices) : base(vertices)
        {
        }

        public override int GetOutputDegree(V vertex) => representation.GetAdjacentEdges(vertex).Count;

        public override int GetInputDegree(V vertex) => representation.GetAdjacentEdges(vertex).Count;

        public override Edge<V> AddEdge(V source, V destination, EP property = default)
        {
            Edge<V> existingEdge = GetEdge(source, destination);

            if(existingEdge != null)
                return existingEdge;

            Edge<V> newEdge = new Edge<V>(source, destination);

            representation.AddEdgeToSource(newEdge);
            representation.AddEdgeToDestination(newEdge);
            representation[newEdge] = property;
            return newEdge;
        }

        /// <summary>Converts this graph to a directed graph with same vertices.</summary>
        /// <returns>directed graph</returns>
        public DirectedSimpleGraph<V, VP, EP> AsDirected()
        {
            DirectedSimpleGraph<V, VP, EP> directedSimpleGraph =
                new DirectedSimpleGraph<V, VP, EP>(Vertices);

            foreach(V vertex in Vertices)
                directedSimpleGraph[vertex] = this[vertex];

            foreach(Edge<V> edge in Edges)
            {
                directedSimpleGraph.AddEdge(edge.Source, edge.Destination, this[edge]);
                directedSimpleGraph.AddEdge(edge.Destination, edge.Source, this[edge]);
            }

            return directedSimpleGraph;
        }
    }
}
