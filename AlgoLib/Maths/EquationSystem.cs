// Structure of linear equations system
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths
{
    public class EquationSystem
    {
        private readonly Equation[] equations;

        /// <summary>Gets the number of equations.</summary>
        /// <value>The number of equations.</value>
        public int Count => equations.Length;

        public EquationSystem(params Equation[] equations)
        {
            this.equations = equations;

            foreach(Equation eq in equations)
                if(eq.Count != equations.Length)
                    throw new ArgumentException(
                        $"Equation {eq} has {eq.Count} variables, but was expected to have {equations.Length}");
        }

        /// <summary>Gets the equation of given index.</summary>
        /// <param name="i">The index of equation.</param>
        /// <returns>The equation of this system specified by given index.</returns>
        public Equation this[Index i] => equations[i];

        /// <summary>Gets the equations from given indices range.</summary>
        /// <param name="r">Range of indices in equation system.</param>
        /// <returns>The equations of this system specified by given indices range.</returns>
        public IEnumerable<Equation> this[Range r] => equations[r];

        public override string ToString() =>
            $"{{ {string.Join(" ; ", equations.Select(eq => eq.ToString()))} }}";

        /// <summary>Solves this equation system.</summary>
        /// <returns>The solution.</returns>
        /// <exception cref="InfiniteSolutionsException">If there are infinitely many solutions.</exception>
        /// <exception cref="NoSolutionException">If there is no solution.</exception>
        public double[] Solve()
        {
            GaussianReduce();

            if(equations[^1][^1] == 0 && equations[^1].FreeTerm == 0)
                throw new InfiniteSolutionsException();

            if(equations[^1][^1] == 0 && equations[^1].FreeTerm != 0)
                throw new NoSolutionException();

            double[] solution = new double[Count];

            solution[^1] = equations[^1].FreeTerm / equations[^1][^1];

            for(int i = Count - 2; i >= 0; --i)
            {
                double value = equations[i].FreeTerm;

                for(int j = Count - 1; j > i; --j)
                    value -= equations[i][j] * solution[j];

                solution[i] = value / equations[i][i];
            }

            return solution;
        }

        /// <summary>Runs the Gaussian elimination algorithm on this equation system.</summary>
        public void GaussianReduce()
        {
            for(int i = 0; i < Count - 1; ++i)
            {
                int indexMin = i;

                for(int j = i + 1; j < Count; ++j)
                {
                    double minCoef = equations[indexMin][i];
                    double actCoef = equations[j][i];

                    if(actCoef != 0 && (minCoef == 0 || Math.Abs(actCoef) < Math.Abs(minCoef)))
                        indexMin = j;
                }

                if(equations[indexMin][i] != 0)
                {
                    Swap(indexMin, i);

                    for(int j = i + 1; j < Count; ++j)
                    {
                        double param = -equations[j][i] / equations[i][i];

                        if(param != 0)
                            equations[j] += equations[i] * param;
                    }
                }
            }
        }

        /// <summary>Swaps two equations in this system.</summary>
        /// <param name="i">The index of first equation.</param>
        /// <param name="j">The index of second equation.</param>
        public void Swap(Index i, Index j) =>
            (equations[j], equations[i]) = (equations[i], equations[j]);

        /// <summary>Checks whether given values solve this equation system.</summary>
        /// <param name="solution">Values.</param>
        /// <returns><c>true</c> if solution is correct, otherwise <c>false</c>.</returns>
        public bool HasSolution(double[] solution) => equations.All(eq => eq.HasSolution(solution));
    }
}
