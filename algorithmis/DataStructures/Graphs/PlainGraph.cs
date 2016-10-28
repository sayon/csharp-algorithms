using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs
{
    public class PlainGraph : GraphAdjacentListsBase<int, int>
    {
        public const int DEF_EDGE_MARK = 0;
        public PlainGraph(int count) : base(count) { }

        public override bool IsDirectional
        {
            get
            {
                return false;
            }
        }
        public override void AddEdge(int from, int to, int mark = DEF_EDGE_MARK)
        {
            base.AddEdge(from, to, mark);
            base.AddEdge(to, from, mark);
        }
       

        public override string DotNodes
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var node in this)
                    sb.AppendLine("\"" + node + "\"");
                foreach (var edge in Edges)
                    if (edge.From.Index < edge.To.Index)
                        sb.AppendLine(String.Format("\"{0}\" -- \"{1}\" [label=\"{2}\"];", edge.From, edge.To, edge.Mark));
                return sb.ToString();
            }
        }

    }
}
