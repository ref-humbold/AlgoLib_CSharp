﻿// Tests: Algorithms for graph searching
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Graphs.Algorithms
{
    [TestFixture]
    public class SearchingTests
    {
        private DirectedSimpleGraph<int, object, object> directedGraph;
        private UndirectedSimpleGraph<int, object, object> undirectedGraph;

        [SetUp]
        public void SetUp()
        {
            directedGraph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 10));
            directedGraph.AddEdgeBetween(0, 1);
            directedGraph.AddEdgeBetween(1, 3);
            directedGraph.AddEdgeBetween(1, 7);
            directedGraph.AddEdgeBetween(3, 4);
            directedGraph.AddEdgeBetween(4, 0);
            directedGraph.AddEdgeBetween(5, 4);
            directedGraph.AddEdgeBetween(5, 8);
            directedGraph.AddEdgeBetween(6, 2);
            directedGraph.AddEdgeBetween(6, 9);
            directedGraph.AddEdgeBetween(8, 5);

            undirectedGraph = new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 10));
            undirectedGraph.AddEdgeBetween(0, 1);
            undirectedGraph.AddEdgeBetween(0, 4);
            undirectedGraph.AddEdgeBetween(1, 3);
            undirectedGraph.AddEdgeBetween(1, 7);
            undirectedGraph.AddEdgeBetween(2, 6);
            undirectedGraph.AddEdgeBetween(3, 4);
            undirectedGraph.AddEdgeBetween(4, 5);
            undirectedGraph.AddEdgeBetween(5, 8);
            undirectedGraph.AddEdgeBetween(6, 9);
        }

        #region Bfs

        [Test]
        public void Bfs_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.Bfs(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { 0 });
            // then
            result.Should().BeSubsetOf(undirectedGraph.Vertices);
            result.Should().NotContain(2);
            result.Should().NotContain(6);
            result.Should().NotContain(9);
        }

        [Test]
        public void Bfs_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result = Searching.Bfs(undirectedGraph, strategy, new List<int>() { 0, 6 });
            // then
            result.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        [Test]
        public void Bfs_WhenUndirectedGraphAndNoRoots_ThenEmpty()
        {
            // when
            IEnumerable<int> result =
                    Searching.Bfs(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { });
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void Bfs_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.Bfs(directedGraph, new EmptyStrategy<int>(), new List<int>() { 1 });
            // then
            result.Should().BeEquivalentTo(new List<int>() { 0, 1, 3, 4, 7 });
        }

        [Test]
        public void Bfs_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result = Searching.Bfs(directedGraph, strategy, new List<int>() { 8, 6 });
            // then
            result.Should().BeEquivalentTo(directedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        #endregion

        #region DfsIterative

        [Test]
        public void DfsIterative_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsIterative(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { 0 });
            // then
            result.Should().BeSubsetOf(undirectedGraph.Vertices);
            result.Should().NotContain(2);
            result.Should().NotContain(6);
            result.Should().NotContain(9);
        }

        [Test]
        public void DfsIterative_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result =
                    Searching.DfsIterative(undirectedGraph, strategy, new List<int>() { 0, 6 });
            // then
            result.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        [Test]
        public void DfsIterative_WhenUndirectedGraphAndNoRoots_ThenEmpty()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsIterative(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { });
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void DfsIterative_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsIterative(directedGraph, new EmptyStrategy<int>(), new List<int>() { 1 });
            // then
            result.Should().BeEquivalentTo(new List<int>() { 0, 1, 3, 4, 7 });
        }

        [Test]
        public void DfsIterative_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result = Searching.DfsIterative(directedGraph, strategy, new List<int>() { 8, 6 });
            // then
            result.Should().BeEquivalentTo(directedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        #endregion

        #region DfsRecursive

        [Test]
        public void DfsRecursive_WhenUndirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsRecursive(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { 0 });
            // then
            result.Should().BeSubsetOf(undirectedGraph.Vertices);
            result.Should().NotContain(2);
            result.Should().NotContain(6);
            result.Should().NotContain(9);
        }

        [Test]
        public void DfsRecursive_WhenUndirectedGraphAndManyRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result =
                    Searching.DfsRecursive(undirectedGraph, strategy, new List<int>() { 0, 6 });
            // then
            result.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        [Test]
        public void DfsRecursive_WhenUndirectedGraphAndNoRoots_ThenEmpty()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsRecursive(undirectedGraph, new EmptyStrategy<int>(), new List<int>() { });
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void DfsRecursive_WhenDirectedGraphAndSingleRoot_ThenVisitedVertices()
        {
            // when
            IEnumerable<int> result =
                    Searching.DfsRecursive(directedGraph, new EmptyStrategy<int>(), new List<int>() { 1 });
            // then
            result.Should().BeEquivalentTo(new List<int>() { 0, 1, 3, 4, 7 });
        }

        [Test]
        public void DfsRecursive_WhenDirectedGraphAndMultipleRoots_ThenAllVertices()
        {
            // given
            TestingStrategy<int> strategy = new TestingStrategy<int>();
            // when
            IEnumerable<int> result = Searching.DfsRecursive(directedGraph, strategy, new List<int>() { 8, 6 });
            // then
            result.Should().BeEquivalentTo(directedGraph.Vertices);
            strategy.entries.Should().BeEquivalentTo(undirectedGraph.Vertices);
            strategy.exits.Should().BeEquivalentTo(undirectedGraph.Vertices);
        }

        #endregion

        private class TestingStrategy<V> : IDfsStrategy<V>
        {
            internal readonly HashSet<V> entries = new HashSet<V>();
            internal readonly HashSet<V> exits = new HashSet<V>();

            public void ForRoot(V root)
            {
            }

            public void OnEntry(V vertex) => entries.Add(vertex);

            public void OnNextVertex(V vertex, V neighbour)
            {
            }

            public void OnExit(V vertex) => exits.Add(vertex);

            public void OnEdgeToVisited(V vertex, V neighbour)
            {
            }
        }
    }
}
