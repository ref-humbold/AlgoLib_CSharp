using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Algorithm for computing diameter of a tree.
[TestFixture]
public class TreeDiameterTests
{
    [Test]
    public void CountDiameter_WhenOneVertex_ThenZero()
    {
        // given
        var tree = new TreeGraph<int, object, Weighted>(0);

        // when
        double result = tree.CountDiameter();

        // then
        result.Should().Be(0);
    }

    [Test]
    public void CountDiameter_WhenAllWeightsEqual_ThenDiameterLength()
    {
        // given
        var weight = new Weighted(1);
        var tree = new TreeGraph<int, object, Weighted>(0);
        tree.AddVertex(1, tree[0], null, weight);
        tree.AddVertex(2, tree[0], null, weight);
        tree.AddVertex(3, tree[1], null, weight);
        tree.AddVertex(4, tree[1], null, weight);
        tree.AddVertex(5, tree[1], null, weight);
        tree.AddVertex(6, tree[2], null, weight);
        tree.AddVertex(7, tree[4], null, weight);
        tree.AddVertex(8, tree[6], null, weight);
        tree.AddVertex(9, tree[6], null, weight);

        // when
        double result = tree.CountDiameter();

        // then
        result.Should().Be(6);
    }

    [Test]
    public void CountDiameter_WhenEdgeWithBigWeight_ThenLongestPath()
    {
        // given
        var tree = new TreeGraph<int, object, Weighted>(0);
        tree.AddVertex(1, tree[0], null, new Weighted(1000));
        tree.AddVertex(2, tree[1], null, new Weighted(10));
        tree.AddVertex(3, tree[1], null, new Weighted(10));
        tree.AddVertex(4, tree[2], null, new Weighted(5));
        tree.AddVertex(5, tree[3], null, new Weighted(5));

        // when
        double result = tree.CountDiameter();

        // then
        result.Should().Be(1015);
    }

    private sealed class Weighted : IWeighted
    {
        public double Weight
        {
            get;
        }

        public Weighted(double weight)
        {
            Weight = weight;
        }
    }
}
