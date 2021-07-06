using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Graphs.Algorithms
{
    [TestFixture]
    public class LowestCommonAncestorTests
    {
        private LowestCommonAncestor<int, object, object> testObject;

        [SetUp]
        public void SetUp()
        {
            TreeGraph<int, object, object> tree = new TreeGraph<int, object, object>(0);
            tree.AddVertex(1, 0);
            tree.AddVertex(2, 0);
            tree.AddVertex(3, 1);
            tree.AddVertex(4, 1);
            tree.AddVertex(5, 1);
            tree.AddVertex(6, 2);
            tree.AddVertex(7, 4);
            tree.AddVertex(8, 6);
            tree.AddVertex(9, 6);

            testObject = new LowestCommonAncestor<int, object, object>(tree, 0);
        }

        [Test]
        public void Find_WhenSameVertex_ThenVertexIsLCA()
        {
            // given
            int vertex = 6;
            // when
            int result = testObject.Find(vertex, vertex);
            // then
            result.Should().Be(vertex);
        }

        [Test]
        public void Find_WhenVerticesInDifferentSubtrees_ThenLCA()
        {
            // when
            int result = testObject.Find(5, 7);
            // then
            result.Should().Be(1);
        }

        [Test]
        public void Find_WhenVerticesSwapped_ThenSameLCA()
        {
            // when
            int result1 = testObject.Find(5, 7);
            int result2 = testObject.Find(7, 5);
            // then
            result1.Should().Be(1);
            result2.Should().Be(result1);
        }

        [Test]
        public void Find_WhenRootIsCommonAncestor_ThenRoot()
        {
            // when
            int result = testObject.Find(3, 9);
            // then
            result.Should().Be(testObject.Root);
        }

        [Test]
        public void Find_WhenVerticesAreOnSamePathFromRoot_ThenLCAIsCloserToRoot()
        {
            //given
            int vertex1 = 8;
            int vertex2 = 2;
            // when
            int result = testObject.Find(vertex1, vertex2);
            // then
            result.Should().Be(vertex2);
        }

        [Test]
        public void Find_WhenRootIsOneOfVertices_ThenRoot()
        {
            // when
            int result = testObject.Find(4, testObject.Root);
            // then
            result.Should().Be(testObject.Root);
        }
    }
}
