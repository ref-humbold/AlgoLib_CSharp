// Strategies for algorithms for graph searching

namespace Algolib.Graphs.Algorithms
{
    public interface IBfsStrategy<V>
    {
        void ForRoot(V root);

        void OnEntry(V vertex);

        void OnNextVertex(V vertex, V neighbour);

        void OnExit(V vertex);
    }

    public interface IDfsStrategy<V> : IBfsStrategy<V>
    {
        void OnEdgeToVisited(V vertex, V neighbour);
    }

    public struct EmptyStrategy<V> : IDfsStrategy<V>
    {
        public void ForRoot(V root)
        {
        }

        public void OnEntry(V vertex)
        {
        }

        public void OnNextVertex(V vertex, V neighbour)
        {
        }

        public void OnExit(V vertex)
        {
        }

        public void OnEdgeToVisited(V vertex, V neighbour)
        {
        }
    }
}
