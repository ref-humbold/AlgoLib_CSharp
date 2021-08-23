// Tests: Structures of directed graphs
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Graphs
{
    [TestFixture]
    public class DirectedGraphTests
    {
        private DirectedSimpleGraph<int, string, string> testObject;

        [SetUp]
        public void SetUp() =>
            testObject = new DirectedSimpleGraph<int, string, string>(Enumerable.Range(0, 10));

        [Test]
        public void IndexerGetSet_WhenSettingProperty_ThenProperty()
        {
            // given
            string vertexProperty = "x";
            string edgeProperty = "y";
            int vertex = 2;
            Edge<int> edge = testObject.AddEdgeBetween(0, 1);
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
        public void IndexerGet_WhenNoProperty_ThenDefault()
        {
            // given
            Edge<int> edge = testObject.AddEdgeBetween(6, 7);
            // when
            string resultVertex = testObject[4];
            string resultEdge = testObject[edge];
            // then
            resultVertex.Should().Be(default);
            resultEdge.Should().Be(default);
        }

        [Test]
        public void IndexerGet_WhenNotExistingEdge_ThenIllegalArgumentException()
        {
            // when
            Action action = () => _ = testObject[new Edge<int>(2, 8)];
            // then
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void VerticesCount_ThenNumberOfVertices()
        {
            // when
            int result = testObject.VerticesCount;
            // then
            result.Should().Be(10);
        }

        [Test]
        public void GetVertices_ThenAllVertices()
        {
            // when
            IEnumerable<int> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(Enumerable.Range(0, 10));
        }

        [Test]
        public void AddVertex_WhenNewVertex_ThenTrue()
        {
            // given
            int newVertex = 13;
            string property = "qwerty";
            // when
            bool result = testObject.AddVertex(newVertex, property);
            // then
            result.Should().BeTrue();
            testObject.VerticesCount.Should().Be(11);
            testObject.GetNeighbours(newVertex).Should().BeEmpty();
            testObject[newVertex].Should().Be(property);
        }

        [Test]
        public void AddVertex_WhenExistingVertex_ThenFalse()
        {
            // given
            int vertex = 6;
            string property = "qwerty";

            testObject[vertex] = property;
            // when
            bool result = testObject.AddVertex(vertex, "abcdefg");
            // then
            result.Should().BeFalse();
            testObject.VerticesCount.Should().Be(10);
            testObject[vertex].Should().Be(property);
        }

        [Test]
        public void EdgesCount_ThenNumberOfEdges()
        {
            // given
            testObject.AddEdgeBetween(7, 7);
            testObject.AddEdgeBetween(1, 5);
            testObject.AddEdgeBetween(2, 4);
            testObject.AddEdgeBetween(8, 0);
            testObject.AddEdgeBetween(6, 3);
            testObject.AddEdgeBetween(3, 6);
            testObject.AddEdgeBetween(9, 3);
            testObject.AddEdgeBetween(8, 0);
            // when
            long result = testObject.EdgesCount;
            // then
            result.Should().Be(7);
        }

        [Test]
        public void Edges_ThenAllEdges()
        {
            // given
            testObject.AddEdgeBetween(7, 7);
            testObject.AddEdgeBetween(1, 5);
            testObject.AddEdgeBetween(2, 4);
            testObject.AddEdgeBetween(8, 0);
            testObject.AddEdgeBetween(6, 3);
            testObject.AddEdgeBetween(3, 6);
            testObject.AddEdgeBetween(9, 3);
            testObject.AddEdgeBetween(8, 0);
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 5), new Edge<int>(2, 4), new Edge<int>(3, 6),
                                        new Edge<int>(6, 3), new Edge<int>(7, 7), new Edge<int>(8, 0),
                                        new Edge<int>(9, 3) });
        }

        [Test]
        public void GetEdge_WhenInDirection_ThenEdge()
        {
            // given
            int source = 9;
            int destination = 5;

            testObject.AddEdgeBetween(source, destination);
            // when
            Edge<int> result = testObject.GetEdge(source, destination);
            // then
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
        }

        [Test]
        public void GetEdge_WhenReversedDirection_ThenNull()
        {
            // given
            int source = 9;
            int destination = 5;

            testObject.AddEdgeBetween(source, destination);
            // when
            Edge<int> result = testObject.GetEdge(destination, source);
            // then
            result.Should().BeNull();
        }

        [Test]
        public void GetEdge_WhenNotExists_ThenNull()
        {
            // when
            Edge<int> result = testObject.GetEdge(1, 2);
            // then
            result.Should().BeNull();
        }

        [Test]
        public void AddEdgeBetween_WhenNewEdge_ThenCreatedEdge()
        {
            // given
            string property = "asdfgh";
            // when
            Edge<int> result = testObject.AddEdgeBetween(1, 5, property);
            testObject.AddEdgeBetween(1, 1);
            // then
            result.Source.Should().Be(1);
            result.Destination.Should().Be(5);
            testObject[result].Should().Be(property);
            testObject.GetNeighbours(1).Should().BeEquivalentTo(new List<int>() { 1, 5 });
            testObject.GetNeighbours(5).Should().BeEmpty();
        }

        [Test]
        public void AddEdgeBetween_WhenDuplicatedEdge_ThenExistingEdge()
        {
            // given
            int source = 3;
            int destination = 7;
            Edge<int> expected = testObject.AddEdgeBetween(source, destination);
            // when
            Edge<int> result = testObject.AddEdgeBetween(source, destination);
            // then
            result.Should().BeSameAs(expected);
        }

        [Test]
        public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
        {
            // given
            testObject.AddEdgeBetween(1, 1);
            testObject.AddEdgeBetween(1, 3);
            testObject.AddEdgeBetween(1, 4);
            testObject.AddEdgeBetween(1, 7);
            testObject.AddEdgeBetween(1, 9);
            testObject.AddEdgeBetween(2, 1);
            testObject.AddEdgeBetween(6, 1);
            // when
            IEnumerable<int> result = testObject.GetNeighbours(1);
            // then
            result.Should().BeEquivalentTo(new List<int>() { 1, 3, 4, 7, 9 });
        }

        [Test]
        public void GetAdjacentEdges_ThenOutgoingEdges()
        {
            // given
            testObject.AddEdgeBetween(1, 1);
            testObject.AddEdgeBetween(1, 3);
            testObject.AddEdgeBetween(1, 4);
            testObject.AddEdgeBetween(1, 7);
            testObject.AddEdgeBetween(1, 9);
            testObject.AddEdgeBetween(2, 1);
            testObject.AddEdgeBetween(6, 1);
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(1);
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 1), new Edge<int>(1, 3), new Edge<int>(1, 4),
                                        new Edge<int>(1, 7), new Edge<int>(1, 9) });
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // given
            testObject.AddEdgeBetween(1, 1);
            testObject.AddEdgeBetween(1, 3);
            testObject.AddEdgeBetween(1, 4);
            testObject.AddEdgeBetween(1, 7);
            testObject.AddEdgeBetween(1, 9);
            testObject.AddEdgeBetween(2, 1);
            testObject.AddEdgeBetween(6, 1);
            // when
            long result = testObject.GetOutputDegree(1);
            // then
            result.Should().Be(5);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // given
            testObject.AddEdgeBetween(1, 1);
            testObject.AddEdgeBetween(3, 1);
            testObject.AddEdgeBetween(4, 1);
            testObject.AddEdgeBetween(7, 1);
            testObject.AddEdgeBetween(9, 1);
            testObject.AddEdgeBetween(1, 2);
            testObject.AddEdgeBetween(1, 6);
            // when
            long result = testObject.GetInputDegree(1);
            // then
            result.Should().Be(5);
        }

        [Test]
        public void Reverse_ThenAllEdgesHaveReversedDirection()
        {
            // given
            int vertex = 5;
            string vertexProperty = "123456";
            string edgeProperty = "zxcvb";
            Edge<int> edge = testObject.AddEdgeBetween(1, 2);
            testObject.AddEdgeBetween(3, 5);
            testObject.AddEdgeBetween(4, 9);
            testObject.AddEdgeBetween(5, 4);
            testObject.AddEdgeBetween(5, 7);
            testObject.AddEdgeBetween(6, 2);
            testObject.AddEdgeBetween(6, 6);
            testObject.AddEdgeBetween(7, 8);
            testObject.AddEdgeBetween(9, 1);
            testObject.AddEdgeBetween(9, 6);
            testObject[vertex] = vertexProperty;
            testObject[edge] = edgeProperty;
            // when
            testObject.Reverse();
            // then
            testObject.Edges.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 9), new Edge<int>(2, 1), new Edge<int>(2, 6),
                                        new Edge<int>(4, 5), new Edge<int>(5, 3), new Edge<int>(6, 6),
                                        new Edge<int>(6, 9), new Edge<int>(7, 5), new Edge<int>(8, 7),
                                        new Edge<int>(9, 4) });
            testObject[vertex].Should().Be(vertexProperty);
            testObject[9].Should().BeNull();
            testObject[testObject.GetEdge(2, 1)].Should().Be(edgeProperty);
            testObject[testObject.GetEdge(5, 3)].Should().BeNull();
        }

        [Test]
        public void ReversedCopy_ThenNewGraphWithReversedEdges()
        {
            // given
            int vertex = 5;
            string vertexProperty = "123456";
            string edgeProperty = "zxcvb";
            Edge<int> edge = testObject.AddEdgeBetween(1, 2);
            testObject.AddEdgeBetween(3, 5);
            testObject.AddEdgeBetween(4, 9);
            testObject.AddEdgeBetween(5, 4);
            testObject.AddEdgeBetween(5, 7);
            testObject.AddEdgeBetween(6, 2);
            testObject.AddEdgeBetween(6, 6);
            testObject.AddEdgeBetween(7, 8);
            testObject.AddEdgeBetween(9, 1);
            testObject.AddEdgeBetween(9, 6);
            testObject[vertex] = vertexProperty;
            testObject[edge] = edgeProperty;
            // when
            IDirectedGraph<int, string, string> result = testObject.ReversedCopy();
            // then
            result.Vertices.Should().BeEquivalentTo(testObject.Vertices);
            result.Edges.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(1, 9), new Edge<int>(2, 1), new Edge<int>(2, 6),
                                        new Edge<int>(4, 5), new Edge<int>(5, 3), new Edge<int>(6, 6),
                                        new Edge<int>(6, 9), new Edge<int>(7, 5), new Edge<int>(8, 7),
                                        new Edge<int>(9, 4) });
            result[vertex].Should().Be(vertexProperty);
            result[9].Should().BeNull();
            result[result.GetEdge(2, 1)].Should().Be(edgeProperty);
            result[result.GetEdge(5, 3)].Should().BeNull();
        }
    }
}
