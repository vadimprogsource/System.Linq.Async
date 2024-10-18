using System;
namespace System.Linq.Async.Executors
{
    public abstract class OrderExecutor<TSource>
    {

        public static IOrderedEnumerable<TSource> ExecuteWith(IEnumerable<TSource> sources, IEnumerable<OrderExecutor<TSource>> executors)
        {
            IOrderedEnumerable<TSource> ordered = executors.First().Execute(sources);

            foreach (OrderExecutor<TSource> executor in executors.Skip(1))
            {
                ordered = executor.Execute(ordered);
            }

            return ordered;
        }


        public abstract IOrderedEnumerable<TSource> Execute(IEnumerable<TSource> sources);
        public abstract IOrderedEnumerable<TSource> Execute(IOrderedEnumerable<TSource> sources);
    }
}

