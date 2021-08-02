// Tests: Structure of tree graph
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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
            resultVertex.Should().Be(vertexProperty);
            resultEdge.Should().Be(edgeProperty);
        }

        [Test]
        public void VerticesCount_ThenNumberOfVertices()
        {
            // when
            long result = testObject.VerticesCount;
            // then
            result.Should().Be(8);
        }

        [Test]
        public void Vertices_ThenAllVertices()
        {
            // when
            IEnumerable<int> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(Enumerable.Range(0, 8));
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
            Edge<int> result = testObject.AddVertex(newVertex, neighbour, vertexProperty, edgeProperty);
            // then
            result.Source.Should().Be(newVertex);
            result.Destination.Should().Be(neighbour);
            testObject.VerticesCount.Should().Be(9);
            testObject.GetNeighbours(newVertex).Should().Equal(neighbour);
            testObject[newVertex].Should().Be(vertexProperty);
            testObject[result].Should().Be(edgeProperty);
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
            result.Should().BeNull();
            testObject.VerticesCount.Should().Be(8);
            testObject[vertex].Should().Be(property);
        }

        [Test]
        public void EdgesCount_ThenNumberOfEdges()
        {
            // when
            long result = testObject.EdgesCount;
            // then
            result.Should().Be(7);
        }

        [Test]
        public void Edges_ThenAllEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 0), new Edge<int>(2, 0), new Edge<int>(3, 0),
                                        new Edge<int>(4, 1), new Edge<int>(5, 1), new Edge<int>(6, 2),
                                        new Edge<int>(7, 2) });
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
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
        }

        [Test]
        public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<int> result = testObject.GetNeighbours(1);
            // then
            result.Should().BeEquivalentTo(new List<int>() { 0, 4, 5 });
        }

        [Test]
        public void GetAdjacentEdges_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(1);
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 0), new Edge<int>(4, 1), new Edge<int>(5, 1) });
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // when
            long result = testObject.GetOutputDegree(1);
            // then
            result.Should().Be(3);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // when
            long result = testObject.GetInputDegree(1);
            // then
            result.Should().Be(3);
        }
    }
}
