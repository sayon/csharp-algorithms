using Algorithmis.DataStructures;
using Algorithmis.DataStructures.AbstractStructures;
using Algorithmis.DataStructures.Graphs;
using Algorithmis.Helpers;
using Algorithmis.Util;
using System;
namespace Algorithmis.TreeAlgorithms.ShortestPaths
{

    /// <summary>
    /// Find distances from all nodes to all others.
    /// Time: O(|V|^3);
    /// Space: O(|V|^2) for distances matrix.
    /// </summary>
    public static class FloydWarshall
    {
        private static int ChooseMinimalPath(int fst, int sndHead, int sndTail)
        { 
            /* this check is necessary because we represent +INFTY as int.MaxValue. Consequently trying to add something to infty results not in infty, but in overflow.*/
            if (sndHead == int.MaxValue || sndTail == int.MaxValue || sndHead + sndTail < 0)
                return fst;
            else
                return Math.Min(fst, sndHead + sndTail);
        }

        /// <summary>
        /// Calculate distances from each node to each node
        /// </summary>
        /// <param name="graph">Input graph</param>
        /// <returns>Matrix containing distances from i-th node to j-th node</returns>
        public static Matrix<int, IntegerRing> FindDistances(IGraph<int, int> graph)
        {
            var distances = new Matrix<int, IntegerRing>(graph.NodesCount, graph.NodesCount, (i, j) => int.MaxValue);

            //fill adjacency matrix with 0s and edge marks
            foreach (var node in graph)
                distances[node.Index, node.Index] = 0;
            foreach (var edge in graph.Edges)
                distances[edge.From.Index, edge.To.Index] = edge.Mark;


            foreach (var i in 0.to(graph.NodesCount))
                foreach (var j in 0.to(graph.NodesCount))
                    foreach (var k in 0.to(graph.NodesCount))
                        distances[i, j] = ChooseMinimalPath(
                            fst: distances[i, j],
                            sndHead: distances[i, k],
                            sndTail: distances[k, j]
                            );

            return distances;
        }
    }
}
