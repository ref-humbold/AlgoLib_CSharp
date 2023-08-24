// Tests: Structure of linear equation
using System;
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
            result.Coefficients.Should().Equal(new[] { 2.0, 3.0, 0.0, -2.5 });
            result.Free.Should().Be(15);
        }

        [Test]
        public void OperatorUnaryMinus_ThenNegated()
        {
            // when
            Equation result = -testObject;
            // then
            result.Coefficients.Should().Equal(new[] { -2.0, -3.0, 0.0, 2.5 });
            result.Free.Should().Be(-15);
        }

        [Test]
        public void OperatorPlus_ThenAddingEquations()
        {
            // when
            Equation result = testObject + new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5.0);
            // then
            result.Coefficients.Should().Equal(new[] { 3.0, 2.0, 4.0, 7.5 });
            result.Free.Should().Be(20);
        }

        [Test]
        public void OperatorMinus_ThenSubtractingEquations()
        {
            // when
            Equation result = testObject - new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5.0);
            // then
            result.Coefficients.Should().Equal(new[] { 1.0, 4.0, -4.0, -12.5 });
            result.Free.Should().Be(10);
        }

        [Test]
        public void OperatorAsterisk_WhenConstantIsNonZeroOnRight_ThenMultiplied()
        {
            // when
            Equation result = testObject * 2;
            // then
            result.Coefficients.Should().Equal(new[] { 4.0, 6.0, 0.0, -5.0 });
            result.Free.Should().Be(30);
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
        public void OperatorSlash_WhenConstantIsNonZero_ThenMultiplied()
        {
            // when
            Equation result = testObject / -2;
            // then
            result.Should().NotBeSameAs(testObject);
            result.Coefficients.Should().Equal(new[] { -1.0, -1.5, 0.0, 1.25 });
            result.Free.Should().Be(-7.5);
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
        public void Combine_WhenConstantIsNonZero_ThenCombined()
        {
            // when
            testObject.Combine(new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5), -2);
            // then
            testObject.Coefficients.Should().Equal(new[] { 0.0, 5.0, -8.0, -22.5 });
            testObject.Free.Should().Be(5);
        }

        [Test]
        public void Combine_WhenConstantIsZero_ThenArithmeticException()
        {
            // when
            Action action =
                () => testObject.Combine(new Equation(new[] { 1.0, -1.0, 10.0, 7.0 }, 5), 0);
            // then
            action.Should().Throw<ArithmeticException>();
        }
    }
}
