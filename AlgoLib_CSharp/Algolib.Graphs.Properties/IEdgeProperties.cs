using System;
using System.Collections.Generic;
using System.Text;

namespace Algolib.Graphs.Properties
{
    public interface IEdgeProperties
    {
    }

    public class WeightedEdgeProperties : IEdgeProperties
    {
        public double Weight { get; }

        public WeightedEdgeProperties(double weight)
        {
            this.Weight = weight;
        }
    }
}
