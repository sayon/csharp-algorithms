using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs
{
    public  interface IDotSerializeable
    {
      
        String DotNodes { get; }
        String DotName { get; }
        String DotFullRepr { get; }
        bool IsDirectional { get; }

       
    }
    public static partial class Extensions
    {
        private static Random nameGenerator = new Random();
        private static string generateName() { return nameGenerator.Next().ToString(); }

        public static string Envelope(this IDotSerializeable s, string name = null)
        {
            var preface = ((s.IsDirectional) ? "digraph " : "graph ") + generateName() + " {\n";
            return preface + s.DotNodes + "\n}";
        }
    }
}
