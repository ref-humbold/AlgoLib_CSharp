using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Tests: Hopcroft-Karp algorithm for matching in a bipartite graph.
[TestFixture]
public class MatchingTests
{
    [Test]
    public void Match_WhenMatchingExists_ThenMaximalMatching()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(
            2, new[] { new[] { 0, 2, 4, 6 }, new[] { 1, 3, 5, 7 } });
        graph.AddEdgeBetween(graph[0], graph[3]);
        graph.AddEdgeBetween(graph[0], graph[5]);
        graph.AddEdgeBetween(graph[1], graph[2]);
        graph.AddEdgeBetween(graph[3], graph[4]);
        graph.AddEdgeBetween(graph[3], graph[6]);
        graph.AddEdgeBetween(graph[6], graph[7]);

        int[] matches = new[] { 5, 2, 1, 4, 3, 0, 7, 6 };
        var expected = Enumerable.Range(0, matches.Length).ToDictionary(i => graph[i], i => graph[matches[i]]);

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        result.Should().ContainKeys(graph.Vertices);
        result.Should().Contain(expected);
    }

    [Test]
    public void Match_WhenVerticesOnlyInGroup0_ThenEmpty()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(2, new[] { new[] { 0, 1, 2, 3, 4 } });

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void Match_WhenVerticesOnlyInGroup1_ThenEmpty()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(
            2, new[] { new int[] { }, new[] { 0, 1, 2, 3, 4 } });

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        result.Should().BeEmpty();
    }

    [Test]
    public void Match_WhenTooManyGroups_ThenArgumentException()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(
            3, new[] { new[] { 0 }, new[] { 1, 2 }, new[] { 3, 4 } });

        // when
        Action action = () => graph.Match();

        // then
        action.Should().Throw<ArgumentException>();
    }
}
