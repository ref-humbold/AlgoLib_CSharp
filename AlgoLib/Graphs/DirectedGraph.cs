// Structures of directed graphs
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        IGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        /// <summary>Reverses directions of edges in this graph.</summary>
        void Reverse();

        /// <returns>the copy of this graph with reversed directions of edges</returns>
        IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> ReversedCopy();
    }

    public class DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>,
        IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        public override int EdgesCount => representation.EdgesSet.Sum(edges => edges.Count);

        public override IEnumerable<Edge<TVertexId>> Edges => representation.Edges;

        public DirectedSimpleGraph() : base()
        {
        }

        public DirectedSimpleGraph(IEnumerable<TVertexId> vertexIds) : base(vertexIds)
        {
        }

        public override int GetOutputDegree(Vertex<TVertexId> vertex) => representation.getAdjacentEdges(vertex).Count();

        public override int GetInputDegree(Vertex<TVertexId> vertex) =>
            representation.EdgesSet
                          .SelectMany(edges => edges.Where(edge => edge.Destination.Equals(vertex)))
                          .Count();

        public override Edge<TVertexId> AddEdge(Edge<TVertexId> edge, TEdgeProperty property = default)
        {
            try
            {
                Edge<TVertexId> existingEdge = this[edge.Source, edge.Destination];

                throw new ArgumentException($"Edge {existingEdge} already exists in the graph");
            }
            catch(KeyNotFoundException)
            {
                representation.addEdgeToSource(edge);
                representation.setProperty(edge, property);
                return edge;
            }
        }

        public void Reverse()
        {
            GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty> newRepresentation =
                new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<TVertexId> vertex in Vertices)
                newRepresentation.setProperty(vertex, representation.getProperty(vertex));

            foreach(Edge<TVertexId> edge in Edges)
            {
                Edge<TVertexId> newEdge = edge.Reversed();

                newRepresentation.addEdgeToSource(newEdge);
                newRepresentation.setProperty(newEdge, representation.getProperty(edge));
            }

            representation = newRepresentation;
        }

        public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> ReversedCopy()
        {
            DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> reversedGraph =
                new DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<TVertexId> vertex in Vertices)
                reversedGraph.Properties[vertex] = Properties[vertex];

            foreach(Edge<TVertexId> edge in Edges)
                reversedGraph.AddEdge(edge.Reversed(), Properties[edge]);

            return reversedGraph;
        }
    }
}
