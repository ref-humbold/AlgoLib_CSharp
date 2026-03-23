using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Tests: Algorithms for topological sorting of a graph.
[TestFixture]
public class TopologicalSortingTests
{
    #region InputsTopologicalSort

    [Test]
    public void InputsTopologicalSort_WhenAcyclicGraph_ThenTopologicalOrder()
    {
        // given
        var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6).ToArray());

        graph.AddEdgeBetween(graph[0], graph[2]);
        graph.AddEdgeBetween(graph[0], graph[4]);
        graph.AddEdgeBetween(graph[1], graph[0]);
        graph.AddEdgeBetween(graph[1], graph[4]);
        graph.AddEdgeBetween(graph[3], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[0]);
        graph.AddEdgeBetween(graph[3], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[1]);
        graph.AddEdgeBetween(graph[5], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[4]);

        // when
        IEnumerable<Vertex<int>> result = graph.InputsTopologicalSort();

        // then
        Assert.That(
            result,
            Is.EqualTo([graph[3], graph[5], graph[1], graph[0], graph[2], graph[4]]));
    }

    [Test]
    public void InputsTopologicalSort_WhenCyclicGraph_ThenDirectedCyclicGraphException()
    {
        // given
        var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6).ToArray());

        graph.AddEdgeBetween(graph[0], graph[2]);
        graph.AddEdgeBetween(graph[0], graph[4]);
        graph.AddEdgeBetween(graph[1], graph[0]);
        graph.AddEdgeBetween(graph[1], graph[4]);
        graph.AddEdgeBetween(graph[2], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[0]);
        graph.AddEdgeBetween(graph[3], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[1]);
        graph.AddEdgeBetween(graph[5], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[4]);

        // when
        Action action = () => graph.InputsTopologicalSort();

        // then
        Assert.That(action, Throws.TypeOf<DirectedCyclicGraphException>());
    }

    [Test]
    public void InputsTopologicalSort_WhenEmptyGraph_ThenVertices()
    {
        // given
        IDirectedGraph<int, object, object> graph =
            new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 5));

        // when
        IEnumerable<Vertex<int>> result = graph.InputsTopologicalSort();

        // then
        Assert.That(result, Is.EqualTo(graph.Vertices));
    }

    #endregion
    #region DfsTopologicalSort

    [Test]
    public void DfsTopologicalSort_WhenAcyclicGraph_ThenTopologicalOrder()
    {
        // given
        var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6).ToArray());

        graph.AddEdgeBetween(graph[0], graph[2]);
        graph.AddEdgeBetween(graph[0], graph[4]);
        graph.AddEdgeBetween(graph[1], graph[0]);
        graph.AddEdgeBetween(graph[1], graph[4]);
        graph.AddEdgeBetween(graph[3], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[0]);
        graph.AddEdgeBetween(graph[3], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[1]);
        graph.AddEdgeBetween(graph[5], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[4]);

        Vertex<int>[][] expecteds =
        [
            [graph[3], graph[5], graph[1], graph[0], graph[2], graph[4]],
            [graph[5], graph[3], graph[1], graph[0], graph[2], graph[4]],
            [graph[3], graph[5], graph[1], graph[0], graph[4], graph[2]],
            [graph[5], graph[3], graph[1], graph[0], graph[4], graph[2]]
        ];

        // when
        IEnumerable<Vertex<int>> result = graph.DfsTopologicalSort();

        // then
        Assert.That(expecteds, Has.Some.EquivalentTo(result.ToArray()));
    }

    [Test]
    public void DfsTopologicalSort_WhenCyclicGraph_ThenDirectedCyclicGraphException()
    {
        // given
        var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6).ToArray());

        graph.AddEdgeBetween(graph[0], graph[2]);
        graph.AddEdgeBetween(graph[0], graph[4]);
        graph.AddEdgeBetween(graph[1], graph[0]);
        graph.AddEdgeBetween(graph[1], graph[4]);
        graph.AddEdgeBetween(graph[2], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[1]);
        graph.AddEdgeBetween(graph[3], graph[0]);
        graph.AddEdgeBetween(graph[3], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[1]);
        graph.AddEdgeBetween(graph[5], graph[2]);
        graph.AddEdgeBetween(graph[5], graph[4]);

        // when
        Action action = () => graph.DfsTopologicalSort();

        // then
        Assert.That(action, Throws.TypeOf<DirectedCyclicGraphException>());
    }

    [Test]
    public void DfsTopologicalSort_WhenEmptyGraph_ThenVertices()
    {
        // given
        var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 5));

        // when
        IEnumerable<Vertex<int>> result = graph.DfsTopologicalSort();

        // then
        Assert.That(result, Is.EqualTo(graph.Vertices));
    }

    #endregion
}
