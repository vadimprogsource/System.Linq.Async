using System;
namespace System.Linq.Async.Executors
{
    public class OrderByDescelingExecutor<TSource, TKey> : OrderByExecutor<TSource, TKey>
    {
        public OrderByDescelingExecutor(Func<TSource, TKey> keySelector) : base(keySelector)
        {
        }

        public override IOrderedEnumerable<TSource> Execute(IEnumerable<TSource> sources) => sources.OrderByDescending(KeySelector);
        public override IOrderedEnumerable<TSource> Execute(IOrderedEnumerable<TSource> sources) => sources.ThenByDescending(KeySelector);
        
    }
}

