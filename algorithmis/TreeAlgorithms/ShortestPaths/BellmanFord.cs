using Algorithmis.DataStructures;
using Algorithmis.DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.TreeAlgorithms.ShortestPaths
{
    /// <summary>
    /// Find distances from one node to all others. Allows negative cycles.
    /// Time: O(|V|*|E|);
    /// Space: O(|V|) for distances array;
    /// </summary>
    public static class BellmanFord
    {
        private static void TryConsiderEdge(Edge<int,int> edge)
        {
            edge.To.Mark = Math.Min(edge.To.Mark, edge.From.Mark + edge.Mark);
        }
        public static IList<int> FindDistances(IGraph<int, int> graph, int from)
        { 
            graph.MarkAllNodes(int.MaxValue);
            graph.MarkNode(from, 0);

            for (int i = 0; i < graph.NodesCount - 2; i++)
                foreach (var edge in graph.Edges)
                    TryConsiderEdge(edge);
             

            return graph.Select((node)=>node.Mark).ToList();
        }
    }
}
