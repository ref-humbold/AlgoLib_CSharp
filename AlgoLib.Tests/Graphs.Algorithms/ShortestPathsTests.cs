﻿using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoLib.Graphs.Algorithms
{
    [TestFixture]
    public class ShortestPathsTests
    {
        private static readonly double inf = IWeighted.Infinity;
        private DirectedSimpleGraph<int, object, Weighted> directedGraph;
        private UndirectedSimpleGraph<int, object, Weighted> undirectedGraph;

        [SetUp]
        public void SetUp()
        {
            directedGraph = new DirectedSimpleGraph<int, object, Weighted>(
                Enumerable.Range(0, 10).ToList());
            directedGraph.AddEdgeBetween(directedGraph[0], directedGraph[1],
                                         new Weighted(4.0));
            directedGraph.AddEdgeBetween(directedGraph[1], directedGraph[4],
                                         new Weighted(7.0));
            directedGraph.AddEdgeBetween(directedGraph[1], directedGraph[7],
                                         new Weighted(12.0));
            directedGraph.AddEdgeBetween(directedGraph[2], directedGraph[4],
                                         new Weighted(6.0));
            directedGraph.AddEdgeBetween(directedGraph[2], directedGraph[6],
                                         new Weighted(8.0));
            directedGraph.AddEdgeBetween(directedGraph[3], directedGraph[0],
                                         new Weighted(3.0));
            directedGraph.AddEdgeBetween(directedGraph[3], directedGraph[7],
                                         new Weighted(5.0));
            directedGraph.AddEdgeBetween(directedGraph[4], directedGraph[5],
                                         new Weighted(1.0));
            directedGraph.AddEdgeBetween(directedGraph[4], directedGraph[3],
                                         new Weighted(10.0));
            directedGraph.AddEdgeBetween(directedGraph[5], directedGraph[6],
                                         new Weighted(4.0));
            directedGraph.AddEdgeBetween(directedGraph[5], directedGraph[8],
                                         new Weighted(2.0));
            directedGraph.AddEdgeBetween(directedGraph[6], directedGraph[5],
                                         new Weighted(7.0));
            directedGraph.AddEdgeBetween(directedGraph[7], directedGraph[5],
                                         new Weighted(2.0));
            directedGraph.AddEdgeBetween(directedGraph[7], directedGraph[8],
                                         new Weighted(6.0));
            directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[9],
                                         new Weighted(10.0));
            directedGraph.AddEdgeBetween(directedGraph[9], directedGraph[6],
                                         new Weighted(3.0));

            undirectedGraph = new UndirectedSimpleGraph<int, object, Weighted>(
                Enumerable.Range(0, 10).ToList());
            undirectedGraph.AddEdgeBetween(undirectedGraph[0], undirectedGraph[1],
                                           new Weighted(4.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[1], undirectedGraph[4],
                                           new Weighted(7.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[1], undirectedGraph[7],
                                           new Weighted(12.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[2], undirectedGraph[6],
                                           new Weighted(8.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[3], undirectedGraph[0],
                                           new Weighted(3.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[3], undirectedGraph[7],
                                           new Weighted(5.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[4], undirectedGraph[5],
                                           new Weighted(1.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[4], undirectedGraph[3],
                                           new Weighted(10.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[5], undirectedGraph[8],
                                           new Weighted(2.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[7], undirectedGraph[5],
                                           new Weighted(2.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[7], undirectedGraph[8],
                                           new Weighted(6.0));
            undirectedGraph.AddEdgeBetween(undirectedGraph[9], undirectedGraph[6],
                                           new Weighted(3.0));
        }

        #region BellmanFord

        [Test]
        public void BellmanFord_WhenDirectedGraph_ThenShortestPathsLengths()
        {
            // given
            var distances = new List<double> { 20.0, 0.0, inf, 17.0, 7.0, 8.0, 12.0, 12.0, 10.0, 20.0 };
            Dictionary<Vertex<int>, double> expected = fromList(directedGraph, distances);
            // when
            Dictionary<Vertex<int>, double> result =
                    ShortestPaths.BellmanFord(directedGraph, directedGraph[1]);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BellmanFord_WhenNegativeEdge_ThenEdgeIncluded()
        {
            // given
            var distances = new List<double> { 8.0, 0.0, inf, 5.0, 7.0, 8.0, 12.0, 10.0, 10.0, 20.0 };
            Dictionary<Vertex<int>, double> expected = fromList(directedGraph, distances);

            directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[3],
                                         new Weighted(-5.0));
            // when
            Dictionary<Vertex<int>, double> result =
                    ShortestPaths.BellmanFord(directedGraph, directedGraph[1]);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BellmanFord_WhenUndirectedGraph_ThenShortestPathsLengths()
        {
            // given
            var distances = new List<double> { 4.0, 0.0, inf, 7.0, 7.0, 8.0, inf, 10.0, 10.0, inf };
            Dictionary<Vertex<int>, double> expected = fromList(undirectedGraph, distances);
            // when
            Dictionary<Vertex<int>, double> result =
                    ShortestPaths.BellmanFord(undirectedGraph.AsDirected(), undirectedGraph[1]);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BellmanFord_WhenNegativeCycle_ThenIllegalStateException()
        {
            // given
            directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[3],
                                         new Weighted(-20.0));
            // when
            Action action = () => ShortestPaths.BellmanFord(directedGraph, directedGraph[1]);
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        #endregion
        #region Dijkstra

        [Test]
        public void Dijkstra_WhenDirectedGraph_ThenShortestPathsLengths()
        {
            // given
            var distances = new List<double> { 20.0, 0.0, inf, 17.0, 7.0, 8.0, 12.0, 12.0, 10.0, 20.0 };
            Dictionary<Vertex<int>, double> expected = fromList(directedGraph, distances);
            // when
            Dictionary<Vertex<int>, double> result =
                    ShortestPaths.Dijkstra(directedGraph, directedGraph[1]);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Dijkstra_WhenUndirectedGraph_ThenShortestPathsLengths()
        {
            // given
            var distances = new List<double> { 4.0, 0.0, inf, 7.0, 7.0, 8.0, inf, 10.0, 10.0, inf };
            Dictionary<Vertex<int>, double> expected = fromList(undirectedGraph, distances);
            // when
            Dictionary<Vertex<int>, double> result =
                    ShortestPaths.Dijkstra(undirectedGraph, undirectedGraph[1]);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void Dijkstra_WhenNegativeEdge_ThenIllegalStateException()
        {
            // given
            directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[3],
                                         new Weighted(-5.0));
            // when
            Action action = () => ShortestPaths.Dijkstra(directedGraph, directedGraph[1]);
            // then
            action.Should().Throw<InvalidOperationException>();
        }

        #endregion
        #region FloydWarshall

        [Test]
        public void FloydWarshall_WhenDirectedGraph_ThenAllShortestPathsLengths()
        {
            // given
            double[,] distances = new double[,] {
                { 0.0, 4.0, inf, 21.0, 11.0, 12.0, 16.0, 16.0, 14.0, 24.0 },
                { 20.0, 0.0, inf, 17.0, 7.0, 8.0, 12.0, 12.0, 10.0, 20.0 },
                { 19.0, 23.0, 0.0, 16.0, 6.0, 7.0, 8.0, 21.0, 9.0, 19.0 },
                { 3.0, 7.0, inf, 0.0, 14.0, 7.0, 11.0, 5.0, 9.0, 19.0 },
                { 13.0, 17.0, inf, 10.0, 0.0, 1.0, 5.0, 15.0, 3.0, 13.0 },
                { inf, inf, inf, inf, inf, 0.0, 4.0, inf, 2.0, 12.0 },
                { inf, inf, inf, inf, inf, 7.0, 0.0, inf, 9.0, 19.0 },
                { inf, inf, inf, inf, inf, 2.0, 6.0, 0.0, 4.0, 14.0 },
                { inf, inf, inf, inf, inf, 20.0, 13.0, inf, 0.0, 10.0 },
                { inf, inf, inf, inf, inf, 10.0, 3.0, inf, 12.0, 0.0 }
            };
            Dictionary<(Vertex<int>, Vertex<int>), double> expected =
                fromMatrix(undirectedGraph, distances);
            // when
            Dictionary<(Vertex<int>, Vertex<int>), double> result =
                    ShortestPaths.FloydWarshall(directedGraph);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FloydWarshall_WhenNegativeEdge_ThenEdgeIncluded()
        {
            // given
            double[,] distances = new double[,] {
                { 0.0, 4.0, inf, 9.0, 11.0, 12.0, 16.0, 14.0, 14.0, 24.0 },
                { 8.0, 0.0, inf, 5.0, 7.0, 8.0, 12.0, 10.0, 10.0, 20.0 },
                { 7.0, 11.0, 0.0, 4.0, 6.0, 7.0, 8.0, 9.0, 9.0, 19.0 },
                { 3.0, 7.0, inf, 0.0, 14.0, 7.0, 11.0, 5.0, 9.0, 19.0 },
                { 1.0, 5.0, inf, -2.0, 0.0, 1.0, 5.0, 3.0, 3.0, 13.0 },
                { 0.0, 4.0, inf, -3.0, 11.0, 0.0, 4.0, 2.0, 2.0, 12.0 },
                { 7.0, 11.0, inf, 4.0, 18.0, 7.0, 0.0, 9.0, 9.0, 19.0 },
                { 2.0, 6.0, inf, -1.0, 13.0, 2.0, 6.0, 0.0, 4.0, 14.0 },
                { -2.0, 2.0, inf, -5.0, 9.0, 2.0, 6.0, 0.0, 0.0, 10.0 },
                { 10.0, 14.0, inf, 7.0, 21.0, 10.0, 3.0, 12.0, 12.0, 0.0 }
            };
            Dictionary<(Vertex<int>, Vertex<int>), double> expected =
                    fromMatrix(undirectedGraph, distances);

            directedGraph.AddEdgeBetween(directedGraph[8], directedGraph[3],
                                         new Weighted(-5.0));
            // when
            Dictionary<(Vertex<int>, Vertex<int>), double> result =
                    ShortestPaths.FloydWarshall(directedGraph);
            // then
            result.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void FloydWarshall_WhenUndirectedGraph_ThenAllShortestPathsLengths()
        {
            // given
            double[,] distances = new double[,] {
                { 0.0, 4.0, inf, 3.0, 11.0, 10.0, inf, 8.0, 12.0, inf },
                { 4.0, 0.0, inf, 7.0, 7.0, 8.0, inf, 10.0, 10.0, inf },
                { inf, inf, 0.0, inf, inf, inf, 8.0, inf, inf, 11.0 },
                { 3.0, 7.0, inf, 0.0, 8.0, 7.0, inf, 5.0, 9.0, inf },
                { 11.0, 7.0, inf, 8.0, 0.0, 1.0, inf, 3.0, 3.0, inf },
                { 10, 8, inf, 7.0, 1.0, 0.0, inf, 2.0, 2.0, inf },
                { inf, inf, 8.0, inf, inf, inf, 0.0, inf, inf, 3.0 },
                { 8.0, 10.0, inf, 5.0, 3.0, 2.0, inf, 0.0, 4.0, inf },
                { 12.0, 10.0, inf, 9.0, 3.0, 2.0, inf, 4.0, 0.0, inf },
                { inf, inf, 11.0, inf, inf, inf, 3.0, inf, inf, 0.0 }
            };
            Dictionary<(Vertex<int>, Vertex<int>), double> expected =
                    fromMatrix(undirectedGraph, distances);
            // when
            Dictionary<(Vertex<int>, Vertex<int>), double> result =
                    ShortestPaths.FloydWarshall(undirectedGraph.AsDirected());
            // then
            result.Should().BeEquivalentTo(expected);
        }

        #endregion

        private Dictionary<Vertex<int>, double> fromList(IGraph<int, object, Weighted> graph,
                                                         List<double> distances)
        {
            var dictionary = new Dictionary<Vertex<int>, double>();

            for(int i = 0; i < distances.Count; ++i)
                dictionary.Add(graph[i], distances[i]);

            return dictionary;
        }

        private Dictionary<(Vertex<int>, Vertex<int>), double> fromMatrix(
                IGraph<int, object, Weighted> graph, double[,] distances)
        {
            var dictionary = new Dictionary<(Vertex<int>, Vertex<int>), double>();

            for(int i = 0; i < distances.GetLength(0); ++i)
                for(int j = 0; j < distances.GetLength(1); ++j)
                    dictionary.Add((graph[i], graph[j]), distances[i, j]);

            return dictionary;
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
}