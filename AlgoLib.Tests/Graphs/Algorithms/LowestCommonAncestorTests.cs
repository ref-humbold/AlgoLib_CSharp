﻿using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms;

// Tests: Algorithm for lowest common ancestors in a rooted tree.
[TestFixture]
public class LowestCommonAncestorTests
{
    private TreeGraph<int, object, object> tree;
    private LowestCommonAncestor<int, object, object> testObject;

    [SetUp]
    public void SetUp()
    {
        tree = new TreeGraph<int, object, object>(0);
        tree.AddVertex(1, tree[0]);
        tree.AddVertex(2, tree[0]);
        tree.AddVertex(3, tree[1]);
        tree.AddVertex(4, tree[1]);
        tree.AddVertex(5, tree[1]);
        tree.AddVertex(6, tree[2]);
        tree.AddVertex(7, tree[4]);
        tree.AddVertex(8, tree[6]);
        tree.AddVertex(9, tree[6]);

        testObject = new LowestCommonAncestor<int, object, object>(tree, tree[0]);
    }

    [Test]
    public void FindLca_WhenSameVertex_ThenVertexIsLowestCommonAncestor()
    {
        // given
        Vertex<int> vertex = tree[6];

        // when
        Vertex<int> result = testObject.FindLca(vertex, vertex);

        // then
        result.Should().Be(vertex);
    }

    [Test]
    public void FindLca_WhenVerticesInDifferentSubtrees_ThenLowestCommonAncestor()
    {
        // when
        Vertex<int> result = testObject.FindLca(tree[5], tree[7]);

        // then
        result.Should().Be(tree[1]);
    }

    [Test]
    public void FindLca_WhenVerticesSwapped_ThenSameLowestCommonAncestor()
    {
        // when
        Vertex<int> result1 = testObject.FindLca(tree[5], tree[7]);
        Vertex<int> result2 = testObject.FindLca(tree[7], tree[5]);

        // then
        result1.Should().Be(tree[1]);
        result2.Should().Be(result1);
    }

    [Test]
    public void FindLca_WhenRootIsCommonAncestor_ThenRoot()
    {
        // when
        Vertex<int> result = testObject.FindLca(tree[3], tree[9]);

        // then
        result.Should().Be(testObject.Root);
    }

    [Test]
    public void FindLca_WhenVerticesAreOnSamePathFromRoot_ThenCloserToRoot()
    {
        // given
        Vertex<int> vertex1 = tree[8];
        Vertex<int> vertex2 = tree[2];

        // when
        Vertex<int> result = testObject.FindLca(vertex1, vertex2);

        // then
        result.Should().Be(vertex2);
    }

    [Test]
    public void FindLca_WhenRootIsOneOfVertices_ThenRoot()
    {
        // when
        Vertex<int> result = testObject.FindLca(tree[4], testObject.Root);

        // then
        result.Should().Be(testObject.Root);
    }
}
