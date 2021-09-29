// Tests: Structure of tree testObject
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
            testObject.AddVertex(1, testObject[0]);
            testObject.AddVertex(2, testObject[0]);
            testObject.AddVertex(3, testObject[0]);
            testObject.AddVertex(4, testObject[1]);
            testObject.AddVertex(5, testObject[1]);
            testObject.AddVertex(6, testObject[2]);
            testObject.AddVertex(7, testObject[2]);
        }

        [Test]
        public void IndexerGetSet_WhenSettingProperty_ThenProperty()
        {
            // given
            string vertexProperty = "x";
            string edgeProperty = "y";
            Vertex<int> vertex = testObject[2];
            Edge<int> edge = testObject[testObject[6], testObject[2]];
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
            IEnumerable<Vertex<int>> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(Enumerable.Range(0, 8));
        }

        [Test]
        public void AddVertex_WhenNewVertex_ThenCreatedEdge()
        {
            // given
            int newVertexId = 13;
            Vertex<int> neighbour = testObject[5];
            string vertexProperty = "qwerty";
            string edgeProperty = "asdfgh";
            // when
            Edge<int> result = testObject.AddVertex(newVertexId, neighbour, vertexProperty, edgeProperty);
            // then
            result.Source.Should().Be(newVertexId);
            result.Destination.Should().Be(neighbour);
            testObject.VerticesCount.Should().Be(9);
            testObject.GetNeighbours(testObject[newVertexId]).Should().Equal(neighbour);
            testObject[testObject[newVertexId]].Should().Be(vertexProperty);
            testObject[result].Should().Be(edgeProperty);
        }

        [Test]
        public void AddVertex_WhenExistingVertex_ThenNull()
        {
            // given
            Vertex<int> vertex = testObject[6];
            string property = "qwerty";

            testObject[vertex] = property;
            // when
            Edge<int> result = testObject.AddVertex(vertex.Id, testObject[2], "abcdefg", "xyz");
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
            result.Should().BeEquivalentTo(new[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(0)),
                                                   new Edge<int>(new Vertex<int>(2), new Vertex<int>(0)),
                                                   new Edge<int>(new Vertex<int>(3), new Vertex<int>(0)),
                                                   new Edge<int>(new Vertex<int>(4), new Vertex<int>(1)),
                                                   new Edge<int>(new Vertex<int>(5), new Vertex<int>(1)),
                                                   new Edge<int>(new Vertex<int>(6), new Vertex<int>(2)),
                                                   new Edge<int>(new Vertex<int>(7), new Vertex<int>(2)) });
        }

        [Test]
        public void GetEdge_WhenExists_ThenEdge()
        {
            // given
            Vertex<int> source = testObject[5];
            Vertex<int> destination = testObject[1];
            // when
            Edge<int> result = testObject[source, destination];
            // then
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
        }

        [Test]
        public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Vertex<int>> result = testObject.GetNeighbours(testObject[1]);
            // then
            result.Should().BeEquivalentTo(new[] { new Vertex<int>(0), new Vertex<int>(4),
                                                   new Vertex<int>(5) });
        }

        [Test]
        public void GetAdjacentEdges_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(testObject[1]);
            // then
            result.Should().BeEquivalentTo(new[] { new Edge<int>(new Vertex<int>(1), new Vertex<int>(0)),
                                                   new Edge<int>(new Vertex<int>(4), new Vertex<int>(1)),
                                                   new Edge<int>(new Vertex<int>(5), new Vertex<int>(1)) });
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // when
            long result = testObject.GetOutputDegree(testObject[1]);
            // then
            result.Should().Be(3);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // when
            long result = testObject.GetInputDegree(testObject[1]);
            // then
            result.Should().Be(3);
        }
    }
}
