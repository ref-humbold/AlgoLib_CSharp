using System;
using FluentAssertions;
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
        string result = testObject.ToString();

        // then
        result.Should().Be(
            "{ 2 x_0 + 3 x_1 + -2 x_2 = 15 ; 7 x_0 + -1 x_1 = 4 ; -1 x_0 + 6 x_1 + 4 x_2 = 9 }");
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
        result.Should().Equal(new[] { 1.0, 3.0, -2.0 });
        testObject.HasSolution(result).Should().BeTrue();
        testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeFalse();
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
        action.Should().Throw<NoSolutionException>();
        testObject.HasSolution(new[] { 1.0, 3.0, -2.0 }).Should().BeFalse();
        testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeFalse();
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
        action.Should().Throw<InfiniteSolutionsException>();
        testObject.HasSolution(new[] { 1.0, 3.0, -2.0 }).Should().BeTrue();
        testObject.HasSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeTrue();
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
        testObject[0].ToString().Should().Be("-1 x_0 + 6 x_1 + 4 x_2 = 9");
        testObject[2].ToString().Should().Be("2 x_0 + 3 x_1 + -2 x_2 = 15");
    }
}
