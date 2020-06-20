using NUnit.Framework;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    [TestFixture]
    public class TreeGraphTests
    {
        private TreeGraph<int, string, string> testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new TreeGraph<int, string, string>(0);
            testObject.AddVertex(1, 0);
            testObject.AddVertex(2, 0);
            testObject.AddVertex(3, 0);
            testObject.AddVertex(4, 1);
            testObject.AddVertex(5, 1);
            testObject.AddVertex(6, 2);
            testObject.AddVertex(7, 2);
        }

        [TearDown]
        public void TearDown()
        {
            testObject = null;
        }

        [Test]
        public void IndexerGetSet_WhenSettingProperty_ThenProperty()
        {
            // given
            string vertexProperty = "x";
            string edgeProperty = "y";
            int vertex = 2;
            Edge<int> edge = testObject.GetEdge(6, 2);
            // when
            testObject[vertex] = vertexProperty;
            testObject[edge] = edgeProperty;

            string resultVertex = testObject[vertex];
            string resultEdge = testObject[edge];
            // then
            Assert.AreEqual(vertexProperty, resultVertex);
            Assert.AreEqual(edgeProperty, resultEdge);
        }

        [Test]
        public void VerticesCount_ThenNumberOfVertices()
        {
            // when
            long result = testObject.VerticesCount;
            // then
            Assert.AreEqual(8, result);
        }

        [Test]
        public void Vertices_ThenAllVertices()
        {
            // when
            IEnumerable<int> result = testObject.Vertices;
            // then
            CollectionAssert.AreEquivalent(new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 }, result);
        }

        [Test]
        public void AddVertex_WhenNewVertex_ThenCreatedEdge()
        {
            // given
            int newVertex = 13;
            int neighbour = 5;
            string vertexProperty = "qwerty";
            string edgeProperty = "asdfgh";
            // when
            Edge<int> result =
                    testObject.AddVertex(newVertex, neighbour, vertexProperty, edgeProperty);
            // then
            Assert.AreEqual(newVertex, result.Source);
            Assert.AreEqual(neighbour, result.Destination);
            Assert.AreEqual(9, testObject.VerticesCount);
            CollectionAssert.AreEquivalent(new List<int>() { neighbour },
                                           testObject.GetNeighbours(newVertex));
            Assert.AreEqual(vertexProperty, testObject[newVertex]);
            Assert.AreEqual(edgeProperty, testObject[result]);
        }

        [Test]
        public void AddVertex_WhenExistingVertex_ThenNull()
        {
            // given
            int vertex = 6;
            string property = "qwerty";

            testObject[vertex] = property;
            // when
            Edge<int> result = testObject.AddVertex(vertex, 2, "abcdefg", "xyz");
            // then
            Assert.IsNull(result);
            Assert.AreEqual(8, testObject.VerticesCount);
            Assert.AreEqual(property, testObject[vertex]);
        }

        [Test]
        public void EdgesCount_ThenNumberOfEdges()
        {
            // when
            long result = testObject.EdgesCount;
            // then
            Assert.AreEqual(7, result);
        }

        [Test]
        public void Edges_ThenAllEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            CollectionAssert.AreEquivalent(
                new List<Edge<int>>() { new Edge<int>(1, 0), new Edge<int>(2, 0), new Edge<int>(3, 0),
                                        new Edge<int>(4, 1), new Edge<int>(5, 1), new Edge<int>(6, 2),
                                        new Edge<int>(7, 2) }, result);
        }

        [Test]
        public void GetEdge_WhenExists_ThenEdge()
        {
            // given
            int source = 5;
            int destination = 1;
            // when
            Edge<int> result = testObject.GetEdge(source, destination);
            // then
            Assert.AreEqual(source, result.Source);
            Assert.AreEqual(destination, result.Destination);
        }

        [Test]
        public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<int> result = testObject.GetNeighbours(1);
            // then
            CollectionAssert.AreEquivalent(new List<int>() { 0, 4, 5 }, result);
        }

        [Test]
        public void GetAdjacentEdges_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(1);
            // then
            CollectionAssert.AreEquivalent(
                new List<Edge<int>>() { new Edge<int>(1, 0), new Edge<int>(4, 1), new Edge<int>(5, 1) },
                result);
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // when
            long result = testObject.GetOutputDegree(1);
            // then
            Assert.AreEqual(3, result);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // when
            long result = testObject.GetInputDegree(1);
            // then
            Assert.AreEqual(3, result);
        }
    }
}
