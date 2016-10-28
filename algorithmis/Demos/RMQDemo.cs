using Algorithmis.DataStructures.Graphs;
using Algorithmis.DataStructures.Graphs.Trees;
using Algorithmis.TreeAlgorithms.RMQnLCA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Demos
{
    class RMQDemo : IDemo
    {
        public string run()
        {
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var g = new RangeMaxQueryTree<int>(arr);
            DotSerializer.Show(g);
            return string.Empty;
        }
    }
}
