using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AlgoLib.Graphs;

// Tests: Structure of tree graph.
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
    public void PropertiesIndexer_WhenSettingProperty_ThenProperty()
    {
        // given
        string vertexProperty = "x";
        string edgeProperty = "y";
        Vertex<int> vertex = testObject[2];
        Edge<int> edge = testObject[6, 2];

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
        long result = testObject.VerticesCount;

        // then
        Assert.That(result, Is.EqualTo(8));
    }

    [Test]
    public void Vertices_ThenAllVertices()
    {
        // when
        IEnumerable<Vertex<int>> result = testObject.Vertices;

        // then
        Assert.That(
            result, Is.EquivalentTo(
                new[]
                {
                    new Vertex<int>(0), new Vertex<int>(1), new Vertex<int>(2), new Vertex<int>(3),
                    new Vertex<int>(4), new Vertex<int>(5), new Vertex<int>(6), new Vertex<int>(7)
                }));
    }

    [Test]
    public void EdgesCount_ThenNumberOfEdges()
    {
        // when
        long result = testObject.EdgesCount;

        // then
        Assert.That(result, Is.EqualTo(7));
    }

    [Test]
    public void Edges_ThenAllEdges()
    {
        // when
        IEnumerable<Edge<int>> result = testObject.Edges;

        // then
        Assert.That(
            result, Is.EquivalentTo(
                new[]
                {
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(0)),
                    new Edge<int>(new Vertex<int>(2), new Vertex<int>(0)),
                    new Edge<int>(new Vertex<int>(3), new Vertex<int>(0)),
                    new Edge<int>(new Vertex<int>(4), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(5), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(6), new Vertex<int>(2)),
                    new Edge<int>(new Vertex<int>(7), new Vertex<int>(2))
                }));
    }

    [Test]
    public void Indexer_WhenVertexExists_ThenVertex()
    {
        // given
        int vertexId = 4;

        // when
        Vertex<int> result = testObject[vertexId];

        // then
        Assert.That(result.Id, Is.EqualTo(vertexId));
    }

    [Test]
    public void Indexer_WhenEdgeExists_ThenEdge()
    {
        // given
        Vertex<int> source = testObject[5];
        Vertex<int> destination = testObject[1];

        // when
        Edge<int> result = testObject[source, destination];

        // then
        Assert.That(result.Source, Is.EqualTo(source));
        Assert.That(result.Destination, Is.EqualTo(destination));
    }

    [Test]
    public void GetNeighbours_ThenDestinationVerticesOfOutgoingEdges()
    {
        // when
        IEnumerable<Vertex<int>> result = testObject.GetNeighbours(testObject[1]);

        // then
        Assert.That(
            result,
            Is.EquivalentTo(new[] { new Vertex<int>(0), new Vertex<int>(4), new Vertex<int>(5) }));
    }

    [Test]
    public void GetAdjacentEdges_ThenDestinationVerticesOfOutgoingEdges()
    {
        // when
        IEnumerable<Edge<int>> result = testObject.GetAdjacentEdges(testObject[1]);

        // then
        Assert.That(
            result, Is.EquivalentTo(
                new[]
                {
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(0)),
                    new Edge<int>(new Vertex<int>(4), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(5), new Vertex<int>(1))
                }));
    }

    [Test]
    public void GetOutputDegree_ThenNumberOfOutgoingEdges()
    {
        // when
        long result = testObject.GetOutputDegree(testObject[1]);

        // then
        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    public void GetInputDegree_ThenNumberOfIncomingEdges()
    {
        // when
        long result = testObject.GetInputDegree(testObject[1]);

        // then
        Assert.That(result, Is.EqualTo(3));
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
        Edge<int> result = testObject.AddVertex(
            newVertexId, neighbour, vertexProperty, edgeProperty);

        // then
        Assert.That(result.Source.Id, Is.EqualTo(newVertexId));
        Assert.That(result.Destination, Is.EqualTo(neighbour));
        Assert.That(testObject.VerticesCount, Is.EqualTo(9));
        Assert.That(
            testObject.GetNeighbours(testObject[newVertexId]), Is.EqualTo(new[] { neighbour }));
        Assert.That(testObject.Properties[testObject[newVertexId]], Is.EqualTo(vertexProperty));
        Assert.That(testObject.Properties[result], Is.EqualTo(edgeProperty));
    }

    [Test]
    public void AddVertex_WhenExistingVertex_ThenArgumentException()
    {
        // given
        Vertex<int> vertex = testObject[6];
        string property = "qwerty";

        testObject.Properties[vertex] = property;

        // when
        Action action = () => testObject.AddVertex(vertex, testObject[2], "abcdefg", "xyz");

        // then
        Assert.That(action, Throws.ArgumentException);
        Assert.That(testObject.VerticesCount, Is.EqualTo(8));
        Assert.That(testObject.Properties[vertex], Is.EqualTo(property));
    }
}
