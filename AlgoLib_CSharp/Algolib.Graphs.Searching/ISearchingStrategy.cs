namespace Algolib.Graphs.Searching
{
    public interface ISearchingStrategy<V>
    {
        /// <summary>Action before vertex processing</summary>
        /// <param name="vertex">current vertex</param>
        void Preprocess(Vertex<V> vertex);

        /// <summary>Action before entering neighbour</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">next vertex</param>
        void ForNeighbour(Vertex<V> vertex, Vertex<V> neighbour);

        /// <summary>Action after vertex processing</summary>
        /// <param name="vertex">current vertex</param>
        void Postprocess(Vertex<V> vertex);

        /// <summary>Action on cycle detectioon</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">neighbouring vertex on cycle</param>
        void OnCycle(Vertex<V> vertex, Vertex<V> neighbour);
    }
}
