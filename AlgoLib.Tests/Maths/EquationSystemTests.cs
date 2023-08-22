// Tests: Structure of linear equations system
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths
{
    [TestFixture]
    public class EquationSystemTests
    {
        [Test]
        public void Solve_WhenSingleSolution_ThenSolution()
        {
            // given
            var testObject = new EquationSystem(
                new[] { new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
                        new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
                        new Equation(new[] { -1.0, 6.0, 4.0 }, 9) });
            // when
            double[] result = testObject.Solve();
            // then
            result.Should().Equal(new[] { 1.0, 3.0, -2.0 });
            testObject.IsSolution(result).Should().BeTrue();
            testObject.IsSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeFalse();
        }

        [Test]
        public void Solve_WhenNoSolution_ThenNoSolutionException()
        {
            // given
            var testObject = new EquationSystem(new Equation[] {
                new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
                new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
                new Equation(new[] { -1.0, -1.5, 1.0 }, -1)
            });
            // when
            Action action = () => testObject.Solve();
            // then
            action.Should().Throw<NoSolutionException>();
            testObject.IsSolution(new[] { 1.0, 3.0, -2.0 }).Should().BeFalse();
            testObject.IsSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeFalse();
        }

        [Test]
        public void Solve_WhenInfiniteSolutions_ThenInfiniteSolutionsException()
        {
            // given
            var testObject = new EquationSystem(new Equation[] {
                new Equation(new[] { 2.0, 3.0, -2.0 }, 15),
                new Equation(new[] { 7.0, -1.0, 0.0 }, 4),
                new Equation(new[] { 4.0, 6.0, -4.0 }, 30)
            });
            // when
            Action action = () => testObject.Solve();
            // then
            action.Should().Throw<InfiniteSolutionsException>();
            testObject.IsSolution(new[] { 1.0, 3.0, -2.0 }).Should().BeTrue();
            testObject.IsSolution(new[] { -2.0, -18.0, -36.5 }).Should().BeTrue();
        }
    }
}
