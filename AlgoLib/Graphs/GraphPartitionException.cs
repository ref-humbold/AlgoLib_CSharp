using System;

namespace AlgoLib.Graphs
{
    [Serializable]
    public class GraphPartitionException : Exception
    {
        public GraphPartitionException(string message)
            : base(message)
        {
        }
    }
}
