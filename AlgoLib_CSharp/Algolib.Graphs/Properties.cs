namespace Algolib.Graphs
{
    public interface IWeightProperties
    {
        static double Inf => double.PositiveInfinity;

        double Weight { get; }
    }
}
