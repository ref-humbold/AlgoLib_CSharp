// Algorithm for computing diameter of a tree.
using System;
using System.Linq;

namespace AlgoLib.Graphs.Algorithms
{
    public static class TreeDiameter
    {
        /// <summary>Computes length of diameter of given tree graph.</summary>
        /// <typeparam name="TVertexId">The type of vertex identifier.</typeparam>
        /// <typeparam name="TVertexProperty">The type of vertex properties.</typeparam>
        /// <typeparam name="TEdgeProperty">The type of edge properties.</typeparam>
        /// <param name="tree">The tree graph.</param>
        /// <returns>The length of the tree diameter.</returns>
        public static double CountDiameter<TVertexId, TVertexProperty, TEdgeProperty>(
            this TreeGraph<TVertexId, TVertexProperty, TEdgeProperty> tree)
            where TEdgeProperty : IWeighted
        {
            Vertex<TVertexId> root = tree.Vertices.Aggregate<Vertex<TVertexId>, Vertex<TVertexId>>(
                null,
                (acc, v) => acc is null || tree.GetOutputDegree(v) > tree.GetOutputDegree(acc) ? v : acc);

            return root is null ? 0.0 : dfs(tree, root, root).Subtree;
        }

        private static (double From, double Subtree) dfs<TVertexId, TVertexProperty, TEdgeProperty>(
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
                    (double childFrom, double childSubtree) = dfs(tree, neighbour, vertex);

                    pathThrough = Math.Max(pathThrough, pathFrom + childFrom + weight);
                    pathSubtree = Math.Max(pathSubtree, childSubtree);
                    pathFrom = Math.Max(pathFrom, childFrom + weight);
                }
            }

            return (pathFrom, Math.Max(pathThrough, pathSubtree));
        }
    }
}
