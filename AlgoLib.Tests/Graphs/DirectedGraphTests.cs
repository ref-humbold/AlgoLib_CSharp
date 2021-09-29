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
            Vertex<int> vertex = testObject[2];
            Edge<int> edge = testObject.AddEdgeBetween(testObject[0], testObject[1]);
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
            Edge<int> edge = testObject.AddEdgeBetween(testObject[6], testObject[7]);
            // when
            string resultVertex = testObject[testObject[4]];
            string resultEdge = testObject[edge];
            // then
            resultVertex.Should().Be(default);
            resultEdge.Should().Be(default);
        }

        [Test]
        public void IndexerGet_WhenNotExistingEdge_ThenIllegalArgumentException()
        {
            // when
            Action action = () => _ = testObject[new Edge<int>(testObject[2], testObject[8])];
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
            IEnumerable<Vertex<int>> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(Enumerable.Range(0, 10));
        }

        [Test]
        public void AddVertex_WhenNewVertex_ThenTrue()
        {
            // given
            int newVertexId = 13;
            string property = "qwerty";
            // when
            bool result = testObject.AddVertex(newVertexId, property);
            // then
            result.Should().BeTrue();
            testObject.VerticesCount.Should().Be(11);
            testObject.GetNeighbours(testObject[newVertexId]).Should().BeEmpty();
            testObject[testObject[newVertexId]].Should().Be(property);
        }

        [Test]
        public void AddVertex_WhenExistingVertex_ThenFalse()
        {
            // given
            Vertex<int> vertex = testObject[6];
            string property = "qwerty";

            testObject[vertex] = property;
            // when
            bool result = testObject.AddVertex(vertex.Id, "abcdefg");
            // then
            result.Should().BeFalse();
            testObject.VerticesCount.Should().Be(10);
            testObject[vertex].Should().Be(property);
        }

        [Test]
        public void EdgesCount_ThenNumberOfEdges()
        {
            // given
            testObject.AddEdgeBetween(testObject[7], testObject[7]);
            testObject.AddEdgeBetween(testObject[1], testObject[5]);
            testObject.AddEdgeBetween(testObject[2], testObject[4]);
            testObject.AddEdgeBetween(testObject[8], testObject[0]);
            testObject.AddEdgeBetween(testObject[6], testObject[3]);
            testObject.AddEdgeBetween(testObject[3], testObject[6]);
            testObject.AddEdgeBetween(testObject[9], testObject[3]);
            testObject.AddEdgeBetween(testObject[8], testObject[0]);
            // when
            long result = testObject.EdgesCount;
            // then
            result.Should().Be(7);
        }

        [Test]
        public void Edges_ThenAllEdges()
        {
            // given
            testObject.AddEdgeBetween(testObject[7], testObject[7]);
            testObject.AddEdgeBetween(testObject[1], testObject[5]);
            testObject.AddEdgeBetween(testObject[2], testObject[4]);
            testObject.AddEdgeBetween(testObject[8], testObject[0]);
            testObject.AddEdgeBetween(testObject[6], testObject[3]);
            testObject.AddEdgeBetween(testObject[3], testObject[6]);
            testObject.AddEdgeBetween(testObject[9], testObject[3]);
            testObject.AddEdgeBetween(testObject[8], testObject[0]);
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { new Edge<int>(new Vertex<int>(1), new Vertex<int>(5)),
                                        new Edge<int>(new Vertex<int>(2), new Vertex<int>(4)),
                                        new Edge<int>(new Vertex<int>(3), new Vertex<int>(6)),
                                        new Edge<int>(new Vertex<int>(6), new Vertex<int>(3)),
                                        new Edge<int>(new Vertex<int>(7), new Vertex<int>(7)),
                                        new Edge<int>(new Vertex<int>(8), new Vertex<int>(0)),
                                        new Edge<int>(new Vertex<int>(9), new Vertex<int>(3)) });
        }

        [Test]
        public void GetEdge_WhenInDirection_ThenEdge()
        {
            // given
            Vertex<int> source = testObject[9];
            Vertex<int> destination = testObject[5];

            testObject.AddEdgeBetween(source, destination);
            // when
            Edge<int> result = testObject[source, destination];
            // then
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
        }

        [Test]
        public void GetEdge_WhenReversedDirection_ThenNull()
        {
            // given
            Vertex<int> source = testObject[9];
            Vertex<int> destination = testObject[5];

            testObject.AddEdgeBetween(source, destination);
            // when
            Edge<int> result = testObject[destination, source];
            // then
            result.Should().BeNull();
        }

        [Test]
        public void GetEdge_WhenNotExists_ThenNull()
        {
            // when
            Edge<int> result = testObject[testObject[1], testObject[2]];
            // then
            result.Should().BeNull();
        }

        [Test]
        public void AddEdgeBetween_WhenNewEdge_ThenCreatedEdge()
        {
            // given
            string property = "asdfgh";
            // when
            Edge<int> result = testObject.AddEdgeBetween(testObject[1], testObject[5], property);
            testObject.AddEdgeBetween(testObject[1], testObject[1]);
            // then
            result.Source.Should().Be(1);
            result.Destination.Should().Be(5);
            testObject[result].Should().Be(property);
            testObject.GetNeighbours(testObject[1]).Should().BeEquivalentTo(
                new[] { new Vertex<int>(1), new Vertex<int>(5) });
            testObject.GetNeighbours(testObject[5]).Should().BeEmpty();
        }

        [Test]
        public void AddEdgeBetween_WhenDuplicatedEdge_ThenExistingEdge()
        {
            // given
            Vertex<int> source = testObject[3];
            Vertex<int> destination = testObject[7];
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
            testObject.AddEdgeBetween(testObject[1], testObject[1]);
            testObject.AddEdgeBetween(testObject[1], testObject[3]);
            testObject.AddEdgeBetween(testObject[1], testObject[4]);
            testObject.AddEdgeBetween(testObject[1], testObject[7]);
            testObject.AddEdgeBetween(testObject[1], testObject[9]);
            testObject.AddEdgeBetween(testObject[2], testObject[1]);
            testObject.AddEdgeBetween(testObject[6], testObject[1]);
            // when
            IEnumerable<Vertex<int>> result = testObject.GetNeighbours(testObject[1]);
            // then
            result.Should().BeEquivalentTo(new[] { new Vertex<int>(1), new Vertex<int>(3),
                                                   new Vertex<int>(4), new Vertex<int>(7),
                                                   new Vertex<int>(9) });
        }

        [Test]
        public void GetAdjacentEdges_ThenOutgoingEdges()
        {
            // given
            testObject.AddEdgeBetween(testObject[1], testObject[1]);
            testObject.AddEdgeBetween(testObject[1], testObject[3]);
            testObject.AddEdgeBetween(testObject[1], testObject[4]);
            testObject.AddEdgeBetween(testObject[1], testObject[7]);
            testObject.AddEdgeBetween(testObject[1], testObject[9]);
            testObject.AddEdgeBetween(testObject[2], testObject[1]);
            testObject.AddEdgeBetween(testObject[6], testObject[1]);
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(testObject[1]);
            // then
            result.Should().BeEquivalentTo(new[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(1)),
                                                   new Edge<int>(new Vertex<int>(1), new Vertex<int>(3)),
                                                   new Edge<int>(new Vertex<int>(1), new Vertex<int>(4)),
                                                   new Edge<int>(new Vertex<int>(1), new Vertex<int>(7)),
                                                   new Edge<int>(new Vertex<int>(1), new Vertex<int>(9)) });
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // given
            testObject.AddEdgeBetween(testObject[1], testObject[1]);
            testObject.AddEdgeBetween(testObject[1], testObject[3]);
            testObject.AddEdgeBetween(testObject[1], testObject[4]);
            testObject.AddEdgeBetween(testObject[1], testObject[7]);
            testObject.AddEdgeBetween(testObject[1], testObject[9]);
            testObject.AddEdgeBetween(testObject[2], testObject[1]);
            testObject.AddEdgeBetween(testObject[6], testObject[1]);
            // when
            long result = testObject.GetOutputDegree(testObject[1]);
            // then
            result.Should().Be(5);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // given
            testObject.AddEdgeBetween(testObject[1], testObject[1]);
            testObject.AddEdgeBetween(testObject[3], testObject[1]);
            testObject.AddEdgeBetween(testObject[4], testObject[1]);
            testObject.AddEdgeBetween(testObject[7], testObject[1]);
            testObject.AddEdgeBetween(testObject[9], testObject[1]);
            testObject.AddEdgeBetween(testObject[1], testObject[2]);
            testObject.AddEdgeBetween(testObject[1], testObject[6]);
            // when
            long result = testObject.GetInputDegree(testObject[1]);
            // then
            result.Should().Be(5);
        }

        [Test]
        public void Reverse_ThenAllEdgesHaveReversedDirection()
        {
            // given
            Vertex<int> vertex = testObject[5];
            string vertexProperty = "123456";
            string edgeProperty = "zxcvb";
            Edge<int> edge = testObject.AddEdgeBetween(testObject[1], testObject[2]);
            testObject.AddEdgeBetween(testObject[3], testObject[5]);
            testObject.AddEdgeBetween(testObject[4], testObject[9]);
            testObject.AddEdgeBetween(testObject[5], testObject[4]);
            testObject.AddEdgeBetween(testObject[5], testObject[7]);
            testObject.AddEdgeBetween(testObject[6], testObject[2]);
            testObject.AddEdgeBetween(testObject[6], testObject[6]);
            testObject.AddEdgeBetween(testObject[7], testObject[8]);
            testObject.AddEdgeBetween(testObject[9], testObject[1]);
            testObject.AddEdgeBetween(testObject[9], testObject[6]);
            testObject[vertex] = vertexProperty;
            testObject[edge] = edgeProperty;
            // when
            testObject.Reverse();
            // then
            testObject.Edges.Should().BeEquivalentTo(new[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(9)),
                                                             new Edge<int>(new Vertex<int>(2), new Vertex<int>(1)),
                                                             new Edge<int>(new Vertex<int>(2), new Vertex<int>(6)),
                                                             new Edge<int>(new Vertex<int>(4), new Vertex<int>(5)),
                                                             new Edge<int>(new Vertex<int>(5), new Vertex<int>(3)),
                                                             new Edge<int>(new Vertex<int>(6), new Vertex<int>(6)),
                                                             new Edge<int>(new Vertex<int>(6), new Vertex<int>(9)),
                                                             new Edge<int>(new Vertex<int>(7), new Vertex<int>(5)),
                                                             new Edge<int>(new Vertex<int>(8), new Vertex<int>(7)),
                                                             new Edge<int>(new Vertex<int>(9), new Vertex<int>(4)) });
            testObject[vertex].Should().Be(vertexProperty);
            testObject[testObject[9]].Should().BeNull();
            testObject[testObject[testObject[2], testObject[1]]].Should().Be(edgeProperty);
            testObject[testObject[testObject[5], testObject[3]]].Should().BeNull();
        }

        [Test]
        public void ReversedCopy_ThenNewGraphWithReversedEdges()
        {
            // given
            Vertex<int> vertex = testObject[5];
            string vertexProperty = "123456";
            string edgeProperty = "zxcvb";
            Edge<int> edge = testObject.AddEdgeBetween(testObject[1], testObject[2]);
            testObject.AddEdgeBetween(testObject[3], testObject[5]);
            testObject.AddEdgeBetween(testObject[4], testObject[9]);
            testObject.AddEdgeBetween(testObject[5], testObject[4]);
            testObject.AddEdgeBetween(testObject[5], testObject[7]);
            testObject.AddEdgeBetween(testObject[6], testObject[2]);
            testObject.AddEdgeBetween(testObject[6], testObject[6]);
            testObject.AddEdgeBetween(testObject[7], testObject[8]);
            testObject.AddEdgeBetween(testObject[9], testObject[1]);
            testObject.AddEdgeBetween(testObject[9], testObject[6]);
            testObject[vertex] = vertexProperty;
            testObject[edge] = edgeProperty;
            // when
            IDirectedGraph<int, string, string> result = testObject.ReversedCopy();
            // then
            result.Vertices.Should().BeEquivalentTo(testObject.Vertices);
            result.Edges.Should().BeEquivalentTo(new[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(9)),
                                                         new Edge<int>(new Vertex<int>(2), new Vertex<int>(1)),
                                                         new Edge<int>(new Vertex<int>(2), new Vertex<int>(6)),
                                                         new Edge<int>(new Vertex<int>(4), new Vertex<int>(5)),
                                                         new Edge<int>(new Vertex<int>(5), new Vertex<int>(3)),
                                                         new Edge<int>(new Vertex<int>(6), new Vertex<int>(6)),
                                                         new Edge<int>(new Vertex<int>(6), new Vertex<int>(9)),
                                                         new Edge<int>(new Vertex<int>(7), new Vertex<int>(5)),
                                                         new Edge<int>(new Vertex<int>(8), new Vertex<int>(7)),
                                                         new Edge<int>(new Vertex<int>(9), new Vertex<int>(4)) });
            result[vertex].Should().Be(vertexProperty);
            result[testObject[9]].Should().BeNull();
            result[result[testObject[2], testObject[1]]].Should().Be(edgeProperty);
            result[result[testObject[5], testObject[3]]].Should().BeNull();
        }
    }
}
