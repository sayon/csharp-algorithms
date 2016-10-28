using Algorithmis.DataStructures.Graphs;
using Algorithmis.DataStructures.Graphs.Trees;
using Algorithmis.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.Demos
{
    class FenwickTreeDemo : IDemo
    {
        public string run()
        {
            var testArray = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var tree = new FenwickTree<int>(testArray, Operations.BinPlusInt, Operations.BinMinusInt, 0);
            DotSerializer.Show(tree);
            return String.Empty;
        }
    }
}
