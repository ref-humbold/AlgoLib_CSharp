// Tests: Structure of directed graph.
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs
{
    [TestFixture]
    public class DirectedGraphTests
    {
        private DirectedSimpleGraph<int, string, string> testObject;

        [SetUp]
        public void SetUp() =>
            testObject = new DirectedSimpleGraph<int, string, string>(Enumerable.Range(0, 10));

        [Test]
        public void PropertiesIndexerGetSet_WhenSettingProperty_ThenProperty()
        {
            // given
            string vertexProperty = "x";
            string edgeProperty = "y";
            Vertex<int> vertex = testObject[2];
            Edge<int> edge = testObject.AddEdgeBetween(testObject[0], testObject[1]);
            // when
            testObject.Properties[vertex] = vertexProperty;
            testObject.Properties[edge] = edgeProperty;

            string resultVertex = testObject.Properties[vertex];
            string resultEdge = testObject.Properties[edge];
            // then
            resultVertex.Should().Be(vertexProperty);
            resultEdge.Should().Be(edgeProperty);
        }

        [Test]
        public void PropertiesIndexerGet_WhenNoProperty_ThenDefault()
        {
            // given
            Edge<int> edge = testObject.AddEdgeBetween(testObject[6], testObject[7]);
            // when
            string resultVertex = testObject.Properties[testObject[4]];
            string resultEdge = testObject.Properties[edge];
            // then
            resultVertex.Should().Be(default);
            resultEdge.Should().Be(default);
        }

        [Test]
        public void PropertiesIndexerGet_WhenNotExistingEdge_ThenArgumentException()
        {
            // when
            Action action = () => _ = testObject.Properties[new Edge<int>(testObject[2], testObject[8])];
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
        public void Vertices_ThenAllVertices()
        {
            // when
            IEnumerable<Vertex<int>> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(
                new Vertex<int>[] { new Vertex<int>(0), new Vertex<int>(1), new Vertex<int>(2),
                                    new Vertex<int>(3), new Vertex<int>(4), new Vertex<int>(5),
                                    new Vertex<int>(6), new Vertex<int>(7), new Vertex<int>(8),
                                    new Vertex<int>(9) });
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
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            result.Should().BeEquivalentTo(
                new Edge<int>[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(5)),
                                  new Edge<int>(new Vertex<int>(2), new Vertex<int>(4)),
                                  new Edge<int>(new Vertex<int>(3), new Vertex<int>(6)),
                                  new Edge<int>(new Vertex<int>(6), new Vertex<int>(3)),
                                  new Edge<int>(new Vertex<int>(7), new Vertex<int>(7)),
                                  new Edge<int>(new Vertex<int>(8), new Vertex<int>(0)),
                                  new Edge<int>(new Vertex<int>(9), new Vertex<int>(3)) });
        }

        [Test]
        public void IndexerGetVertex_WhenExisting_ThenVertex()
        {
            // given
            int vertexId = 4;
            // when
            Vertex<int> result = testObject[vertexId];
            // then
            result.Id.Should().Be(vertexId);
        }

        [Test]
        public void IndexerGetVertex_WhenNotExists_ThenKeyNotFoundException()
        {
            // when
            Action action = () => _ = testObject[12];
            // then
            action.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void IndexerGetEdge_WhenInDirection_ThenEdge()
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
        public void IndexerGetEdge_WhenReversedDirection_ThenKeyNotFoundException()
        {
            // given
            Vertex<int> source = testObject[9];
            Vertex<int> destination = testObject[5];

            testObject.AddEdgeBetween(source, destination);
            // when
            Action action = () => _ = testObject[destination, source];
            // then
            action.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void IndexerGetEdge_WhenNotExists_ThenKeyNotFoundException()
        {
            // when
            Action action = () => _ = testObject[1, 2];
            // then
            action.Should().Throw<KeyNotFoundException>();
        }

        [Test]
        public void AddVertex_WhenNewVertexId_ThenCreatedVertex()
        {
            // given
            int newVertexId = 13;
            string property = "qwerty";
            // when
            Vertex<int> result = testObject.AddVertex(newVertexId, property);
            // then
            result.Id.Should().Be(newVertexId);
            testObject.VerticesCount.Should().Be(11);
            testObject.GetNeighbours(result).Should().BeEmpty();
            testObject.Properties[result].Should().Be(property);
        }

        [Test]
        public void AddVertex_WhenExistingVertex_ThenArgumentException()
        {
            // given
            Vertex<int> vertex = testObject[6];
            string property = "qwerty";

            testObject.Properties[vertex] = property;
            // when
            Action action = () => testObject.AddVertex(vertex.Id, "abcdefg");
            // then
            action.Should().Throw<ArgumentException>();
            testObject.VerticesCount.Should().Be(10);
            testObject.Properties[vertex].Should().Be(property);
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
            result.Source.Should().Be(new Vertex<int>(1));
            result.Destination.Should().Be(new Vertex<int>(5));
            testObject.Properties[result].Should().Be(property);
            testObject.GetNeighbours(testObject[1]).Should().BeEquivalentTo(
                new[] { new Vertex<int>(1), new Vertex<int>(5) });
            testObject.GetNeighbours(testObject[5]).Should().BeEmpty();
        }

        [Test]
        public void AddEdgeBetween_WhenDuplicatedEdge_ThenArgumentException()
        {
            // given
            Vertex<int> source = testObject[3];
            Vertex<int> destination = testObject[7];
            Edge<int> expected = testObject.AddEdgeBetween(source, destination);
            // when
            Action action = () => testObject.AddEdgeBetween(source, destination);
            // then
            action.Should().Throw<ArgumentException>();
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
            result.Should().BeEquivalentTo(
                new[] { new Vertex<int>(1), new Vertex<int>(3), new Vertex<int>(4),
                        new Vertex<int>(7), new Vertex<int>(9) });
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
            result.Should().BeEquivalentTo(
                new[] {
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(3)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(4)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(7)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(9))
                });
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
            testObject.Properties[vertex] = vertexProperty;
            testObject.Properties[edge] = edgeProperty;
            // when
            testObject.Reverse();
            // then
            testObject.Edges.Should().BeEquivalentTo(
                new[] {
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(9)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(6)),
                    new Edge<int>(new Vertex<int>(4), new Vertex<int>(5)),
                    new Edge<int>(new Vertex<int>(5), new Vertex<int>(3)),
                    new Edge<int>(new Vertex<int>(6), new Vertex<int>(6)),
                    new Edge<int>(new Vertex<int>(6), new Vertex<int>(9)),
                    new Edge<int>(new Vertex<int>(7), new Vertex<int>(5)),
                    new Edge<int>(new Vertex<int>(8), new Vertex<int>(7)),
                    new Edge<int>(new Vertex<int>(9), new Vertex<int>(4))
                });
            testObject.Properties[vertex].Should().Be(vertexProperty);
            testObject.Properties[testObject[9]].Should().BeNull();
            testObject.Properties[testObject[2, 1]].Should().Be(edgeProperty);
            testObject.Properties[testObject[5, 3]].Should().BeNull();
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
            testObject.Properties[vertex] = vertexProperty;
            testObject.Properties[edge] = edgeProperty;
            // when
            IDirectedGraph<int, string, string> result = testObject.ReversedCopy();
            // then
            result.Vertices.Should().BeEquivalentTo(testObject.Vertices);
            result.Edges.Should().BeEquivalentTo(
                new[] {
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(9)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(6)),
                    new Edge<int>(new Vertex<int>(4), new Vertex<int>(5)),
                    new Edge<int>(new Vertex<int>(5), new Vertex<int>(3)),
                    new Edge<int>(new Vertex<int>(6), new Vertex<int>(6)),
                    new Edge<int>(new Vertex<int>(6), new Vertex<int>(9)),
                    new Edge<int>(new Vertex<int>(7), new Vertex<int>(5)),
                    new Edge<int>(new Vertex<int>(8), new Vertex<int>(7)),
                    new Edge<int>(new Vertex<int>(9), new Vertex<int>(4))
                });
            result.Properties[vertex].Should().Be(vertexProperty);
            result.Properties[result[9]].Should().BeNull();
            result.Properties[result[2, 1]].Should().Be(edgeProperty);
            result.Properties[result[5, 3]].Should().BeNull();
        }
    }
}
