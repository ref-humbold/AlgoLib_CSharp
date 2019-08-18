// Directed graphs structures.
using System;
using System.Linq;
using System.Collections.Generic;

namespace Algolib.Graphs
{
    public interface IDirectedGraph
    {
        /// <summary>Odwracanie skierowania grafu</summary>
        void Reverse();
    }

    public class DirectedSimpleGraph : SimpleGraph, IDirectedGraph
    {
        public DirectedSimpleGraph(int n) : base(n)
        {
        }

        public DirectedSimpleGraph(int n, List<Tuple<int, int>> edges) : base(n)
        {
            foreach(var e in edges)
                graphrepr[(int)e.Item1].Add(Tuple.Create(e.Item2, DEFAULT_WEIGHT));
        }

        public override int EdgesNumber => Vertices.Aggregate(0, (int acc, int v) => acc + GetOutdegree(v));

        public override IEnumerable<Tuple<int, int>> Edges => Vertices.SelectMany(
                v => GetNeighbours(v).Select(u => Tuple.Create(v, u)));

        public override void AddEdge(int v, int u)
        {
            if(v > VerticesNumber || u > VerticesNumber)
                throw new ArgumentOutOfRangeException("No such vertex.");

            graphrepr[v].Add(Tuple.Create(u, DEFAULT_WEIGHT));
        }

        public override int GetIndegree(int v) => Edges.Aggregate(0, (int acc, Tuple<int, int> e) => e.Item1 == v ? acc + 1 : acc);

        public void Reverse()
        {
            List<HashSet<Tuple<int, double>>> revgraphrepr = new List<HashSet<Tuple<int, double>>>();

            foreach(var v in Vertices)
                revgraphrepr.Add(new HashSet<Tuple<int, double>>());

            foreach(var e in Edges)
                revgraphrepr[e.Item2].Add(Tuple.Create(e.Item1, DEFAULT_WEIGHT));

            graphrepr = revgraphrepr;
        }
    }
}
