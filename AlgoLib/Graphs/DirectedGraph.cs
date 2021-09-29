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

        public override int GetOutputDegree(Vertex<VertexId> vertex) => representation.getAdjacentEdges(vertex).Count;

        public override int GetInputDegree(Vertex<VertexId> vertex) =>
            representation.EdgesSet
                          .SelectMany(edges => edges.Where(edge => edge.Destination.Equals(vertex)))
                          .Count();

        public override Edge<VertexId> AddEdge(Edge<VertexId> edge, EdgeProperty property = default)
        {
            Edge<VertexId> existingEdge = this[edge.Source, edge.Destination];

            if(existingEdge != null)
                return existingEdge;

            representation.addEdgeToSource(edge);
            representation.setProperty(edge, property);
            return edge;
        }

        public void Reverse()
        {
            GraphRepresentation<VertexId, VertexProperty, EdgeProperty> newRepresentation =
                new GraphRepresentation<VertexId, VertexProperty, EdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<VertexId> vertex in Vertices)
                newRepresentation.setProperty(vertex, representation.getProperty(vertex));

            foreach(Edge<VertexId> edge in Edges)
            {
                Edge<VertexId> newEdge = edge.Reversed();

                newRepresentation.addEdgeToSource(newEdge);
                newRepresentation.setProperty(newEdge, representation.getProperty(edge));
            }

            representation = newRepresentation;
        }

        public IDirectedGraph<VertexId, VertexProperty, EdgeProperty> ReversedCopy()
        {
            DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> reversedGraph =
                new DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<VertexId> vertex in Vertices)
                reversedGraph.Properties[vertex] = Properties[vertex];

            foreach(Edge<VertexId> edge in Edges)
                reversedGraph.AddEdge(edge.Reversed(), Properties[edge]);

            return reversedGraph;
        }
    }
}
