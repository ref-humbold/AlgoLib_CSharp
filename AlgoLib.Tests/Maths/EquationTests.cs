using System;
using NUnit.Framework;

namespace AlgoLib.Maths;

// Tests: Structure of linear equation.
[TestFixture]
public class EquationTests
{
    private Equation testObject;

    [SetUp]
    public void SetUp() => testObject = new Equation([2.0, 3.0, 0.0, -2.5], 15);

    [Test]
    public void OperatorUnaryPlus_ThenCopied()
    {
        // when
        Equation result = +testObject;

        // then
        Assert.That(result, Is.Not.SameAs(testObject));
        Assert.That(result.Coefficients, Is.EqualTo([2.0, 3.0, 0.0, -2.5]));
        Assert.That(result.FreeTerm, Is.EqualTo(15));
    }

    [Test]
    public void OperatorUnaryMinus_ThenNegated()
    {
        // when
        Equation result = -testObject;

        // then
        Assert.That(result.Coefficients, Is.EqualTo([-2.0, -3.0, 0.0, 2.5]));
        Assert.That(result.FreeTerm, Is.EqualTo(-15));
    }

    [Test]
    public void OperatorPlus_ThenAddingEquations()
    {
        // when
        Equation result = testObject + new Equation([1.0, -1.0, 4.0, 10.0], 5.0);

        // then
        Assert.That(result.Coefficients, Is.EqualTo([3.0, 2.0, 4.0, 7.5]));
        Assert.That(result.FreeTerm, Is.EqualTo(20));
    }

    [Test]
    public void OperatorMinus_ThenSubtractingEquations()
    {
        // when
        Equation result = testObject - new Equation([1.0, -1.0, 4.0, 10.0], 5.0);

        // then
        Assert.That(result.Coefficients, Is.EqualTo([1.0, 4.0, -4.0, -12.5]));
        Assert.That(result.FreeTerm, Is.EqualTo(10));
    }

    [Test]
    public void OperatorAsterisk_WhenConstantIsNonZero_ThenMultipliedEachCoordinate()
    {
        // when
        Equation result = testObject * 2;

        // then
        Assert.That(result.Coefficients, Is.EqualTo([4.0, 6.0, 0.0, -5.0]));
        Assert.That(result.FreeTerm, Is.EqualTo(30));
    }

    [Test]
    public void OperatorAsterisk_WhenConstantIsZero_ThenArithmeticException()
    {
        // when
        Action action = () => _ = 0 * testObject;

        // then
        Assert.That(action, Throws.TypeOf<ArithmeticException>());
    }

    [Test]
    public void OperatorSlash_WhenConstantIsNonZero_ThenDividedEachCoordinate()
    {
        // when
        Equation result = testObject / -2;

        // then
        Assert.That(result, Is.Not.SameAs(testObject));
        Assert.That(result.Coefficients, Is.EqualTo([-1.0, -1.5, 0.0, 1.25]));
        Assert.That(result.FreeTerm, Is.EqualTo(-7.5));
    }

    [Test]
    public void OperatorSlash_WhenConstantIsZero_ThenArithmeticException()
    {
        // when
        Action action = () => _ = testObject / 0;

        // then
        Assert.That(action, Throws.TypeOf<ArithmeticException>());
    }

    [Test]
    public void ToString_ThenStringRepresentation()
    {
        // when
        string result = testObject.ToString();

        // then
        Assert.That(result, Is.EqualTo("2 x_0 + 3 x_1 + -2.5 x_3 = 15"));
    }

    [Test]
    public void HasSolution_WhenSolution_ThenTrue()
    {
        // when
        bool result = testObject.HasSolution([10, 10, -29, 14]);

        // then
        Assert.That(result, Is.True);
    }

    [Test]
    public void HasSolution_WhenNotSolution_ThenFalse()
    {
        // when
        bool result = testObject.HasSolution([10, 6, -17, 14]);

        // then
        Assert.That(result, Is.False);
    }
}
