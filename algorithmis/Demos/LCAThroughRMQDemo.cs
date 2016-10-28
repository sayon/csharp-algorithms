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
    class LCAThroughRMQDemo : IDemo
    {
        public string run()
        {
            var sb = new StringBuilder();
            var tree = new BinaryTree<int>(0,
                 new BinaryTree<int>(0,
                    new BinaryTree<int>(0),
                    new BinaryTree<int>(0)),
                 new BinaryTree<int>(0))
                 .ForAll((node) => node.RightIndex());

            DotSerializer.Show(tree);
            var solver = new LCAThroughRMQTaskSolver<BinaryTree<int>>(tree);
            sb.AppendLine(solver.AskIndex(2, 4).ToString());

            return sb.ToString();
        }
    }
}
