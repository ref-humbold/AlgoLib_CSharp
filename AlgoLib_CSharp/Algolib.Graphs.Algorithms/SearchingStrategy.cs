// Strategies for algorithms for graph searching

namespace Algolib.Graphs.Algorithms
{
    public interface IBfsStrategy<V>
    {
        void ForRoot(V root);

        void OnEnter(V vertex);

        void OnNextVertex(V vertex, V neighbour);

        void OnExit(V vertex);
    }

    public interface IDfsStrategy<V> : IBfsStrategy<V>
    {
        void OnEdgeToVisited(V vertex, V neighbour);
    }

    public class EmptyStrategy<V> : IDfsStrategy<V>
    {
        public void ForRoot(V root)
        {
        }

        public void OnEnter(V vertex)
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
