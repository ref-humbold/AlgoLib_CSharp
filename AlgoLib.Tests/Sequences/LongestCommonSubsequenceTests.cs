// Tests: Algorithm for longest common subsequence
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Text
{
    [TestFixture]
    public class LongestCommonSubsequenceTests
    {
        [Test]
        public void FindLcsLength_WhenEmptySequence_ThenZero()
        {
            // when
            int result = LongestCommonSubsequence.FindLcsLength("qwertyuiop", "");
            // then
            result.Should().Be(0);
        }

        [Test]
        public void FindLcsLength_WhenSameSequence_ThenSequenceLength()
        {
            // given
            string sequence = "qwertyuiop";
            // when
            int result = LongestCommonSubsequence.FindLcsLength(sequence, sequence);
            // then
            result.Should().Be(sequence.Length);
        }

        [Test]
        public void FindLcsLength_WhenSubsequence_ThenSubsequenceLength()
        {
            // when
            int result = LongestCommonSubsequence.FindLcsLength("qwertyuiop", "zxqwertyasdfuiopcvb");
            // then
            result.Should().Be("qwertyuiop".Length);
        }

        [Test]
        public void FindLcsLength_WhenDifferent_ThenZero()
        {
            // when
            int result = LongestCommonSubsequence.FindLcsLength("qwertyuiop", "asdfghjkl");
            // then
            result.Should().Be(0);
        }

        [Test]
        public void FindLcsLength_WhenCommonSubsequence_ThenCommonSubsequenceLength()
        {
            // when
            int result = LongestCommonSubsequence.FindLcsLength("qwertyuiop", "zxrtyasdfuiopcvb");
            // then
            result.Should().Be("rtyuiop".Length);
        }
    }
}
