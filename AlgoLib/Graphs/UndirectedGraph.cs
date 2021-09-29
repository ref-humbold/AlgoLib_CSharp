// Structures of undirected graphs
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs
{
    public interface IUndirectedGraph<VertexId, VertexProperty, EdgeProperty> :
        IGraph<VertexId, VertexProperty, EdgeProperty>
    {
    }

    public class UndirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> :
        SimpleGraph<VertexId, VertexProperty, EdgeProperty>,
        IUndirectedGraph<VertexId, VertexProperty, EdgeProperty>
    {
        public override int EdgesCount => representation.Edges.Distinct().Count();

        public override IEnumerable<Edge<VertexId>> Edges => representation.Edges.Distinct();

        public UndirectedSimpleGraph() : base()
        {
        }

        public UndirectedSimpleGraph(IEnumerable<VertexId> vertexIds) : base(vertexIds)
        {
        }

        public override int GetOutputDegree(Vertex<VertexId> vertex) => representation.getAdjacentEdges(vertex).Count;

        public override int GetInputDegree(Vertex<VertexId> vertex) => representation.getAdjacentEdges(vertex).Count;

        public override Edge<VertexId> AddEdge(Edge<VertexId> edge, EdgeProperty property = default)
        {
            Edge<VertexId> existingEdge = this[edge.Source, edge.Destination];

            if(existingEdge != null)
                return existingEdge;

            representation.addEdgeToSource(edge);
            representation.addEdgeToDestination(edge);
            representation.setProperty(edge, property);
            return edge;
        }

        /// <summary>Converts this graph to a directed graph with same vertices.</summary>
        /// <returns>directed graph</returns>
        public DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> AsDirected()
        {
            DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty> directedSimpleGraph =
                new DirectedSimpleGraph<VertexId, VertexProperty, EdgeProperty>(Vertices.Select(v => v.Id));

            foreach(Vertex<VertexId> vertex in Vertices)
                directedSimpleGraph.SetProperty(vertex, GetProperty(vertex));

            foreach(Edge<VertexId> edge in Edges)
            {
                directedSimpleGraph.AddEdge(edge, GetProperty(edge));
                directedSimpleGraph.AddEdge(edge.Reversed(), GetProperty(edge));
            }

            return directedSimpleGraph;
        }
    }
}
