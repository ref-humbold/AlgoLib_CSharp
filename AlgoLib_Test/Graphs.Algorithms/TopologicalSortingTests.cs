﻿using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Algolib.Graphs.Algorithms
{
    [TestFixture]
    public class TopologicalSortingTests
    {
        #region SortUsingInputs

        [Test]
        public void SortUsingInputs_WhenEmptyGraph_ThenVertices()
        {
            // given
            IDirectedGraph<int, object, object> graph =
                new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 5));
            // when
            IEnumerable<int> result = TopologicalSorting.SortUsingInputs(graph);
            // then
            result.Should().Equal(graph.Vertices);
        }

        #endregion
        #region SortUsingDfs

        [Test]
        public void SortUsingDfs_WhenEmptyGraph_ThenVertices()
        {
            // given
            IDirectedGraph<int, object, object> graph =
                new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 5));
            // when
            IEnumerable<int> result = TopologicalSorting.SortUsingDfs(graph);
            // then
            result.Should().Equal(graph.Vertices);
        }

        #endregion
    }
}
