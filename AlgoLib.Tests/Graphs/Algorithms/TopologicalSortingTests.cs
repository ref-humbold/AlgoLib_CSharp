using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
        DirectedSimpleGraph<int, object, object> graph = new DirectedSimpleGraph<int, object, object>(
            Enumerable.Range(0, 6).ToArray());

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
        IEnumerable<Vertex<int>> result = TopologicalSorting.InputsTopologicalSort(graph);

        // then
        result.Should().ContainInOrder(graph[3], graph[5], graph[1], graph[0], graph[2], graph[4]);
    }

    [Test]
    public void InputsTopologicalSort_WhenCyclicGraph_ThenDirectedCyclicGraphException()
    {
        // given
        DirectedSimpleGraph<int, object, object> graph = new DirectedSimpleGraph<int, object, object>(
                Enumerable.Range(0, 6).ToArray());

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
        Action action = () => TopologicalSorting.InputsTopologicalSort(graph);

        // then
        action.Should().Throw<DirectedCyclicGraphException>();
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
        result.Should().Equal(graph.Vertices);
    }

    #endregion
    #region DfsTopologicalSort

    [Test]
    public void DfsTopologicalSort_WhenAcyclicGraph_ThenTopologicalOrder()
    {
        // given
        DirectedSimpleGraph<int, object, object> graph = new DirectedSimpleGraph<int, object, object>(
                Enumerable.Range(0, 6).ToArray());

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

        var expecteds = new Vertex<int>[][] {
            new Vertex<int>[] { graph[3], graph[5], graph[1], graph[0], graph[2], graph[4] },
            new Vertex<int>[] { graph[5], graph[3], graph[1], graph[0], graph[2], graph[4] },
            new Vertex<int>[] { graph[3], graph[5], graph[1], graph[0], graph[4], graph[2] },
            new Vertex<int>[] { graph[5], graph[3], graph[1], graph[0], graph[4], graph[2] }
        };

        // when
        IEnumerable<Vertex<int>> result = TopologicalSorting.DfsTopologicalSort(graph);

        // then
        expecteds.Should().ContainEquivalentOf(result.ToArray());
    }

    [Test]
    public void DfsTopologicalSort_WhenCyclicGraph_ThenDirectedCyclicGraphException()
    {
        // given
        DirectedSimpleGraph<int, object, object> graph = new DirectedSimpleGraph<int, object, object>(
                Enumerable.Range(0, 6).ToArray());

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
        Action action = () => TopologicalSorting.DfsTopologicalSort(graph);

        // then
        action.Should().Throw<DirectedCyclicGraphException>();
    }

    [Test]
    public void DfsTopologicalSort_WhenEmptyGraph_ThenVertices()
    {
        // given
        IDirectedGraph<int, object, object> graph =
            new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 5));

        // when
        IEnumerable<Vertex<int>> result = graph.DfsTopologicalSort();

        // then
        result.Should().Equal(graph.Vertices);
    }

    #endregion
}
