using System;

namespace System.Linq.Async.Executors
{
    public class OrderByExecutor<TSource, TKey>(Func<TSource, TKey> keySelector) : OrderExecutor<TSource>
    {
        protected readonly Func<TSource, TKey> KeySelector = keySelector;

        protected override IOrderedEnumerable<TSource> Execute(IEnumerable<TSource> sources) => sources.OrderBy(KeySelector);

        protected override IOrderedEnumerable<TSource> Execute(IOrderedEnumerable<TSource> sources) => sources.ThenBy(KeySelector);
        
    }

    
}

