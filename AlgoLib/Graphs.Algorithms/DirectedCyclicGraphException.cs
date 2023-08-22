using System;

namespace AlgoLib.Graphs.Algorithms
{
    [Serializable]
    public class DirectedCyclicGraphException : Exception
    {
        public DirectedCyclicGraphException(string message)
            : base(message)
        {
        }
    }
}
