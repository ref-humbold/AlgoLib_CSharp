using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Graphs;

// Tests: Structure of directed graph.
[TestFixture]
public class DirectedGraphTests
{
    private DirectedSimpleGraph<int, string, string> testObject;

    [SetUp]
    public void SetUp() =>
        testObject = new DirectedSimpleGraph<int, string, string>(Enumerable.Range(0, 10));

    [Test]
    public void PropertiesIndexer_WhenSettingProperty_ThenProperty()
    {
        // given
        var vertexProperty = "x";
        var edgeProperty = "y";
        Vertex<int> vertex = testObject[2];
        Edge<int> edge = testObject.AddEdgeBetween(testObject[0], testObject[1]);

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
    public void PropertiesIndexer_WhenNoProperty_ThenDefault()
    {
        // given
        Edge<int> edge = testObject.AddEdgeBetween(testObject[6], testObject[7]);

        // when
        string resultVertex = testObject.Properties[testObject[4]];
        string resultEdge = testObject.Properties[edge];

        // then
        Assert.That(resultVertex, Is.Default);
        Assert.That(resultEdge, Is.Default);
    }

    [Test]
    public void PropertiesIndexer_WhenNotExistingEdge_ThenArgumentException()
    {
        // when
        Action action = () =>
            _ = testObject.Properties[new Edge<int>(testObject[2], testObject[8])];

        // then
        Assert.That(action, Throws.ArgumentException);
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
    public void Vertices_ThenAllVertices()
    {
        // when
        IEnumerable<Vertex<int>> result = testObject.Vertices;

        // then
        Assert.That(
            result, Is.EquivalentTo(
                new Vertex<int>[]
                {
                    new(0), new(1), new(2),
                    new(3), new(4), new(5),
                    new(6), new(7), new(8),
                    new(9)
                }));
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
        Assert.That(result, Is.EqualTo(7));
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
        Assert.That(
            result, Is.EquivalentTo(
                new Edge<int>[]
                {
                    new(new Vertex<int>(1), new Vertex<int>(5)),
                    new(new Vertex<int>(2), new Vertex<int>(4)),
                    new(new Vertex<int>(3), new Vertex<int>(6)),
                    new(new Vertex<int>(6), new Vertex<int>(3)),
                    new(new Vertex<int>(7), new Vertex<int>(7)),
                    new(new Vertex<int>(8), new Vertex<int>(0)),
                    new(new Vertex<int>(9), new Vertex<int>(3))
                }));
    }

    [Test]
    public void Indexer_WhenExistingVertex_ThenVertex()
    {
        // given
        var vertexId = 4;

        // when
        Vertex<int> result = testObject[vertexId];

        // then
        Assert.That(result.Id, Is.EqualTo(vertexId));
    }

    [Test]
    public void Indexer_WhenNotExistingVertex_ThenKeyNotFoundException()
    {
        // when
        Action action = () => _ = testObject[12];

        // then
        Assert.That(action, Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void Indexer_WhenEdgeInDirection_ThenEdge()
    {
        // given
        Vertex<int> source = testObject[9];
        Vertex<int> destination = testObject[5];

        testObject.AddEdgeBetween(source, destination);

        // when
        Edge<int> result = testObject[source, destination];

        // then
        Assert.That(result.Source, Is.EqualTo(source));
        Assert.That(result.Destination, Is.EqualTo(destination));
    }

    [Test]
    public void Indexer_WhenEdgeInReversedDirection_ThenKeyNotFoundException()
    {
        // given
        Vertex<int> source = testObject[9];
        Vertex<int> destination = testObject[5];

        testObject.AddEdgeBetween(source, destination);

        // when
        Action action = () => _ = testObject[destination, source];

        // then
        Assert.That(action, Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void Indexer_WhenNotExistingEdge_ThenKeyNotFoundException()
    {
        // when
        Action action = () => _ = testObject[1, 2];

        // then
        Assert.That(action, Throws.TypeOf<KeyNotFoundException>());
    }

    [Test]
    public void AddVertex_WhenNewVertexId_ThenCreatedVertex()
    {
        // given
        var newVertexId = 13;
        var property = "qwerty";

        // when
        Vertex<int> result = testObject.AddVertex(newVertexId, property);

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
        Vertex<int> vertex = testObject[6];
        var property = "qwerty";

        testObject.Properties[vertex] = property;

        // when
        Action action = () => testObject.AddVertex(vertex.Id, "abcdefg");

        // then
        Assert.That(action, Throws.ArgumentException);
        Assert.That(testObject.VerticesCount, Is.EqualTo(10));
        Assert.That(testObject.Properties[vertex], Is.EqualTo(property));
    }

    [Test]
    public void AddEdgeBetween_WhenNewEdge_ThenCreatedEdge()
    {
        // given
        var property = "asdfgh";

        // when
        Edge<int> result = testObject.AddEdgeBetween(testObject[1], testObject[5], property);
        testObject.AddEdgeBetween(testObject[1], testObject[1]);

        // then
        Assert.That(result.Source, Is.EqualTo(new Vertex<int>(1)));
        Assert.That(result.Destination, Is.EqualTo(new Vertex<int>(5)));
        Assert.That(testObject.Properties[result], Is.EqualTo(property));
        Assert.That(
            testObject.GetNeighbours(testObject[1]),
            Is.EquivalentTo([new Vertex<int>(1), new Vertex<int>(5)]));
        Assert.That(testObject.GetNeighbours(testObject[5]), Is.Empty);
    }

    [Test]
    public void AddEdgeBetween_WhenDuplicatedEdge_ThenArgumentException()
    {
        // given
        Vertex<int> source = testObject[3];
        Vertex<int> destination = testObject[7];

        testObject.AddEdgeBetween(source, destination);

        // when
        Action action = () => testObject.AddEdgeBetween(source, destination);

        // then
        Assert.That(action, Throws.ArgumentException);
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
        Assert.That(
            result, Is.EquivalentTo(
                [
                    new Vertex<int>(1), new Vertex<int>(3), new Vertex<int>(4),
                    new Vertex<int>(7), new Vertex<int>(9)
                ]));
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
        Assert.That(
            result, Is.EquivalentTo(
                [
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(1)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(3)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(4)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(7)),
                    new Edge<int>(new Vertex<int>(1), new Vertex<int>(9))
                ]));
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
        Assert.That(result, Is.EqualTo(5));
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
        Assert.That(result, Is.EqualTo(5));
    }

    [Test]
    public void Reverse_ThenAllEdgesHaveReversedDirection()
    {
        // given
        Vertex<int> vertex = testObject[5];
        var vertexProperty = "123456";
        var edgeProperty = "zxcvb";
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
        Assert.That(
            testObject.Edges, Is.EquivalentTo(
                [
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
                ]));
        Assert.That(testObject.Properties[vertex], Is.EqualTo(vertexProperty));
        Assert.That(testObject.Properties[testObject[9]], Is.Null);
        Assert.That(testObject.Properties[testObject[2, 1]], Is.EqualTo(edgeProperty));
        Assert.That(testObject.Properties[testObject[5, 3]], Is.Null);
    }

    [Test]
    public void ReversedCopy_ThenNewGraphWithReversedEdges()
    {
        // given
        Vertex<int> vertex = testObject[5];
        var vertexProperty = "123456";
        var edgeProperty = "zxcvb";
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
        Assert.That(result.Vertices, Is.EquivalentTo(testObject.Vertices));
        Assert.That(
            result.Edges, Is.EquivalentTo(
                [
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
                ]));
        Assert.That(result.Properties[vertex], Is.EqualTo(vertexProperty));
        Assert.That(result.Properties[result[9]], Is.Null);
        Assert.That(result.Properties[result[2, 1]], Is.EqualTo(edgeProperty));
        Assert.That(result.Properties[result[5, 3]], Is.Null);
    }
}
