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

        [TearDown]
        public void TearDown()
        {
            testObject = null;
        }

        [Test]
        public void Find_WhenSameVertex_ThenVertexIsLCA()
        {
            // given
            int vertex = 6;
            // when
            int result = testObject.Find(vertex, vertex);
            // then
            Assert.AreEqual(vertex, result);
        }

        [Test]
        public void Find_WhenVerticesInDifferentSubtrees_ThenLCA()
        {
            // when
            int result = testObject.Find(5, 7);
            // then
            Assert.AreEqual(1, result);
        }

        [Test]
        public void Find_WhenVerticesSwapped_ThenSameLCA()
        {
            // when
            int result1 = testObject.Find(5, 7);
            int result2 = testObject.Find(7, 5);
            // then
            Assert.AreEqual(1, result1);
            Assert.AreEqual(result1, result2);
        }

        [Test]
        public void Find_WhenRootIsCommonAncestor_ThenRoot()
        {
            // when
            int result = testObject.Find(3, 9);
            // then
            Assert.AreEqual(testObject.Root, result);
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
            Assert.AreEqual(vertex2, result);
        }

        [Test]
        public void Find_WhenRootIsOneOfVertices_ThenRoot()
        {
            // when
            int result = testObject.Find(4, testObject.Root);
            // then
            Assert.AreEqual(testObject.Root, result);
        }
    }
}
