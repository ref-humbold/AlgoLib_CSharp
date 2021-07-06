namespace Algolib.Graphs
{
    public interface IWeighted
    {
        static double Infinity => double.PositiveInfinity;

        double Weight
        {
            get;
        }
    }
}
