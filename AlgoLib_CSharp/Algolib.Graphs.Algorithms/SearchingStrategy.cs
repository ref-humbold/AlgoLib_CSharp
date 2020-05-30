// Strategies for algorithms for graph searching
using System.Collections.Generic;
using System.Linq;

namespace Algolib.Graphs.Algorithms
{
    public interface ISearchingStrategy<V>
    {
        /// <summary>Runs an action before vertex processing.</summary>
        /// <param name="vertex">current vertex</param>
        public void PreProcess(Vertex<V> vertex);

        /// <summary>Runs an action before entering neighbour</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">next vertex</param>
        public void ForNeighbour(Vertex<V> vertex, Vertex<V> neighbour);

        /// <summary>Runs an action after vertex processing</summary>
        /// <param name="vertex">current vertex</param>
        public void PostProcess(Vertex<V> vertex);
    }

    public class EmptyStrategy<V> : ISearchingStrategy<V>
    {
        public void PreProcess(Vertex<V> vertex)
        {
        }

        public void ForNeighbour(Vertex<V> vertex, Vertex<V> neighbour)
        {
        }

        public void PostProcess(Vertex<V> vertex)
        {
        }
    }

    public class TimerStrategy<V, E> : ISearchingStrategy<V>
    {
        private readonly Dictionary<Vertex<V>, int> preTimes;
        private readonly Dictionary<Vertex<V>, int> postTimes;
        private int timer;

        public TimerStrategy(IGraph<V, E> graph)
        {
            timer = 1;
            preTimes = new Dictionary<Vertex<V>, int>(
                graph.Vertices.Select(v => new KeyValuePair<Vertex<V>, int>(v, 0)));
            postTimes = new Dictionary<Vertex<V>, int>(
                graph.Vertices.Select(v => new KeyValuePair<Vertex<V>, int>(v, 0)));
        }

        /// <param name="vertex">a vertex</param>
        /// <returns>preprocess time of vertex</returns>
        public int GetPreTime(Vertex<V> vertex) => preTimes[vertex];

        /// <param name="vertex">a vertex</param>
        /// <returns>postprocess time of vertex</returns>
        public int GetPostTime(Vertex<V> vertex) => postTimes[vertex];

        public void PreProcess(Vertex<V> vertex)
        {
            preTimes[vertex] = timer;
            ++timer;
        }

        public void ForNeighbour(Vertex<V> vertex, Vertex<V> neighbour)
        {
        }

        public void PostProcess(Vertex<V> vertex)
        {
            postTimes[vertex] = timer;
            ++timer;
        }
    }
}
