// Structures of directed graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IDirectedGraph<VertexId, VertexProperty, EdgeProperty> : IGraph<VertexId, VertexProperty, EdgeProperty>
    {
        /// <summary>Reverses directions of edges in this graph.</summary>
        void Reverse();

        /// <returns>the copy of this graph with reversed directions of edges</returns>
        IDirectedGraph<VertexId, VertexProperty, EdgeProperty> ReversedCopy();
    }

    public class DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> :
        SimpleGraph<VertexId, VertexProperty, EdgeProperty>,
        IDirectedGraph<VertexId, VertexProperty, EdgeProperty>
    {
        public override int EdgesCount => representation.EdgesSet.Sum(edges => edges.Count);

        public override IEnumerable<Edge<VertexId>> Edges => representation.Edges;

        public DirectedSimpleGraph() : base()
        {
        }

        public DirectedSimpleGraph(IEnumerable<VertexId> vertexIds) : base(vertexIds)
        {
        }

        public override int GetOutputDegree(Vertex<VertexId> vertex) => representation.GetAdjacentEdges(vertex).Count;

        public override int GetInputDegree(Vertex<VertexId> vertex) =>
            representation.EdgesSet
                          .SelectMany(edges => edges.Where(edge => edge.Destination.Equals(vertex)))
                          .Count();

        public override Edge<VertexId> AddEdge(Edge<VertexId> edge, EdgeProperty property = default)
        {
            Edge<VertexId> existingEdge = GetEdge(edge.Source, edge.Destination);

            if(existingEdge != null)
                return existingEdge;

            representation.AddEdgeToSource(edge);
            representation[edge] = property;
            return edge;
        }

        public void Reverse()
        {
            GraphRepresentation<VertexId, VertexProperty, EdgeProperty> newRepresentation =
                new GraphRepresentation<VertexId, VertexProperty, EdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<VertexId> vertex in Vertices)
                newRepresentation[vertex] = representation[vertex];

            foreach(Edge<VertexId> edge in Edges)
            {
                Edge<VertexId> newEdge = edge.Reversed();

                newRepresentation.AddEdgeToSource(newEdge);
                newRepresentation[newEdge] = representation[edge];
            }

            representation = newRepresentation;
        }

        public IDirectedGraph<VertexId, VertexProperty, EdgeProperty> ReversedCopy()
        {
            DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> reversedGraph =
                new DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<VertexId> vertex in Vertices)
                reversedGraph[vertex] = this[vertex];

            foreach(Edge<VertexId> edge in Edges)
                reversedGraph.AddEdge(edge.Reversed(), this[edge]);

            return reversedGraph;
        }
    }
}
