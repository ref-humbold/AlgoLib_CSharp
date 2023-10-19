using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Tests: Algorithms for graph searching.
[TestFixture]
public class SearchingTests
{
    private DirectedSimpleGraph<int, object, object> directedGraph;
    private UndirectedSimpleGraph<int, object, object> undirectedGraph;

    [SetUp]
    public void SetUp()
    {
        directedGraph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 10));
        directedGraph.AddEdgeBetween(directedGraph[0], directedGraph[1]);
        directedGraph.AddEdgeBetween(directedGraph[1], directedGraph[3]);
        directedGraph.AddEdgeBetween(directedGraph[1], directedGraph[7]);
        directedGraph.AddEdgeBetween(directedGraph[3], directedGraph[4]);
        directedGraph.AddEdgeBetween(directedGraph[4], directedGraph[0]);
        directedGraph.AddEdgeBetween(directedGraph[5], directedGraph[4]);
        directedGraph.AddEdgeBetween(directedGraph[5], directedGraph[8]);
        directedGraph.AddEdgeBetween(directedGraph[6], directedGraph[2]);
        directedGraph.AddEdgeBetween(directedGraph[6], directedGraph[9]);
        directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[5]);

        undirectedGraph = new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 10));
        undirectedGraph.AddEdgeBetween(undirectedGraph[0], undirectedGraph[1]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[0], undirectedGraph[4]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[1], undirectedGraph[3]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[1], undirectedGraph[7]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[2], undirectedGraph[6]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[3], undirectedGraph[4]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[4], undirectedGraph[5]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[5], undirectedGraph[8]);
        undirectedGraph.AddEdgeBetween(undirectedGraph[6], undirectedGraph[9]);
    }

    #region Bfs

    [Test]
    public void Bfs_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
            undirectedGraph.Bfs(default(EmptyStrategy<int>), new[] { undirectedGraph[0] });
        // then
        result.Should().BeSubsetOf(undirectedGraph.Vertices);
        result.Should().NotContain(undirectedGraph[2]);
        result.Should().NotContain(undirectedGraph[6]);
        result.Should().NotContain(undirectedGraph[9]);
    }

    [Test]
    public void Bfs_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
            undirectedGraph.Bfs(strategy, new[] { undirectedGraph[0], undirectedGraph[6] });
        // then
        result.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    [Test]
    public void Bfs_WhenUndirectedGraphAndNoRoots_ThenEmpty()
    {
        // when
        IEnumerable<Vertex<int>> result =
                undirectedGraph.Bfs(default(EmptyStrategy<int>), Array.Empty<Vertex<int>>());
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void Bfs_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
            directedGraph.Bfs(default(EmptyStrategy<int>), new[] { directedGraph[1] });
        // then
        result.Should().BeEquivalentTo(
            new[] { directedGraph[0], directedGraph[1], directedGraph[3],
                    directedGraph[4], directedGraph[7] });
    }

    [Test]
    public void Bfs_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
            directedGraph.Bfs(strategy, new[] { directedGraph[8], directedGraph[6] });
        // then
        result.Should().BeEquivalentTo(directedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    #endregion

    #region DfsIterative

    [Test]
    public void DfsIterative_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
            undirectedGraph.DfsIterative(default(EmptyStrategy<int>), new[] { undirectedGraph[0] });
        // then
        result.Should().BeSubsetOf(undirectedGraph.Vertices);
        result.Should().NotContain(undirectedGraph[2]);
        result.Should().NotContain(undirectedGraph[6]);
        result.Should().NotContain(undirectedGraph[9]);
    }

    [Test]
    public void DfsIterative_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
            undirectedGraph.DfsIterative(strategy, new[] { undirectedGraph[0], undirectedGraph[6] });
        // then
        result.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    [Test]
    public void DfsIterative_WhenUndirectedGraphAndNoRoots_ThenEmpty()
    {
        // when
        IEnumerable<Vertex<int>> result =
                undirectedGraph.DfsIterative(default(EmptyStrategy<int>), Array.Empty<Vertex<int>>());
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void DfsIterative_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
            directedGraph.DfsIterative(default(EmptyStrategy<int>), new[] { directedGraph[1] });
        // then
        result.Should().BeEquivalentTo(
            new[] { directedGraph[0], directedGraph[1], directedGraph[3],
                     directedGraph[4], directedGraph[7] });
    }

    [Test]
    public void DfsIterative_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
            directedGraph.DfsIterative(strategy, new[] { directedGraph[8], directedGraph[6] });
        // then
        result.Should().BeEquivalentTo(directedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    #endregion

    #region DfsRecursive

    [Test]
    public void DfsRecursive_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
            undirectedGraph.DfsRecursive(default(EmptyStrategy<int>), new[] { undirectedGraph[0] });
        // then
        result.Should().BeSubsetOf(undirectedGraph.Vertices);
        result.Should().NotContain(undirectedGraph[2]);
        result.Should().NotContain(undirectedGraph[6]);
        result.Should().NotContain(undirectedGraph[9]);
    }

    [Test]
    public void DfsRecursive_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
                undirectedGraph.DfsRecursive(strategy, new[] { undirectedGraph[0], undirectedGraph[6] });
        // then
        result.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    [Test]
    public void DfsRecursive_WhenUndirectedGraphAndNoRoots_ThenEmpty()
    {
        // when
        IEnumerable<Vertex<int>> result =
                undirectedGraph.DfsRecursive(default(EmptyStrategy<int>), Array.Empty<Vertex<int>>());
        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void DfsRecursive_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
    {
        // when
        IEnumerable<Vertex<int>> result =
                directedGraph.DfsRecursive(default(EmptyStrategy<int>), new[] { directedGraph[1] });
        // then
        result.Should().BeEquivalentTo(
            new[] {
                directedGraph[0], directedGraph[1], directedGraph[3], directedGraph[4],
                directedGraph[7]
            });
    }

    [Test]
    public void DfsRecursive_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
    {
        // given
        var strategy = new TestingStrategy<int>();
        // when
        IEnumerable<Vertex<int>> result =
            directedGraph.DfsRecursive(strategy, new[] { directedGraph[8], directedGraph[6] });
        // then
        result.Should().BeEquivalentTo(directedGraph.Vertices);
        strategy.Entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
        strategy.Exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
    }

    #endregion

    private class TestingStrategy<TVertexId> : IDfsStrategy<TVertexId>
    {
        public HashSet<Vertex<TVertexId>> Entries { get; } = new();

        public HashSet<Vertex<TVertexId>> Exits { get; } = new();

        public void ForRoot(Vertex<TVertexId> root)
        {
        }

        public void OnEntry(Vertex<TVertexId> vertex) => Entries.Add(vertex);

        public void OnNextVertex(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }

        public void OnExit(Vertex<TVertexId> vertex) => Exits.Add(vertex);

        public void OnEdgeToVisited(Vertex<TVertexId> vertex, Vertex<TVertexId> neighbour)
        {
        }
    }
}
