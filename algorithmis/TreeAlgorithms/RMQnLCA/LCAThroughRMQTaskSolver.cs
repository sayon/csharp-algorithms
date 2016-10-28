using Algorithmis.DataStructures.Graphs.Trees;
using Algorithmis.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithmis.TreeAlgorithms.RMQnLCA
{
    public class LCAThroughRMQTaskSolver<T> where T : ITree<int, T>, new()
    { 
        IList<int> _firstEncounters; 
         
        IList<ITree<int, T>> _eulerNodes;
        RangeMinQueryTree<int> _rmqSolver;


        public LCAThroughRMQTaskSolver(ITree<int, T> source)
        { 
            MarkAllNodesWithDepth(source);
            fillFirstEncounters(source);
            _rmqSolver = new RangeMinQueryTree<int>(_eulerNodes.Select((node) => node.Index).ToList());
        }
        
        public int AskIndex(int fst, int snd)
        {
            int fstInEuler = firstInEuler(fst);
            int sndInEuler = firstInEuler(snd);
            int leftBound = Math.Min(fstInEuler, sndInEuler);
            int rightBound = Math.Max(fstInEuler, sndInEuler);
            return _rmqSolver.Ask( leftBound, rightBound ); 
        }
        
        void MarkAllNodesWithDepth(ITree<int, T> source, int curDepth = 0)
        {
            source.Value = curDepth;
            foreach (ITree<int, T> node in source.Children)
                MarkAllNodesWithDepth(node, curDepth + 1);
        }

        private int firstInEuler(int treeIdx)
        {
            return _firstEncounters[treeIdx - 1];
        }
        private void fillFirstEncounters(ITree<int, T> source)
        {
            _firstEncounters = new List<int>().Fill(-1, source.Count());
            int i = 0;

            _eulerNodes = source.EulerEnumerateNodes().ToList();
            foreach (var node in _eulerNodes)
            {
                if (_firstEncounters[node.Index - 1] == -1) //indexes in the tree are not zero-based, but 1-based
                    _firstEncounters[node.Index - 1] = i;
                i++;
            }
        }

    }
}
