using System;
using NUnit.Framework;

namespace Algolib.Mathmat
{
    [TestFixture]
    public class EquationTests
    {
        protected Equation TestObject;

        [SetUp]
        public void SetUp()
        {
            TestObject = new Equation(new double[] { 2, 3, 0, -2 }, 15);
        }

        [TearDown]
        public void TearDown()
        {
            TestObject = null;
        }

        [Test]
        public void ToString_ThenStringRepresentation()
        {
            // when
            string result = TestObject.ToString();
            // then
            Assert.AreEqual("2 x_0 + 3 x_1 + -2 x_3 = 15", result);
        }

        [Test]
        public void Multiply_WhenConstantIsNonZero_ThenMultiplied()
        {
            // when
            TestObject.Multiply(2);
            // then
            Assert.AreEqual(new double[] { 4, 6, 0, -4 }, TestObject.Coefficients);
            Assert.AreEqual(30, TestObject.Free);
        }

        [Test]
        public void Multiply_WhenConstantIsZero_ThenArithmeticException()
        {
            // when - then
            Assert.Throws<ArithmeticException>(() => TestObject.Multiply(0));
        }

        [Test]
        public void Combine_WhenConstantIsNonZero_ThenCombined()
        {
            // when
            TestObject.Combine(new Equation(new double[] { 1, -1, 4, 10 }, 5), -2);
            // then
            Assert.AreEqual(new double[] { 0, 5, -8, -22 }, TestObject.Coefficients);
            Assert.AreEqual(5, TestObject.Free);
        }

        [Test]
        public void Combine_WhenConstantIsZero_ThenArithmeticException()
        {
            // when - then
            Assert.Throws<ArithmeticException>(
                () => TestObject.Combine(new Equation(new double[] { 1, -1, 10, 7 }, 5), 0));
        }
    }
}