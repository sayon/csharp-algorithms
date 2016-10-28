using Algorithmis.DataStructures.Graphs;
using Algorithmis.Demos;
using Algorithmis.TreeAlgorithms.ShortestPaths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Demos
{
    class DijkstraDemo : IDemo
    { 
        public string run()
        {
            var g = new PlainGraph(6);
            g.AddEdge(0, 5, 14);
            g.AddEdge(0, 2, 9);
            g.AddEdge(0, 1, 7);
            g.AddEdge(1, 2, 10);
            g.AddEdge(2, 5, 2);
            g.AddEdge(1, 3, 15);
            g.AddEdge(2, 3, 11);
            g.AddEdge(3, 4, 6);
            g.AddEdge(2, 5, 2);
            g.AddEdge(4, 5, 9);

            Dijkstra.FindDistances(g, 0);
            DotSerializer.Show(g);
            return String.Empty;
        }
    }
}
