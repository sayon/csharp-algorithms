using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithmis.DataStructures.Graphs.Trees
{
    public interface ITree<T, TTree>
         where TTree : ITree<T, TTree>
    {
        IEnumerable<TTree> Children { get; set; }
        int Index { get; set; }
        T Value { get; set; }
    }

    public interface ITreeParent<T, TTree> : ITree<T, TTree>
         where TTree : ITree<T, TTree>
    {
        TTree Parent { get; set; }
    }

    public static class ITreeExtensions
    {
        public static TTree Actual<T, TTree>(this ITree<T, TTree> source)
            where TTree : ITree<T, TTree>
        { 
            return (TTree)source;
        }

        public static ITree<TResult, TResultTree> Project<TSource, TSourceTree, TResult, TResultTree>(this ITree<TSource, TSourceTree> tree, Func<ITree<TSource, TSourceTree>, ITree<TResult, TResultTree>> transform)
            where TSourceTree : ITree<TSource, TSourceTree>
            where TResultTree : ITree<TResult, TResultTree>
        {
            ITree<TResult, TResultTree> newtree = transform(tree);
            newtree.Children = tree.Children.Select(
                (node) => node.Project(transform).Actual()
                   );
            return newtree;
         }


        //FIXME here!
        public static TTree Index<T, TTree>(this ITree<T, TTree> source)
            where TTree : ITree<T, TTree>, new()
        {
            int i = 0;
            return source.ForAll((node) => node.Index = i++ );
        }

        /// <summary>
        /// Going through all edges (Euler path)
        /// </summary>
        /// <typeparam name="T">Node content type</typeparam>
        /// <typeparam name="TTree">Tree type</typeparam> 
        /// <returns>Enumerates nodes along Euler path</returns>
        public static IEnumerable<ITree<T, TTree>> EulerEnumerateNodes<T, TTree>(this ITree<T, TTree> tree) where TTree : ITree<T, TTree>
        {
            yield return tree;
            foreach (var child in tree.Children)
            {
                foreach (var node in child.EulerEnumerateNodes())
                    yield return node;
                yield return tree;
            }
        }

        public static U ForAll<T, U>(this ITree<T,U> tree, Action<U> act) where U : ITree<T, U>
        {
            act(tree.Actual());
            foreach (var child in tree.Children)
                child.ForAll(act);
            return tree.Actual();
        }

        public static IEnumerable<T> Values<T, U>(this IEnumerable<ITree<T, U>> source) where U : ITree<T, U>
        {
            return source.Select((node) => node.Value);
        }
        public static int Count<T, TTree>(this ITree<T, TTree> tree, int acc = 0) where TTree : ITree<T, TTree>
        { 
            foreach (var child in tree.Children)
                acc = child.Count(acc);
            return acc+1;
        }

        public static void DFS<T, TTree>(ITree<T, TTree> tree, Action<ITree<T, TTree>> act) where TTree : ITree<T, TTree>
        {
            if (tree != null)
            {
                act(tree);
                foreach (var child in tree.Children)
                    DFS(child, act);
            }
        }

        public static void BFS<V, E>(IGraph<V, E> graph, Action<Node<V>> act, int idxStart)
        {
            var startNode = graph[idxStart];
            var queue = new Queue<Node<V>>();
            queue.Enqueue(startNode);
            var visited = new bool[graph.NodesCount];

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                act(currentNode);
                visited[currentNode.Index] = true;
                foreach (var node in graph.Adjacents(currentNode))
                    if (!visited[node.Index])
                        queue.Enqueue(node);
            }
        }
        public static IEnumerable<ITree<V, T>> BFS<V, T>(ITree<V, T> tree, Action<ITree<V, T>> act) where T : ITree<V, T>
        {
            var queue = new Queue<ITree<V, T>>();
            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                foreach (var node in currentNode.Children)
                    queue.Enqueue(node);
                yield return currentNode;
            }
        }


    }

}
