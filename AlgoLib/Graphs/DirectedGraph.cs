// Structure of directed graph.
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Graphs
{
    public interface IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        IGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        /// <summary>Reverses directions of all edges in this graph.</summary>
        void Reverse();

        /// <summary>Returns reversed copy of this graph.</summary>
        /// <returns>The copy of this graph with reversed directions of all edges.</returns>
        IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> ReversedCopy();
    }

    public class DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty> :
        SimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>,
        IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty>
    {
        public override int EdgesCount => Representation.EdgesSet.Sum(edges => edges.Count);

        public override IEnumerable<Edge<TVertexId>> Edges => Representation.Edges;

        public DirectedSimpleGraph()
            : base()
        {
        }

        public DirectedSimpleGraph(IEnumerable<TVertexId> vertexIds)
            : base(vertexIds)
        {
        }

        public override int GetOutputDegree(Vertex<TVertexId> vertex) =>
            Representation.getAdjacentEdges(vertex).Count();

        public override int GetInputDegree(Vertex<TVertexId> vertex) =>
            Representation.EdgesSet
                          .SelectMany(edges => edges.Where(edge => edge.Destination.Equals(vertex)))
                          .Count();

        public override Edge<TVertexId> AddEdge(
            Edge<TVertexId> edge, TEdgeProperty property = default)
        {
            try
            {
                Edge<TVertexId> existingEdge = this[edge.Source, edge.Destination];

                throw new ArgumentException($"Edge {existingEdge} already exists in the graph");
            }
            catch(KeyNotFoundException)
            {
                Representation.addEdgeToSource(edge);
                Representation.setProperty(edge, property);
                return edge;
            }
        }

        public void Reverse()
        {
            var newRepresentation = new GraphRepresentation<TVertexId, TVertexProperty, TEdgeProperty>(
                Vertices.Select(v => v.Id));

            foreach(Vertex<TVertexId> vertex in Vertices)
                newRepresentation.setProperty(vertex, Representation.getProperty(vertex));

            foreach(Edge<TVertexId> edge in Edges)
            {
                Edge<TVertexId> newEdge = edge.Reversed();

                newRepresentation.addEdgeToSource(newEdge);
                newRepresentation.setProperty(newEdge, Representation.getProperty(edge));
            }

            Representation = newRepresentation;
        }

        public IDirectedGraph<TVertexId, TVertexProperty, TEdgeProperty> ReversedCopy()
        {
            var reversedGraph = new DirectedSimpleGraph<TVertexId, TVertexProperty, TEdgeProperty>(
                Vertices.Select(v => v.Id));

            foreach(Vertex<TVertexId> vertex in Vertices)
                reversedGraph.Properties[vertex] = Properties[vertex];

            foreach(Edge<TVertexId> edge in Edges)
                reversedGraph.AddEdge(edge.Reversed(), Properties[edge]);

            return reversedGraph;
        }
    }
}
