// Strategies for algorithms for graph searching

namespace Algolib.Graphs.Algorithms
{
    public interface IBfsStrategy<VertexId>
    {
        void ForRoot(Vertex<VertexId> root);

        void OnEntry(Vertex<VertexId> vertex);

        void OnNextVertex(Vertex<VertexId> vertex, Vertex<VertexId> neighbour);

        void OnExit(Vertex<VertexId> vertex);
    }

    public interface IDfsStrategy<VertexId> : IBfsStrategy<VertexId>
    {
        void OnEdgeToVisited(Vertex<VertexId> vertex, Vertex<VertexId> neighbour);
    }

    public struct EmptyStrategy<VertexId> : IDfsStrategy<VertexId>
    {
        public void ForRoot(Vertex<VertexId> root)
        {
        }

        public void OnEntry(Vertex<VertexId> vertex)
        {
        }

        public void OnNextVertex(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
        {
        }

        public void OnExit(Vertex<VertexId> vertex)
        {
        }

        public void OnEdgeToVisited(Vertex<VertexId> vertex, Vertex<VertexId> neighbour)
        {
        }
    }
}
