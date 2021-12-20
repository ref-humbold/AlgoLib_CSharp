// Tests: Algorithm for longest common subsequence
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class LongestCommonSubsequenceTests
    {
        [Test]
        public void CountLcsLength_WhenEmptySequence_ThenZero()
        {
            // when
            int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "");
            // then
            result.Should().Be(0);
        }

        [Test]
        public void CountLcsLength_WhenRepeatedSingleElement_ThenOne()
        {
            // when
            int result = LongestCommonSubsequence.CountLcsLength("abcde", "eeee");
            // then
            result.Should().Be(1);
        }

        [Test]
        public void CountLcsLength_WhenSameSequence_ThenSequenceLength()
        {
            // given
            string sequence = "qwertyuiop";
            // when
            int result = LongestCommonSubsequence.CountLcsLength(sequence, sequence);
            // then
            result.Should().Be(sequence.Length);
        }

        [Test]
        public void CountLcsLength_WhenSubsequence_ThenSubsequenceLength()
        {
            // when
            int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "zxqwertyasdfuiopcvb");
            // then
            result.Should().Be("qwertyuiop".Length);
        }

        [Test]
        public void CountLcsLength_WhenDifferent_ThenZero()
        {
            // when
            int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "asdfghjkl");
            // then
            result.Should().Be(0);
        }

        [Test]
        public void CountLcsLength_WhenCommonSubsequence_ThenCommonSubsequenceLength()
        {
            // when
            int result = LongestCommonSubsequence.CountLcsLength("qwertyuiop", "zxrtyasdfuiopcvb");
            // then
            result.Should().Be("rtyuiop".Length);
        }
    }
}
