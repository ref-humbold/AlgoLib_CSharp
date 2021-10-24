// Tests: Structure of linear equation
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Mathmat
{
    [TestFixture]
    public class EquationTests
    {
        private Equation testObject;

        [SetUp]
        public void SetUp() => testObject = new Equation(new double[] { 2, 3, 0, -2 }, 15);

        [Test]
        public void ToString_ThenStringRepresentation()
        {
            // when
            string result = testObject.ToString();
            // then
            result.Should().Be("2 x_0 + 3 x_1 + -2 x_3 = 15");
        }

        [Test]
        public void Multiply_WhenConstantIsNonZero_ThenMultiplied()
        {
            // when
            testObject.Multiply(2);
            // then
            testObject.Coefficients.Should().Equal(new double[] { 4, 6, 0, -4 });
            testObject.Free.Should().Be(30);
        }

        [Test]
        public void Multiply_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            Action action = () => testObject.Multiply(0);
            // then
            action.Should().Throw<ArithmeticException>();
        }

        [Test]
        public void Combine_WhenConstantIsNonZero_ThenCombined()
        {
            // when
            testObject.Combine(new Equation(new double[] { 1, -1, 4, 10 }, 5), -2);
            // then
            testObject.Coefficients.Should().Equal(new double[] { 0, 5, -8, -22 });
            testObject.Free.Should().Be(5);
        }

        [Test]
        public void Combine_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            Action action =
                () => testObject.Combine(new Equation(new double[] { 1, -1, 10, 7 }, 5), 0);
            // then
            action.Should().Throw<ArithmeticException>();
        }
    }
}
