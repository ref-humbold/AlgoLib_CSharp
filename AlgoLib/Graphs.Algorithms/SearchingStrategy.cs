// Strategies for algorithms for graph searching.

namespace AlgoLib.Graphs.Algorithms
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
        public readonly void ForRoot(Vertex<TVertexId> root)
        {
        }

        public readonly void OnEntry(Vertex<TVertexId> vertex)
        {
        }

        public readonly void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }

        public readonly void OnExit(Vertex<TVertexId> vertex)
        {
        }

        public readonly void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }
    }
}
