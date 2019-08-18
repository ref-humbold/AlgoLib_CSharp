using System;
using System.Collections.Generic;

namespace Algolib.Graphs.Searching
{
    public interface ISearchingStrategy
    {
        void Preprocess(int vertex);

        void Postprocess(int vertex);

        void OnCycle(int vertex, int neighbour);
    }
}
