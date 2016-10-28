using System; 
namespace Algorithmis.TreeAlgorithms.RMQnLCA
{
    interface IRMQTaskSolver<T>
     where T : IComparable<T>
    {
        T Ask(int from, int to);
    }
}
