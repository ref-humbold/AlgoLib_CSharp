// Strategies for algorithms for graph searching

namespace Algolib.Graphs.Algorithms
{
    public interface IBFSStrategy<V>
    {
        /// <summary>Runs an action before vertex processing.</summary>
        /// <param name="vertex">current vertex</param>
        void OnEnter(Vertex<V> vertex);

        /// <summary>Runs an action before entering neighbour.</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">next vertex</param>
        void OnNextVertex(Vertex<V> vertex, Vertex<V> neighbour);

        /// <summary>Runs an action after vertex processing.</summary>
        /// <param name="vertex">current vertex</param>
        void OnExit(Vertex<V> vertex);
    }

    public interface IDFSStrategy<V> : IBFSStrategy<V>
    {
        /// <summary>Runs an action when edge to a visited neighbour was detected.</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">next vertex</param>
        void OnEdgeToVisited(Vertex<V> vertex, Vertex<V> neighbour);
    }

    public class EmptyStrategy<V> : IDFSStrategy<V>
    {
        public void OnEnter(Vertex<V> vertex)
        {
        }

        public void OnNextVertex(Vertex<V> vertex, Vertex<V> neighbour)
        {
        }

        public void OnExit(Vertex<V> vertex)
        {
        }

        public void OnEdgeToVisited(Vertex<V> vertex, Vertex<V> neighbour)
        {
        }
    }
}
