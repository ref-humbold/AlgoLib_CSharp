// Tests: Structure of multipartite graph.
using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs
{
    [TestFixture]
    public class MultipartiteGraphTest
    {
        private MultipartiteGraph<int, string, string> testObject;

        [SetUp]
        public void SetUp()
        {
            testObject = new MultipartiteGraph<int, string, string>(
                5, new[] { new[] { 0, 1, 2 }, new[] { 3, 4 }, new[] { 5, 6, 7, 8 }, new[] { 9 } });
            testObject.AddEdgeBetween(new Vertex<int>(0), new Vertex<int>(3));
            testObject.AddEdgeBetween(new Vertex<int>(1), new Vertex<int>(5));
            testObject.AddEdgeBetween(new Vertex<int>(2), new Vertex<int>(9));
            testObject.AddEdgeBetween(new Vertex<int>(4), new Vertex<int>(6));
            testObject.AddEdgeBetween(new Vertex<int>(7), new Vertex<int>(9));
        }

        [Test]
        public void PropertiesIndexerGetSet_WhenSettingProperty_ThenProperty()
        {
            // given
            var vertex = new Vertex<int>(2);
            Edge<int> edge = testObject[0, 3];
            string vertexProperty = "x";
            string edgeProperty = "y";
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
        public void VerticesCount_ThenNumberOfVertices()
        {
            // when
            int result = testObject.VerticesCount;
            // then
            result.Should().Be(10);
        }

        [Test]
        public void EdgesCount_ThenNumberOfEdges()
        {
            // when
            int result = testObject.EdgesCount;
            // then
            result.Should().Be(5);
        }

        [Test]
        public void Vertices_ThenAllVertices()
        {
            // when
            IEnumerable<Vertex<int>> result = testObject.Vertices;
            // then
            result.Should().BeEquivalentTo(
                new[] {
                    new Vertex<int>(0), new Vertex<int>(1), new Vertex<int>(2),
                    new Vertex<int>(3), new Vertex<int>(4), new Vertex<int>(5),
                    new Vertex<int>(6), new Vertex<int>(7), new Vertex<int>(8),
                    new Vertex<int>(9)
                });
        }

        [Test]
        public void Edges_ThenAllEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.Edges;
            // then
            result.Should().BeEquivalentTo(
                new[] {
                    new Edge<int>(new Vertex<int>(0), new Vertex<int>(3)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(5)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(9)),
                    new Edge<int>(new Vertex<int>(4), new Vertex<int>(6)),
                    new Edge<int>(new Vertex<int>(7), new Vertex<int>(9))
                });
        }

        [Test]
        public void Indexer_WhenVertexExists_ThenVertex()
        {
            // given
            int vertexId = 5;
            // when
            Vertex<int> result = testObject[vertexId];
            // then
            result.Id.Should().Be(vertexId);
        }

        [Test]
        public void Indexer_WhenEdgeExists_ThenEdge()
        {
            // given
            var source = new Vertex<int>(2);
            var destination = new Vertex<int>(9);
            // when
            Edge<int> result = testObject[source, destination];
            // then
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
        }

        [Test]
        public void GetAdjacentEdges_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(new Vertex<int>(9));
            // then
            result.Should().BeEquivalentTo(
                new[] { new Edge<int>(new Vertex<int>(2), new Vertex<int>(9)),
                        new Edge<int>(new Vertex<int>(7), new Vertex<int>(9)) });
        }

        [Test]
        public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
        {
            // when
            IEnumerable<Vertex<int>> result = testObject.GetNeighbours(new Vertex<int>(9));
            // then
            result.Should().BeEquivalentTo(new[] { new Vertex<int>(2), new Vertex<int>(7) });
        }

        [Test]
        public void GetOutputDegree_ThenNumberOfOutgoingEdges()
        {
            // when
            int result = testObject.GetOutputDegree(new Vertex<int>(9));
            // then
            result.Should().Be(2);
        }

        [Test]
        public void GetInputDegree_ThenNumberOfIncomingEdges()
        {
            // when
            int result = testObject.GetInputDegree(new Vertex<int>(9));
            // then
            result.Should().Be(2);
        }

        [Test]
        public void GetVerticesFromGroup_WhenValidGroup_ThenVertices()
        {
            // when
            IEnumerable<Vertex<int>> result = testObject.GetVerticesFromGroup(2);
            // then
            result.Should().BeEquivalentTo(new[] { new Vertex<int>(5), new Vertex<int>(6),
                                                    new Vertex<int>(7), new Vertex<int>(8) });
        }

        [Test]
        public void GetVerticesFromGroup_WhenInvalidGroup_ThenIndexOutOfRangeException()
        {
            // when
            Action action = () => testObject.GetVerticesFromGroup(14);

            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void AddVertex_WhenNewVertex_ThenCreatedVertex()
        {
            // given
            int newVertexId = 13;
            string property = "qwerty";
            // when
            Vertex<int> result = testObject.AddVertex(4, newVertexId, property);
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
            var vertex = new Vertex<int>(6);
            string property = "qwerty";

            testObject.Properties[vertex] = property;
            // when
            Action action = () => testObject.AddVertex(3, vertex, "xyz");
            // then
            action.Should().Throw<ArgumentException>();
            testObject.VerticesCount.Should().Be(10);
            testObject.Properties[vertex].Should().Be(property);
        }

        [Test]
        public void AddVertex_WhenInvalidGroup_ThenIndexOutOfRangeException()
        {
            // when
            Action action = () => testObject.AddVertex(-3, 19);

            // then
            action.Should().Throw<IndexOutOfRangeException>();
        }

        [Test]
        public void AddEdge_WhenNewEdge_ThenCreatedEdge()
        {
            // given
            var source = new Vertex<int>(2);
            var destination = new Vertex<int>(8);
            string property = "asdfgh";
            // when
            Edge<int> result = testObject.AddEdgeBetween(source, destination, property);
            // then
            result.Source.Should().Be(source);
            result.Destination.Should().Be(destination);
            testObject.Properties[result].Should().Be(property);
            testObject.GetNeighbours(destination).Should().BeEquivalentTo(new[] { source });
        }

        [Test]
        public void AddEdge_WhenExistingEdge_ThenArgumentException()
        {
            // given
            var source = new Vertex<int>(8);
            var destination = new Vertex<int>(3);

            testObject.AddEdgeBetween(source, destination);
            // when
            Action action = () => testObject.AddEdgeBetween(source, destination);
            // then
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void AddEdge_WhenSameGroup_ThenGraphPartitionException()
        {
            // when
            Action action = () => testObject.AddEdgeBetween(new Vertex<int>(5), new Vertex<int>(8));

            // then
            action.Should().Throw<GraphPartitionException>();
        }

        [Test]
        public void AddEdge_WhenInvalidVertex_ThenArgumentException()
        {
            // when
            Action action = () => testObject.AddEdgeBetween(new Vertex<int>(15), new Vertex<int>(18));

            // then
            action.Should().Throw<ArgumentException>();
        }
    }
}
