// Strategies for algorithms for graph searching

namespace Algolib.Graphs.Algorithms
{
    public interface IBfsStrategy<TVertexId>
    {
        void ForRoot(Vertex<TVertexId> root);

        void OnEntry(Vertex<TVertexId> vertex);

        void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour);

        void OnExit(Vertex<TVertexId> vertex);
    }

    public interface IDfsStrategy<TVertexId> : IBfsStrategy<TVertexId>
    {
        void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour);
    }

    public struct EmptyStrategy<TVertexId> : IDfsStrategy<TVertexId>
    {
        public void ForRoot(Vertex<TVertexId> root)
        {
        }

        public void OnEntry(Vertex<TVertexId> vertex)
        {
        }

        public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }

        public void OnExit(Vertex<TVertexId> vertex)
        {
        }

        public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }
    }
}
