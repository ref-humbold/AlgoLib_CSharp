using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms
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
            graph.AddEdgeBetween(graph[0], graph[1]);
            graph.AddEdgeBetween(graph[0], graph[2]);
            graph.AddEdgeBetween(graph[0], graph[7]);
            graph.AddEdgeBetween(graph[1], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[3]);
            graph.AddEdgeBetween(graph[1], graph[4]);
            graph.AddEdgeBetween(graph[3], graph[5]);
            graph.AddEdgeBetween(graph[4], graph[5]);
            graph.AddEdgeBetween(graph[5], graph[6]);
            graph.AddEdgeBetween(graph[7], graph[8]);
            graph.AddEdgeBetween(graph[7], graph[9]);
            graph.AddEdgeBetween(graph[7], graph[11]);
            graph.AddEdgeBetween(graph[8], graph[9]);
            graph.AddEdgeBetween(graph[9], graph[10]);
            graph.AddEdgeBetween(graph[9], graph[11]);
            graph.AddEdgeBetween(graph[10], graph[11]);
            // when
            IEnumerable<Edge<int>> result = graph.FindEdgeCut();
            // then
            result.Should().BeEquivalentTo(
                new List<Edge<int>>() { graph[graph[0], graph[7]], graph[graph[5], graph[6]] });
        }

        [Test]
        public void FindEdgeCut_WhenNoBridges_ThenEmptyList()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6));
            graph.AddEdgeBetween(graph[0], graph[1]);
            graph.AddEdgeBetween(graph[0], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[3]);
            graph.AddEdgeBetween(graph[1], graph[4]);
            graph.AddEdgeBetween(graph[3], graph[5]);
            graph.AddEdgeBetween(graph[4], graph[5]);
            // when
            IEnumerable<Edge<int>> result = graph.FindEdgeCut();
            // then
            result.Should().BeEmpty();
        }

        [Test]
        public void FindVertexCut_WhenPresentSeparators_ThenSeparators()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 12));
            graph.AddEdgeBetween(graph[0], graph[1]);
            graph.AddEdgeBetween(graph[0], graph[2]);
            graph.AddEdgeBetween(graph[0], graph[7]);
            graph.AddEdgeBetween(graph[1], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[3]);
            graph.AddEdgeBetween(graph[1], graph[4]);
            graph.AddEdgeBetween(graph[3], graph[5]);
            graph.AddEdgeBetween(graph[4], graph[5]);
            graph.AddEdgeBetween(graph[5], graph[6]);
            graph.AddEdgeBetween(graph[7], graph[8]);
            graph.AddEdgeBetween(graph[7], graph[9]);
            graph.AddEdgeBetween(graph[7], graph[11]);
            graph.AddEdgeBetween(graph[8], graph[9]);
            graph.AddEdgeBetween(graph[9], graph[10]);
            graph.AddEdgeBetween(graph[9], graph[11]);
            graph.AddEdgeBetween(graph[10], graph[11]);
            // when
            IEnumerable<Vertex<int>> result = graph.FindVertexCut();
            // then
            result.Should().BeEquivalentTo(new List<Vertex<int>>() { graph[0], graph[1], graph[5], graph[7] });
        }

        [Test]
        public void FindVertexCut_WhenNoSeparators_ThenEmptyList()
        {
            // given
            UndirectedSimpleGraph<int, object, object> graph =
                new UndirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 6));
            graph.AddEdgeBetween(graph[0], graph[1]);
            graph.AddEdgeBetween(graph[0], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[2]);
            graph.AddEdgeBetween(graph[1], graph[3]);
            graph.AddEdgeBetween(graph[1], graph[4]);
            graph.AddEdgeBetween(graph[2], graph[3]);
            graph.AddEdgeBetween(graph[3], graph[5]);
            graph.AddEdgeBetween(graph[4], graph[5]);
            // when
            IEnumerable<Vertex<int>> result = graph.FindVertexCut();
            // then
            result.Should().BeEmpty();
        }
    }
}
