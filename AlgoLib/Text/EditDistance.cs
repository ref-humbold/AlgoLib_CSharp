using System;
using System.Linq;

namespace AlgoLib.Text
{
    public static class EditDistance
    {
        public static double CountLevenshtein(this string source, string destination,
                                              double insertionCost = 1.0, double deletionCost = 1.0,
                                              double substitutionCost = 1.0)
        {
            double[] distance = Enumerable.Range(0, destination.Length + 1)
                                          .Select(i => i * insertionCost)
                                          .ToArray();

            foreach(char element in source)
            {
                double previousAbove = distance[0];
                distance[0] = previousAbove + deletionCost;

                for(int i = 0; i < destination.Length; ++i)
                {
                    double previousDiagonal = previousAbove;

                    previousAbove = distance[i + 1];
                    distance[i + 1] = element == destination[i]
                                      ? previousDiagonal
                                      : Math.Min(Math.Min(previousAbove + deletionCost,
                                                          distance[i] + insertionCost),
                                                 previousDiagonal + substitutionCost);
                }
            }

            return distance[^1];
        }
    }
}
