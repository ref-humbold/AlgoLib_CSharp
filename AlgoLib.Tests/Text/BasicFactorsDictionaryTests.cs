using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Text;

// Tests: Structure of basic factors dictionary using Karp-Miller-Rosenberg algorithm.
[TestFixture]
public class BasicFactorsDictionaryTests
{
    private BasicFactorsDictionary testObject;

    public static IEnumerable<object[]> ParamsFor_Indexer_WhenRange => [
        [0..1, (2, 0)], [0..3, (7, 6)], [0.., (20, 21)], [1..2, (1, 0)], [3..4, (4, 0)],
        [3..7, (16, 0)], [4..6, (6, 0)], [7.., (12, 0)], [8..9, (3, 0)], [8..10, (9, 0)]
    ];

    public static IEnumerable<Range> ParamsFor_Indexer_WhenInvalidStartAndEndIndices => [
        22.., 0..22, 5..15, 18..28, ^23..3
    ];

    [SetUp]
    public void SetUp() => testObject = new BasicFactorsDictionary("mississippi");

    [TestCaseSource(nameof(ParamsFor_Indexer_WhenRange))]
    public void Indexer_WhenRange_ThenCode(Range range, (int C1, int C2) expected)
    {
        // when
        (int, int) result = testObject[range];

        // then
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void Indexer_WhenStartIndexEqualToEndIndex_ThenArgumentException()
    {
        // when
        Action result = () => _ = testObject[4..4];

        // then
        Assert.That(result, Throws.ArgumentException);
    }

    [Test]
    public void Indexer_WhenStartIndexGreaterThanEndIndex_ThenArgumentException()
    {
        // when
        Action result = () => _ = testObject[6..2];

        // then
        Assert.That(result, Throws.ArgumentException);
    }

    [TestCaseSource(nameof(ParamsFor_Indexer_WhenInvalidStartAndEndIndices))]
    public void Indexer_WhenInvalidStartAndEndIndices_ThenIndexOutOfRangeException(Range range)
    {
        // when
        Action action = () => _ = testObject[range];

        // then
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
    }
}
