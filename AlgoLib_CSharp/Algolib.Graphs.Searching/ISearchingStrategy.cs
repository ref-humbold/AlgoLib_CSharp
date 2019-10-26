using System;
using System.Collections.Generic;

namespace Algolib.Graphs.Searching
{
    public interface ISearchingStrategy
    {
        /// <summary>Action before vertex processing</summary>
        /// <param name="vertex">current vertex</param>
        void Preprocess(int vertex);

        /// <summary>Action before entering neighbour</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">next vertex</param>
        void ForNeighbour(int vertex, int neighbour);

        /// <summary>Action after vertex processing</summary>
        /// <param name="vertex">current vertex</param>
        void Postprocess(int vertex);

        /// <summary>Action on cycle detectioon</summary>
        /// <param name="vertex">current vertex</param>
        /// <param name="neighbour">neighbouring vertex on cycle</param>
        void OnCycle(int vertex, int neighbour);
    }
}
