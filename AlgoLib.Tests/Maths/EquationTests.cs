﻿// Tests: Structure of linear equation
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
        public void SetUp() => testObject = new Equation(new[] { 2.0, 3.0, 0.0, -2.0 }, 15);

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
            testObject.Coefficients.Should().Equal(new[] { 4.0, 6.0, 0.0, -4.0 });
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
            testObject.Combine(new Equation(new[] { 1.0, -1.0, 4.0, 10.0 }, 5), -2);
            // then
            testObject.Coefficients.Should().Equal(new[] { 0.0, 5.0, -8.0, -22.0 });
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
