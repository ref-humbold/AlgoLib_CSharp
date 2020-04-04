// Structure of linear equations system
using System;
using System.Linq;

namespace Algolib.Mathmat
{
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

    public class EquationSystem
    {
        private readonly Equation[] equations;

        public EquationSystem(Equation[] equations)
        {
            this.equations = equations;

            foreach(Equation eq in equations)
                if(eq.Count != equations.Length)
                    throw new ArgumentException($"Equation {eq} has {eq.Count} variables, but was "
                                                + $" expected to have {equations.Length}");
        }

        public int Count => equations.Length;

        /// <param name="i">index of equation</param>
        /// <returns>i-th equation of this system</returns>
        public Equation this[int i] => equations[i];

        public override string ToString() =>
            $"{{ {string.Join(" ; ", equations.Select(eq => eq.ToString()))} }}";

        /// <summary>Solves this equation system.</summary>
        /// <exception cref="InfiniteSolutionsException">if there are infinitely many solutions</exception>
        /// <exception cref="NoSolutionException">if there is no solution</exception>
        public double[] Solve()
        {
            GaussianReduce();

            if(equations[Count - 1][Count - 1] == 0 && equations[Count - 1].Free == 0)
                throw new InfiniteSolutionsException();

            if(equations[Count - 1][Count - 1] == 0 && equations[Count - 1].Free != 0)
                throw new NoSolutionException();

            double[] solution = new double[Count];

            solution[Count - 1] = equations[Count - 1].Free / equations[Count - 1][Count - 1];

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
        /// <param name="i">index of first equation</param>
        /// <param name="j">index of second equation</param>
        public void Swap(int i, int j)
        {
            Equation temp = equations[i];

            equations[i] = equations[j];
            equations[j] = temp;
        }

        /// <summary>Checks whether given values solve this equation system.</summary>
        /// <param name="solution">values to check</param>
        /// <returns>
        /// <code>true</code>
        /// if solution is correct, otherwise
        /// <code>false</code>
        /// .
        /// </returns>
        public bool IsSolution(double[] solution) => equations.All(eq => eq.IsSolution(solution));
    }
}
