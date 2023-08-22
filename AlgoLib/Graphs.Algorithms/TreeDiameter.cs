// Algorithm for counting diameter of a tree
using System;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class TreeDiameter
    {
        /// <summary>Computes length of diameter of given tree.</summary>
        /// <param name="tree">The tree graph.</param>
        /// <typeparam name="TVertexId">Type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">Type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">Type of edge properties.</typeparam>
        /// <returns>Diameter length.</returns>
        public static double CountDiameter<TVertexId, TVertexProperty, TEdgeProperty>(
            this TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> tree)
            where TEdgeProperty : IWeighted
        {
            Vertex<TVertexId> root = tree.Vertices.Aggregate<Vertex<TVertexId>, Vertex<TVertexId>>(
                null,
                (acc, v) => acc is null || tree.GetOutputDegree(v) > tree.GetOutputDegree(acc) ? v : acc);

            return root is null ? 0.0 : dfs(tree, root, root).Item2;
        }

        private static (double, double) dfs<TVertexId, TVertexProperty, TEdgeProperty>(
            TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> tree,
            Vertex<TVertexId> vertex,
            Vertex<TVertexId> parent)
            where TEdgeProperty : IWeighted
        {
            double pathFrom = 0.0;
            double pathSubtree = 0.0;
            double pathThrough = 0.0;

            foreach(Edge<TVertexId> edge in tree.GetAdjacentEdges(vertex))
            {
                Vertex<TVertexId> neighbour = edge.GetNeighbour(vertex);

                if(!neighbour.Equals(parent))
                {
                    double weight = tree.Properties[edge].Weight;
                    (double, double) result = dfs(tree, neighbour, vertex);

                    pathThrough = Math.Max(pathThrough, pathFrom + result.Item1 + weight);
                    pathSubtree = Math.Max(pathSubtree, result.Item2);
                    pathFrom = Math.Max(pathFrom, result.Item1 + weight);
                }
            }

            return (pathFrom, Math.Max(pathThrough, pathSubtree));
        }
    }
}
