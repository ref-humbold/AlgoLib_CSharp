using System;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Structure of linear equations system.
[TestFixture]
public class EquationSystemTests
{
    [Test]
    public void ToString_ThenStringRepresentation()
    {
        // given
        var testObject = new EquationSystem(
            new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
            new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
            new Equation(new[] { -1.0, 6.0, 4.0 }, 9));

        // when
        var result = testObject.ToString();

        // then
        Assert.That(
            result,
            Is.EqualTo(
                "{ 2 x_0 + 3 x_1 + -2 x_2 = 15 ; 7 x_0 + -1 x_1 = 4 ; -1 x_0 + 6 x_1 + 4 x_2 = 9 }"));
    }

    [Test]
    public void Solve_WhenSingleSolution_ThenSolution()
    {
        // given
        var testObject = new EquationSystem(
            new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
            new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
            new Equation(new[] { -1.0, 6.0, 4.0 }, 9));

        // when
        double[] result = testObject.Solve();

        // then
        Assert.That(result, Is.EqualTo(new[] { 1.0, 3.0, -2.0 }));
        Assert.That(testObject.HasSolution(result), Is.True);
        Assert.That(testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }), Is.False);
    }

    [Test]
    public void Solve_WhenNoSolution_ThenNoSolutionException()
    {
        // given
        var testObject = new EquationSystem(
            new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
            new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
            new Equation(new[] { -1.0, -1.5, 1.0 }, -1));

        // when
        Action action = () => testObject.Solve();

        // then
        Assert.That(action, Throws.TypeOf<NoSolutionException>());
        Assert.That(testObject.HasSolution(new[] { 1.0, 3.0, -2.0 }), Is.False);
        Assert.That(testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }), Is.False);
    }

    [Test]
    public void Solve_WhenInfiniteSolutions_ThenInfiniteSolutionsException()
    {
        // given
        var testObject = new EquationSystem(
            new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
            new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
            new Equation(new[] { 4.0, 6.0, -4.0 }, 30));

        // when
        Action action = () => testObject.Solve();

        // then
        Assert.That(action, Throws.TypeOf<InfiniteSolutionsException>());
        Assert.That(testObject.HasSolution(new[] { 1.0, 3.0, -2.0 }), Is.True);
        Assert.That(testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }), Is.True);
    }

    [Test]
    public void Swap_ThenEquationsSwapped()
    {
        // given
        var testObject = new EquationSystem(
            new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
            new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
            new Equation(new[] { -1.0, 6.0, 4.0 }, 9));

        // when
        testObject.Swap(0, 2);

        // then
        Assert.That(testObject[0].ToString(), Is.EqualTo("-1 x_0 + 6 x_1 + 4 x_2 = 9"));
        Assert.That(testObject[2].ToString(), Is.EqualTo("2 x_0 + 3 x_1 + -2 x_2 = 15"));
    }
}
