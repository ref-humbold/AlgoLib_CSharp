using System;
using System.Linq;

namespace AlgoLib.Text
{
    public static class EditDistance
    {
        /// <summary>Computes cost of Levenshtein edit distance between given texts.</summary>
        /// <param name="source">Initial text.</param>
        /// <param name="destination">Final text.</param>
        /// <param name="insertionCost">Cost of insertion operation.</param>
        /// <param name="deletionCost">Cost of deletion operation.</param>
        /// <param name="substitutionCost">Cost of substitution operation.</param>
        /// <returns>Cost of edit distance.</returns>
        /// <exception cref="ArgumentException">If any operation cost is negative.</exception>
        public static double CountLevenshtein(this string source, string destination,
                                              double insertionCost = 1.0, double deletionCost = 1.0,
                                              double substitutionCost = 1.0)
        {
            if(insertionCost < 0 || deletionCost < 0 || substitutionCost < 0)
                throw new ArgumentException("Cost of operation cannot be negative");

            double[] distance = Enumerable.Range(0, destination.Length + 1)
                                          .Select(i => i * insertionCost)
                                          .ToArray();

            foreach(char element in source)
            {
                double previousAbove = distance[0];

                distance[0] += deletionCost;

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

        /// <summary>Computes cost of LCS edit distance between given texts.</summary>
        /// <param name="source">Initial text.</param>
        /// <param name="destination">Final text.</param>
        /// <param name="insertionCost">Cost of insertion operation.</param>
        /// <param name="deletionCost">Cost of deletion operation.</param>
        /// <returns>Cost of edit distance.</returns>
        /// <exception cref="ArgumentException">If any operation cost is negative.</exception>
        public static double CountLcs(this string source, string destination,
                                      double insertionCost = 1.0, double deletionCost = 1.0)
        {
            if(insertionCost < 0 || deletionCost < 0)
                throw new ArgumentException("Cost of operation cannot be negative");

            double[] distance = Enumerable.Range(0, destination.Length + 1)
                                          .Select(i => i * insertionCost)
                                          .ToArray();

            foreach(char element in source)
            {
                double previousAbove = distance[0];

                distance[0] += deletionCost;

                for(int i = 0; i < destination.Length; ++i)
                {
                    double previousDiagonal = previousAbove;

                    previousAbove = distance[i + 1];
                    distance[i + 1] = element == destination[i]
                                      ? previousDiagonal
                                      : Math.Min(previousAbove + deletionCost,
                                                 distance[i] + insertionCost);
                }
            }

            return distance[^1];
        }

        /// <summary>Computes cost of Hamming edit distance between given texts of equal length.</summary>
        /// <param name="source">Initial text.</param>
        /// <param name="destination">Final text.</param>
        /// <param name="substitutionCost">Cost of substitution operation.</param>
        /// <returns>Cost of edit distance.</returns>
        /// <exception cref="ArgumentException">
        /// If any operation cost is negative, or if texts have different length.
        /// </exception>
        public static double CountHamming(this string source, string destination,
                                          double substitutionCost = 1.0)
        {
            if(substitutionCost < 0)
                throw new ArgumentException("Cost of operation cannot be negative");

            if(source.Length != destination.Length)
                throw new ArgumentException("Texts should have equal length");

            return source.Zip(destination)
                         .Where(p => p.First != p.Second)
                         .Select(_ => substitutionCost)
                         .Sum();
        }
    }
}
