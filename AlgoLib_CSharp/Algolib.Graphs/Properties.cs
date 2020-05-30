namespace Algolib.Graphs
{
    public interface IWeightProperties
    {
        double Inf => double.PositiveInfinity;

        double Weight { get; }
    }
}
