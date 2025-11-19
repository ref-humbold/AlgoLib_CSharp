using System.Linq;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Tests: Algorithms for minimal spanning tree.
[TestFixture]
public class MinimalSpanningTreeTest
{
    private const double Precision = 1e-6;
    private UndirectedSimpleGraph<int, object, WeightProp> graph;

    [SetUp]
    public void SetUp()
    {
        graph = new UndirectedSimpleGraph<int, object, WeightProp>(
            Enumerable.Range(0, 5).ToArray());

        graph.AddEdgeBetween(graph[0], graph[1], new WeightProp { Weight = -1.0 });
        graph.AddEdgeBetween(graph[0], graph[2], new WeightProp { Weight = 4.0 });
        graph.AddEdgeBetween(graph[1], graph[2], new WeightProp { Weight = 9.0 });
        graph.AddEdgeBetween(graph[1], graph[3], new WeightProp { Weight = 7.0 });
        graph.AddEdgeBetween(graph[1], graph[4], new WeightProp { Weight = 12.0 });
        graph.AddEdgeBetween(graph[2], graph[4], new WeightProp { Weight = 6.0 });
        graph.AddEdgeBetween(graph[3], graph[4], new WeightProp { Weight = 3.0 });
    }

    #region Kruskal

    [Test]
    public void Kruskal_ThenMinimalSpanningTree()
    {
        // when
        IUndirectedGraph<int, object, WeightProp> result = graph.Kruskal();

        // then
        double mstSize = result.Edges
                               .Select(edge => result.Properties[edge].Weight)
                               .Sum();

        Assert.That(result.VerticesCount, Is.EqualTo(graph.VerticesCount));
        Assert.That(result.Vertices, Is.EquivalentTo(graph.Vertices));
        Assert.That(result.EdgesCount, Is.EqualTo(4));
        Assert.That(
            result.Edges,
            Is.EquivalentTo([graph[0, 1], graph[0, 2], graph[2, 4], graph[3, 4]]));
        Assert.That(mstSize, Is.EqualTo(12.0).Within(Precision));
    }

    #endregion
    #region Prim

    [Test]
    public void Prim_ThenMinimalSpanningTree()
    {
        // when
        IUndirectedGraph<int, object, WeightProp>
            result = MinimalSpanningTree.Prim(graph, graph[0]);

        // then
        double mstSize = result.Edges
                               .Select(edge => result.Properties[edge].Weight)
                               .Sum();

        Assert.That(result.VerticesCount, Is.EqualTo(graph.VerticesCount));
        Assert.That(result.Vertices, Is.EquivalentTo(graph.Vertices));
        Assert.That(result.EdgesCount, Is.EqualTo(4));
        Assert.That(
            result.Edges,
            Is.EquivalentTo([graph[0, 1], graph[0, 2], graph[2, 4], graph[3, 4]]));
        Assert.That(mstSize, Is.EqualTo(12.0).Within(Precision));
    }

    [Test]
    public void Prim_WhenDifferentSources_ThenSameMinimalSpanningTree()
    {
        // when
        IUndirectedGraph<int, object, WeightProp> result1 =
            MinimalSpanningTree.Prim(graph, graph[1]);
        IUndirectedGraph<int, object, WeightProp> result4 =
            MinimalSpanningTree.Prim(graph, graph[4]);

        // then
        Assert.That(result1.EdgesCount, Is.EqualTo(result4.EdgesCount));
        Assert.That(result1.Edges, Is.EquivalentTo(result4.Edges));
    }

    #endregion

    private class WeightProp : IWeighted
    {
        public double Weight { get; set; }
    }
}
