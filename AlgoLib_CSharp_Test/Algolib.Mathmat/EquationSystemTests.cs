using System;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    // Tests: Structure of linear equations system
    [TestFixture]
    public class EquationSystemTests
    {
        [Test]
        public void Solve_WhenSingleSolution_ThenSolution()
        {
            //given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, 6, 4 }, 9)
            });
            // when
            double[] result = testObject.Solve();
            // then
            result.Should().Equal(new double[] { 1, 3, -2 });
            testObject.IsSolution(result).Should().BeTrue();
            testObject.IsSolution(new double[] { -2, -18, -36.5 }).Should().BeFalse();
        }

        [Test]
        public void Solve_WhenNoSolution_ThenNoSolutionException()
        {
            // given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { -1, -1.5, 1 }, -1)
            });
            // when
            Action action = () => testObject.Solve();
            // then
            action.Should().Throw<NoSolutionException>();
            testObject.IsSolution(new double[] { 1, 3, -2 }).Should().BeFalse();
            testObject.IsSolution(new double[] { -2, -18, -36.5 }).Should().BeFalse();
        }

        [Test]
        public void Solve_WhenInfiniteSolutions_ThenInfiniteSolutionsException()
        {
            // given
            EquationSystem testObject = new EquationSystem(new Equation[] {
                new Equation(new double [] { 2, 3, -2 }, 15),
                new Equation(new double [] { 7, -1, 0 }, 4),
                new Equation(new double[] { 4, 6, -4 }, 30)
            });
            // when
            Action action = () => testObject.Solve();
            // then
            action.Should().Throw<InfiniteSolutionsException>();
            testObject.IsSolution(new double[] { 1, 3, -2 }).Should().BeTrue();
            testObject.IsSolution(new double[] { -2, -18, -36.5 }).Should().BeTrue();
        }
    }
}
