using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgoLib.Maths;

/// <summary>Structure of system of linear equations.</summary>
public class EquationSystem
{
    private readonly Equation[] equations;

    /// <summary>Gets the number of equations in this system.</summary>
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

    /// <summary>Gets the equation at given index.</summary>
    /// <param name="i">The index.</param>
    /// <returns>The equation specified by the index.</returns>
    public Equation this[Index i] => equations[i];

    /// <summary>Gets the equations from given indices range.</summary>
    /// <param name="r">The range of indices.</param>
    /// <returns>The equations specified by the indices range.</returns>
    public IEnumerable<Equation> this[Range r] => equations[r];

    public override string ToString() =>
        $"{{ {string.Join(" ; ", equations.Select(eq => eq.ToString()))} }}";

    /// <summary>Computes solution of this equation system.</summary>
    /// <returns>The solution.</returns>
    /// <exception cref="InfiniteSolutionsException">If infinitely many solutions.</exception>
    /// <exception cref="NoSolutionException">If no solution.</exception>
    public double[] Solve()
    {
        GaussianReduce();

        if(equations[^1].Coefficients[^1] == 0 && equations[^1].FreeTerm == 0)
            throw new InfiniteSolutionsException();

        if(equations[^1].Coefficients[^1] == 0 && equations[^1].FreeTerm != 0)
            throw new NoSolutionException();

        double[] solution = new double[Count];

        solution[^1] = equations[^1].FreeTerm / equations[^1].Coefficients[^1];

        for(int i = Count - 2; i >= 0; --i)
        {
            double value = equations[i].FreeTerm;

            for(int j = Count - 1; j > i; --j)
                value -= equations[i].Coefficients[j] * solution[j];

            solution[i] = value / equations[i].Coefficients[i];
        }

        return solution;
    }

    /// <summary>Runs the Gaussian elimination algorithm on this equation system.</summary>
    public void GaussianReduce()
    {
        for(int i = 0; i < Count - 1; ++i)
        {
            int indexMin = getMinimalCoefficientIndex(i);

            if(equations[indexMin].Coefficients[i] != 0)
            {
                Swap(indexMin, i);

                for(int j = i + 1; j < Count; ++j)
                {
                    double param = -equations[j].Coefficients[i] / equations[i].Coefficients[i];

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
    /// <param name="solution">The values.</param>
    /// <returns><c>true</c> if the solution is correct, otherwise <c>false</c>.</returns>
    public bool HasSolution(double[] solution) => equations.All(eq => eq.HasSolution(solution));

    private int getMinimalCoefficientIndex(int startingIndex)
    {
        int indexMin = startingIndex;

        for(int i = startingIndex + 1; i < Count; ++i)
        {
            double minCoefficient = equations[indexMin].Coefficients[startingIndex];
            double currentCoefficient = equations[i].Coefficients[startingIndex];

            if(currentCoefficient != 0 &&
                    (minCoefficient == 0 || Math.Abs(currentCoefficient) < Math.Abs(minCoefficient)))
                indexMin = i;
        }

        return indexMin;
    }
}
