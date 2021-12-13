using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms
{
    [TestFixture]
    public class TopologicalSortingTests
    {
        #region InputsTopologicalSort

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
}
