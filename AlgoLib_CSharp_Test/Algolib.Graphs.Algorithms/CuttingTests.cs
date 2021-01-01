using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Algolib.Graphs.Algorithms
{
    [TestFixture]
    public class CuttingTests
    {
        [Test]
        public void FindEdgeCut_WhenPresentBridges_ThenBridges()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 12));
            graph.AddEdgeBetween(0, 1);
            graph.AddEdgeBetween(0, 2);
            graph.AddEdgeBetween(0, 7);
            graph.AddEdgeBetween(1, 2);
            graph.AddEdgeBetween(1, 3);
            graph.AddEdgeBetween(1, 4);
            graph.AddEdgeBetween(3, 5);
            graph.AddEdgeBetween(4, 5);
            graph.AddEdgeBetween(5, 6);
            graph.AddEdgeBetween(7, 8);
            graph.AddEdgeBetween(7, 9);
            graph.AddEdgeBetween(7, 11);
            graph.AddEdgeBetween(8, 9);
            graph.AddEdgeBetween(9, 10);
            graph.AddEdgeBetween(9, 11);
            graph.AddEdgeBetween(10, 11);
            // when
            IEnumerable<Edge<int>> result = Cutting.FindEdgeCut(graph);
            // then
            CollectionAssert.AreEquivalent(
                new List<Edge<int>>() { graph.GetEdge(0, 7), graph.GetEdge(5, 6) }, result);
        }

        [Test]
        public void FindEdgeCut_WhenNoBridges_ThenEmptyList()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6));
            graph.AddEdgeBetween(0, 1);
            graph.AddEdgeBetween(0, 2);
            graph.AddEdgeBetween(1, 2);
            graph.AddEdgeBetween(1, 3);
            graph.AddEdgeBetween(1, 4);
            graph.AddEdgeBetween(3, 5);
            graph.AddEdgeBetween(4, 5);
            // when
            IEnumerable<Edge<int>> result = Cutting.FindEdgeCut(graph);
            // then
            CollectionAssert.IsEmpty(result);
        }

        [Test]
        public void FindVertexCut_WhenPresentSeparators_ThenSeparators()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 12));
            graph.AddEdgeBetween(0, 1);
            graph.AddEdgeBetween(0, 2);
            graph.AddEdgeBetween(0, 7);
            graph.AddEdgeBetween(1, 2);
            graph.AddEdgeBetween(1, 3);
            graph.AddEdgeBetween(1, 4);
            graph.AddEdgeBetween(3, 5);
            graph.AddEdgeBetween(4, 5);
            graph.AddEdgeBetween(5, 6);
            graph.AddEdgeBetween(7, 8);
            graph.AddEdgeBetween(7, 9);
            graph.AddEdgeBetween(7, 11);
            graph.AddEdgeBetween(8, 9);
            graph.AddEdgeBetween(9, 10);
            graph.AddEdgeBetween(9, 11);
            graph.AddEdgeBetween(10, 11);
            // when
            IEnumerable<int> result = Cutting.FindVertexCut(graph);
            // then
            CollectionAssert.AreEquivalent(new List<int>() { 0, 1, 5, 7 }, result);
        }

        [Test]
        public void FindVertexCut_WhenNoSeparators_ThenEmptyList()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6));
            graph.AddEdgeBetween(0, 1);
            graph.AddEdgeBetween(0, 2);
            graph.AddEdgeBetween(1, 2);
            graph.AddEdgeBetween(1, 3);
            graph.AddEdgeBetween(1, 4);
            graph.AddEdgeBetween(2, 3);
            graph.AddEdgeBetween(3, 5);
            graph.AddEdgeBetween(4, 5);
            // when
            IEnumerable<int> result = Cutting.FindVertexCut(graph);
            // then
            CollectionAssert.IsEmpty(result);
        }
    }
}
