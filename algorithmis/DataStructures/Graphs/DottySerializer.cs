using System;
using System.Diagnostics;
using System.IO;
namespace Algorithmis.DataStructures.Graphs
{
    public static class DotSerializer
    {

        public static readonly string EXTENSION = ".txt"; 
        public static void Show(IDotSerializeable graph)
        {
            var name = _generateFilename(graph);
            File.WriteAllText(name + EXTENSION, graph.DotFullRepr);
            Process.Start(Config.DOTTYPATH, name + EXTENSION);
        }
        private static string _generateFilename(object  o)
        {
            return DateTime.Now.Day.ToString()
                + DateTime.Now.Hour.ToString()
                + DateTime.Now.Minute.ToString()
                + DateTime.Now.Second.ToString()
                + new Random().Next(1000).ToString()
                + Math.Abs(o.GetHashCode()).ToString();
        } 
    }
}
