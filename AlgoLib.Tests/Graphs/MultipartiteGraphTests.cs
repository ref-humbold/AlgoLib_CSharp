using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs;

// Tests: Structure of multipartite graph.
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
    public void PropertiesIndexer_WhenSettingProperty_ThenProperty()
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
        Assert.That(resultVertex, Is.EqualTo(vertexProperty));
        Assert.That(resultEdge, Is.EqualTo(edgeProperty));
    }

    [Test]
    public void VerticesCount_ThenNumberOfVertices()
    {
        // when
        int result = testObject.VerticesCount;

        // then
        Assert.That(result, Is.EqualTo(10));
    }

    [Test]
    public void EdgesCount_ThenNumberOfEdges()
    {
        // when
        int result = testObject.EdgesCount;

        // then
        Assert.That(result, Is.EqualTo(5));
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
        Assert.That(result.Id, Is.EqualTo(vertexId));
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
        Assert.That(result.Source, Is.EqualTo(source));
        Assert.That(result.Destination, Is.EqualTo(destination));
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
        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void GetInputDegree_ThenNumberOfIncomingEdges()
    {
        // when
        int result = testObject.GetInputDegree(new Vertex<int>(9));

        // then
        Assert.That(result, Is.EqualTo(2));
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
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
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
        Assert.That(result.Id, Is.EqualTo(newVertexId));
        Assert.That(testObject.VerticesCount, Is.EqualTo(11));
        Assert.That(testObject.GetNeighbours(result), Is.Empty);
        Assert.That(testObject.Properties[result], Is.EqualTo(property));
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
        Assert.That(action, Throws.ArgumentException);
        Assert.That(testObject.VerticesCount, Is.EqualTo(10));
        Assert.That(testObject.Properties[vertex], Is.EqualTo(property));
    }

    [Test]
    public void AddVertex_WhenInvalidGroup_ThenIndexOutOfRangeException()
    {
        // when
        Action action = () => testObject.AddVertex(-3, 19);

        // then
        Assert.That(action, Throws.TypeOf<IndexOutOfRangeException>());
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
        Assert.That(result.Source, Is.EqualTo(source));
        Assert.That(result.Destination, Is.EqualTo(destination));
        Assert.That(testObject.Properties[result], Is.EqualTo(property));
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
        Assert.That(action, Throws.ArgumentException);
    }

    [Test]
    public void AddEdge_WhenSameGroup_ThenGraphPartitionException()
    {
        // when
        Action action = () => testObject.AddEdgeBetween(new Vertex<int>(5), new Vertex<int>(8));

        // then
        Assert.That(action, Throws.TypeOf<GraphPartitionException>());
    }

    [Test]
    public void AddEdge_WhenInvalidVertex_ThenArgumentException()
    {
        // when
        Action action = () => testObject.AddEdgeBetween(new Vertex<int>(15), new Vertex<int>(18));

        // then
        Assert.That(action, Throws.ArgumentException);
    }
}
