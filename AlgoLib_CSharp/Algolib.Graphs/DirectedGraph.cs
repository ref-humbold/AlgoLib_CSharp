// Structures of directed graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IDirectedGraph<V, VP, EP> : IGraph<V, VP, EP>
    {
        /// <summary>Reverses directions of edges in this graph.</summary>
        void Reverse();

        /// <returns>the copy of this graph with reversed directions of edges</returns>
        IDirectedGraph<V, VP, EP> ReversedCopy();
    }

    public class DirectedSimpleGraph<V, VP, EP> : SimpleGraph<V, VP, EP>, IDirectedGraph<V, VP, EP>
    {
        public override int EdgesCount => representation.EdgesSet.Sum(edges => edges.Count);

        public override IEnumerable<Edge<V>> Edges => representation.Edges;

        public DirectedSimpleGraph() : base()
        {
        }

        public DirectedSimpleGraph(IEnumerable<V> vertices) : base(vertices)
        {
        }

        public override int GetOutputDegree(V vertex) => representation.GetAdjacentEdges(vertex).Count;

        public override int GetInputDegree(V vertex) =>
            representation.EdgesSet
                          .SelectMany(edges => edges.Where(edge => edge.Destination.Equals(vertex)))
                          .Count();

        public override Edge<V> AddEdge(V source, V destination, EP property = default)
        {
            Edge<V> existingEdge = GetEdge(source, destination);

            if(existingEdge != null)
                return existingEdge;

            Edge<V> newEdge = new Edge<V>(source, destination);

            representation.AddEdgeToSource(newEdge);
            representation[newEdge] = property;
            return newEdge;
        }

        public void Reverse()
        {
            GraphRepresentation<V, VP, EP> newRepresentation = new GraphRepresentation<V, VP, EP>(Vertices);

            foreach(V vertex in Vertices)
                newRepresentation[vertex] = representation[vertex];

            foreach(Edge<V> edge in Edges)
            {
                Edge<V> newEdge = edge.Reversed();

                newRepresentation.AddEdgeToSource(newEdge);
                newRepresentation[newEdge] = representation[edge];
            }

            representation = newRepresentation;
        }

        public IDirectedGraph<V, VP, EP> ReversedCopy()
        {
            DirectedSimpleGraph<V, VP, EP> reversedGraph = new DirectedSimpleGraph<V, VP, EP>(Vertices);

            foreach(V vertex in Vertices)
                reversedGraph[vertex] = this[vertex];

            foreach(Edge<V> edge in Edges)
                reversedGraph.AddEdge(edge.Destination, edge.Source, this[edge]);

            return reversedGraph;
        }
    }
}
