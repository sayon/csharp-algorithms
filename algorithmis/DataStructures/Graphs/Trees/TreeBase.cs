using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithmis.DataStructures.Graphs.Trees
{
    public abstract class TreeBase<T, U> : ITreeParent<T, U>, IDotSerializeable
        where U : TreeBase<T, U>
    {
        public TreeBase(U parent) { Parent = parent; }
        public T Value
        {
            get;
            set;
        }
        public int Index { get; set; }
        public abstract IEnumerable<U> Children { get; set; }

        public U Parent { get; set; }


        #region Dotty repr

        public virtual String DotName
        {
            get
            {
                return string.Format("\"({0}:{1})\"", Index, Value);
            }
        }
        public virtual String DotNodes
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Children.Count() == 0) sb.AppendLine(DotName);
                else
                    foreach (var child in Children)
                        if (child != null)
                        {
                            sb.AppendLine(DotName + " ->" + child.DotName);
                            sb.AppendLine(child.DotNodes);
                        }
                return sb.ToString();
            }
        }
        #endregion

        public override string ToString()
        {
            return DotName;
        }
        public bool IsDirectional { get { return true; } }

        public string DotFullRepr
        {
            get { return this.Envelope(); }
        }
    }
}
