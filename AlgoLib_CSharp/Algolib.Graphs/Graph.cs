// Graphs structures.
using System;
using System.Linq;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IGraph
    {
        /// <summary>Oznaczenie nieskończoności</summary>
        double Inf { get; }

        /// <summary>Liczba wierzchołków</summary>
        int VerticesCount { get; }

        /// <summary>Liczba krawędzi</summary>
        int EdgesCount { get; }

        /// <summary>Lista wierzchołków</summary>
        IEnumerable<int> Vertices { get; }

        /// <summary>Lista krawędzi</summary>
        IEnumerable<Tuple<int, int>> Edges { get; }

        /// <summary>Dodawanie nowego wierzchołka</summary>
        /// <returns>oznaczenie wierzchołka</returns>
        int AddVertex();

        /// <summary>Dodawanie nowej krawędzi</summary>
        /// <param name="v">początkowy wierzchołek</param>
        /// <param name="u">końcowy wierzchołek</param>
        void AddEdge(int v, int u);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>lista sąsiadów wierzchołka</returns>
        IEnumerable<int> GetNeighbours(int v);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>stopień wyjściowy wierzchołka</returns>
        int GetOutdegree(int v);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>stopień wejściowy wierzchołka</returns>
        int GetIndegree(int v);
    }

    public interface IWeightedGraph : IGraph
    {
        /// <summary>Lista krawędzi z wagami</summary>
        IEnumerable<Tuple<int, int, double>> WeightedEdges { get; }

        /// <summary>Dodawanie nowej krawędzi z jej wagą</summary>
        /// <param name="v">początkowy wierzchołek</param>
        /// <param name="u">końcowy wierzchołek</param>
        /// <param name="wg">waga krawędzi</param>
        void AddWeightedEdge(int v, int u, double wg);

        /// <param name="v">numer wierzchołka</param>
        /// <returns>lista sąsiadów wierzchołka wraz z wagami krawędzi</returns>
        IEnumerable<Tuple<int, double>> GetWeightedNeighbours(int v);
    }

    public abstract class SimpleGraph : IGraph
    {
        /// <summary>Domyślna waga krawędzi</summary>
        protected const double DEFAULT_WEIGHT = 1.0;

        /// <summary>Lista sąsiedztwa grafu</summary>
        protected List<HashSet<Tuple<int, double>>> graphrepr;

        public SimpleGraph(int n)
        {
            graphrepr = new List<HashSet<Tuple<int, double>>>(n);
        }

        public double Inf => double.PositiveInfinity;

        public int VerticesCount => graphrepr.Count;

        public abstract int EdgesCount { get; }

        public IEnumerable<int> Vertices => Enumerable.Range(0, VerticesCount);

        public abstract IEnumerable<Tuple<int, int>> Edges { get; }

        public int AddVertex()
        {
            graphrepr.Add(new HashSet<Tuple<int, double>>());

            return graphrepr.Count - 1;
        }

        public abstract void AddEdge(int v, int u);

        public IEnumerable<int> GetNeighbours(int v) => graphrepr[v].Select(wv => wv.Item1);

        public int GetOutdegree(int v) => graphrepr[v].Count;

        public abstract int GetIndegree(int v);
    }
}
