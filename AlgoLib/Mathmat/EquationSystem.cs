// Structure of linear equations system
using System;
using System.Linq;

namespace AlgoLib.Mathmat
{
    public class EquationSystem
    {
        private readonly Equation[] equations;

        /// <summary>Number of equations.</summary>
        public int Count => equations.Length;

        public EquationSystem(Equation[] equations)
        {
            this.equations = equations;

            foreach(Equation eq in equations)
                if(eq.Count != equations.Length)
                    throw new ArgumentException($"Equation {eq} has {eq.Count} variables, but was "
                                                + $" expected to have {equations.Length}");
        }

        /// <param name="i">Index of equation</param>
        /// <returns>i-th equation of this system</returns>
        public Equation this[Index i] => equations[i];

        public override string ToString() =>
            $"{{ {string.Join(" ; ", equations.Select(eq => eq.ToString()))} }}";

        /// <summary>Solves this equation system.</summary>
        /// <exception cref="InfiniteSolutionsException">If there are infinitely many solutions</exception>
        /// <exception cref="NoSolutionException">If there is no solution</exception>
        public double[] Solve()
        {
            GaussianReduce();

            if(equations[^1][^1] == 0 && equations[^1].Free == 0)
                throw new InfiniteSolutionsException();

            if(equations[^1][^1] == 0 && equations[^1].Free != 0)
                throw new NoSolutionException();

            double[] solution = new double[Count];

            solution[^1] = equations[^1].Free / equations[^1][^1];

            for(int i = Count - 2; i >= 0; --i)
            {
                double value = equations[i].Free;

                for(int j = Count - 1; j > i; --j)
                    value -= equations[i][j] * solution[j];

                solution[i] = value / equations[i][i];
            }

            return solution;
        }

        /// <summary>Runs the Gauss elimination algorithm on this equation system.</summary>
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
                            equations[j].Combine(equations[i], param);
                    }
                }
            }
        }

        /// <summary>Swaps two equations in this system.</summary>
        /// <param name="i">Index of first equation</param>
        /// <param name="j">Index of second equation</param>
        public void Swap(Index i, Index j)
        {
            Equation temp = equations[i];

            equations[i] = equations[j];
            equations[j] = temp;
        }

        /// <summary>Checks whether given values solve this equation system.</summary>
        /// <param name="solution">Values to check</param>
        /// <returns><c>true</c> if solution is correct, otherwise <c>false</c></returns>
        public bool IsSolution(double[] solution) => equations.All(eq => eq.IsSolution(solution));
    }

    public class NoSolutionException : Exception
    {
        public NoSolutionException() : base()
        {
        }
    }

    public class InfiniteSolutionsException : Exception
    {
        public InfiniteSolutionsException() : base()
        {
        }
    }
}
