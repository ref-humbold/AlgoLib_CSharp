// Tests: Structure of linear equation.
using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Maths
{
    [TestFixture]
    public class EquationTests
    {
        private Equation testObject;

        [SetUp]
        public void SetUp() => testObject = new Equation(new[] { 2.0, 3.0, 0.0, -2.5 }, 15);

        [Test]
        public void OperatorUnaryPlus_ThenCopied()
        {
            // when
            Equation result = +testObject;
            // then
            result.Should().NotBeSameAs(testObject);
            coefficients(result).Should().Equal(new[] { 2.0, 3.0, 0.0, -2.5 });
            result.FreeTerm.Should().Be(15);
        }

        [Test]
        public void OperatorUnaryMinus_ThenNegated()
        {
            // when
            Equation result = -testObject;
            // then
            coefficients(result).Should().Equal(new[] { -2.0, -3.0, 0.0, 2.5 });
            result.FreeTerm.Should().Be(-15);
        }

        [Test]
        public void OperatorPlus_ThenAddingEquations()
        {
            // when
            Equation result = testObject + new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5.0);
            // then
            coefficients(result).Should().Equal(new[] { 3.0, 2.0, 4.0, 7.5 });
            result.FreeTerm.Should().Be(20);
        }

        [Test]
        public void OperatorMinus_ThenSubtractingEquations()
        {
            // when
            Equation result = testObject - new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5.0);
            // then
            coefficients(result).Should().Equal(new[] { 1.0, 4.0, -4.0, -12.5 });
            result.FreeTerm.Should().Be(10);
        }

        [Test]
        public void OperatorAsterisk_WhenConstantIsNonZero_ThenMultipliedEachCoordinate()
        {
            // when
            Equation result = testObject * 2;
            // then
            coefficients(result).Should().Equal(new[] { 4.0, 6.0, 0.0, -5.0 });
            result.FreeTerm.Should().Be(30);
        }

        [Test]
        public void OperatorAsterisk_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            Action action = () => _ = 0 * testObject;
            // then
            action.Should().Throw<ArithmeticException>();
        }

        [Test]
        public void OperatorSlash_WhenConstantIsNonZero_ThenDividedEachCoordinate()
        {
            // when
            Equation result = testObject / -2;
            // then
            result.Should().NotBeSameAs(testObject);
            coefficients(result).Should().Equal(new[] { -1.0, -1.5, 0.0, 1.25 });
            result.FreeTerm.Should().Be(-7.5);
        }

        [Test]
        public void OperatorSlash_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            Action action = () => _ = testObject / 0;
            // then
            action.Should().Throw<ArithmeticException>();
        }

        [Test]
        public void ToString_ThenStringRepresentation()
        {
            // when
            string result = testObject.ToString();
            // then
            result.Should().Be("2 x_0 + 3 x_1 + -2.5 x_3 = 15");
        }

        [Test]
        public void HasSolution_WhenSolution_ThenTrue()
        {
            // when
            bool result = testObject.HasSolution(new double[] { 10, 10, -29, 14 });
            // then
            result.Should().BeTrue();
        }

        [Test]
        public void HasSolution_WhenNotSolution_ThenFalse()
        {
            // when
            bool result = testObject.HasSolution(new double[] { 10, 6, -17, 14 });
            // then
            result.Should().BeFalse();
        }

        private static double[] coefficients(Equation eq) =>
            Enumerable.Range(0, eq.Count).Select(i => eq[i]).ToArray();
    }
}
