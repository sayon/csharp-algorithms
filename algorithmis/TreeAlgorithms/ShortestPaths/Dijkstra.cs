using Algorithmis.DataStructures.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.TreeAlgorithms.ShortestPaths
{
    /*
     * Find shortest distances from one vertice to all others
     */
    public static class Dijkstra
    {
        private static void TryLowerDistance(Edge<int, int> edge)
        {
            edge.To.Mark = Math.Min(edge.To.Mark, edge.From.Mark + edge.Mark);
        }
        private static Node<int> NodeWithMininalMark(IList<Node<int>> nodes)
        { 
            var minnode = nodes.First();
            foreach (var node in nodes) 
                if (minnode.Mark > node.Mark)
                    minnode = node; 
            return minnode;
        }

        public static void FindDistances(IGraph<int,int> graph, int from)
        {
            graph.MarkAllNodes(int.MaxValue);
            graph.MarkNode(from, 0);
            var priorityQueue = graph.ToList();


            while (priorityQueue.Count > 0)
            {
                var closestNode = NodeWithMininalMark(priorityQueue);

                priorityQueue.Remove(closestNode);
                foreach (var edge in graph.AdjacentEdges(closestNode.Index))
                    TryLowerDistance(edge);        
            }
        }
    }
}
