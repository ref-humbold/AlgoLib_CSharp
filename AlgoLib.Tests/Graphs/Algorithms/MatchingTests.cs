using System;
using System.Collections.Generic;
using System.Linq;
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
        var graph = new MultipartiteGraph<int, object, object>(2, [[0, 2, 4, 6], [1, 3, 5, 7]]);
        graph.AddEdgeBetween(graph[0], graph[3]);
        graph.AddEdgeBetween(graph[0], graph[5]);
        graph.AddEdgeBetween(graph[1], graph[2]);
        graph.AddEdgeBetween(graph[3], graph[4]);
        graph.AddEdgeBetween(graph[3], graph[6]);
        graph.AddEdgeBetween(graph[6], graph[7]);

        int[] matches = [5, 2, 1, 4, 3, 0, 7, 6];
        Dictionary<Vertex<int>, Vertex<int>> expected = Enumerable.Range(0, matches.Length)
            .ToDictionary(i => graph[i], i => graph[matches[i]]);

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        foreach(Vertex<int> vertex in graph.Vertices)
            Assert.That(result, Does.ContainKey(vertex));

        foreach(KeyValuePair<Vertex<int>, Vertex<int>> entry in expected)
            Assert.That(result, Does.Contain(entry));
    }

    [Test]
    public void Match_WhenVerticesOnlyInGroup0_ThenEmpty()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(2, [[0, 1, 2, 3, 4]]);

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Match_WhenVerticesOnlyInGroup1_ThenEmpty()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(2, [[], [0, 1, 2, 3, 4]]);

        // when
        Dictionary<Vertex<int>, Vertex<int>> result = graph.Match();

        // then
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Match_WhenTooManyGroups_ThenArgumentException()
    {
        // given
        var graph = new MultipartiteGraph<int, object, object>(3, [[0], [1, 2], [3, 4]]);

        // when
        Action action = () => graph.Match();

        // then
        Assert.That(action, Throws.ArgumentException);
    }
}
