// Tests: Structure of suffix array
using System;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class SuffixArrayTests
    {
        private static readonly string Text = "mississippi";
        private SuffixArray testObject;

        [SetUp]
        public void SetUp() => testObject = new SuffixArray(Text);

        [Test]
        public void Text_ThenText()
        {
            // when
            string result = testObject.Text;
            // then
            result.Should().Be(Text);
        }

        [Test]
        public void Count_ThenNumberOfElements()
        {
            // when
            int result = testObject.Count;
            // then
            result.Should().Be(11);
        }

        [Test]
        public void Indexer_WhenInRange_ThenSuffix()
        {
            // when
            string result0 = testObject[0];
            string result1 = testObject[3];
            string result2 = testObject[6];
            string result3 = testObject[^2];
            // then
            result0.Should().Be("i");
            result1.Should().Be("ississippi");
            result2.Should().Be("ppi");
            result3.Should().Be("ssippi");
        }

        [Test]
        public void Indexer_WhenOutOfRange_ThenIndexOutOfBoundsException()
        {
            // when
            Action action = () => _ = testObject[20];
            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void IndexAt_WhenInRange_ThenIndexInText()
        {
            // when
            int result0 = testObject.IndexAt(0);
            int result1 = testObject.IndexAt(3);
            int result2 = testObject.IndexAt(6);
            int result3 = testObject.IndexAt(^2);
            // then
            result0.Should().Be(10);
            result1.Should().Be(1);
            result2.Should().Be(8);
            result3.Should().Be(5);
        }

        [Test]
        public void IndexAt_WhenOutOfRange_ThenIndexOutOfBoundsException()
        {
            // when
            Action action = () => _ = testObject.IndexAt(20);
            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void IndexOf_WhenInRange_ThenIndexInArray()
        {
            // when
            int result0 = testObject.IndexOf(0);
            int result1 = testObject.IndexOf(3);
            int result2 = testObject.IndexOf(6);
            int result3 = testObject.IndexOf(^2);
            // then
            result0.Should().Be(4);
            result1.Should().Be(8);
            result2.Should().Be(7);
            result3.Should().Be(5);
        }

        [Test]
        public void IndexOf_WhenOutOfRange_ThenIndexOutOfBoundsException()
        {
            // when
            Action action = () => _ = testObject.IndexOf(20);
            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void CountLCP_WhenSameSuffix_ThenLengthOfPrefix()
        {
            // when
            int result = testObject.CountLCP(4, 4);
            // then
            result.Should().Be(7);
        }

        [Test]
        public void CountLCP_WhenFirstEarlierThanSecondSuffix_ThenLengthOfPrefix()
        {
            // when
            int result = testObject.CountLCP(1, ^1);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void CountLCP_WhenFirstFurtherThanSecondSuffix_ThenLengthOfPrefix()
        {
            // when
            int result = testObject.CountLCP(^2, 6);
            // then
            result.Should().Be(0);
        }

        [Test]
        public void CountLCP_WhenSwapSuffix_ThenSameLength()
        {
            // when
            int result0 = testObject.CountLCP(2, 5);
            int result1 = testObject.CountLCP(5, 2);
            // then
            result0.Should().Be(3);
            result1.Should().Be(result0);
        }
    }
}
