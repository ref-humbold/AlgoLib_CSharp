namespace Algolib.Graphs
{
    public interface IWeighted
    {
        static double Inf => double.PositiveInfinity;

        double Weight { get; }
    }
}
