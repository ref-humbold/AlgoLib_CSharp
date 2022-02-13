using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms
{
    [TestFixture]
    public class StronglyConnectedComponentsTests
    {
        [Test]
        public void FindScc_WhenManyComponents_ThenAllListed()
        {
            // given
            var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 10));

            graph.AddEdgeBetween(graph[0], graph[4]);
            graph.AddEdgeBetween(graph[0], graph[5]);
            graph.AddEdgeBetween(graph[1], graph[0]);
            graph.AddEdgeBetween(graph[2], graph[3]);
            graph.AddEdgeBetween(graph[3], graph[1]);
            graph.AddEdgeBetween(graph[4], graph[1]);
            graph.AddEdgeBetween(graph[4], graph[3]);
            graph.AddEdgeBetween(graph[6], graph[5]);
            graph.AddEdgeBetween(graph[6], graph[9]);
            graph.AddEdgeBetween(graph[7], graph[4]);
            graph.AddEdgeBetween(graph[7], graph[6]);
            graph.AddEdgeBetween(graph[8], graph[3]);
            graph.AddEdgeBetween(graph[8], graph[7]);
            graph.AddEdgeBetween(graph[9], graph[8]);
            // when
            List<HashSet<Vertex<int>>> result = graph.FindScc();
            // then
            result.Should().BeEquivalentTo(
                new[] {
                    new HashSet<Vertex<int>> { graph[0], graph[1], graph[3], graph[4] },
                    new HashSet<Vertex<int>> { graph[2] },
                    new HashSet<Vertex<int>> { graph[5] },
                    new HashSet<Vertex<int>> { graph[6], graph[7], graph[8], graph[9] }
                });
        }

        [Test]
        public void FindScc_WhenSingleComponent_ThenAllVertices()
        {
            // given
            var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 7));

            graph.AddEdgeBetween(graph[0], graph[1]);
            graph.AddEdgeBetween(graph[1], graph[2]);
            graph.AddEdgeBetween(graph[2], graph[3]);
            graph.AddEdgeBetween(graph[3], graph[4]);
            graph.AddEdgeBetween(graph[4], graph[5]);
            graph.AddEdgeBetween(graph[5], graph[6]);
            graph.AddEdgeBetween(graph[6], graph[0]);
            // when
            List<HashSet<Vertex<int>>> result = graph.FindScc();
            // then
            result.Should().BeEquivalentTo(new[] { new HashSet<Vertex<int>>(graph.Vertices) });
        }

        [Test]
        public void FindScc_WhenEmptyGraph_ThenEachVertexIsComponent()
        {
            // given
            var graph = new DirectedSimpleGraph<int, object, object>(Enumerable.Range(0, 4));
            // when
            List<HashSet<Vertex<int>>> result = graph.FindScc();
            // then
            result.Should().BeEquivalentTo(
                new[] {
                    new HashSet<Vertex<int>> { graph[0] },
                    new HashSet<Vertex<int>> { graph[1] },
                    new HashSet<Vertex<int>> { graph[2] },
                    new HashSet<Vertex<int>> { graph[3] }
                });
        }
    }
}
