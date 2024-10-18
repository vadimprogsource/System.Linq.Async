using System;

namespace System.Linq.Async.Executors
{
    public class OrderByExecutor<TSource,TKey>: OrderExecutor<TSource>
    {
        protected readonly Func<TSource, TKey> KeySelector;

        public OrderByExecutor(Func<TSource, TKey> keySelector)
        {
            KeySelector = keySelector;
        }

        public override IOrderedEnumerable<TSource> Execute(IEnumerable<TSource> sources) => sources.OrderBy(KeySelector);

        public override IOrderedEnumerable<TSource> Execute(IOrderedEnumerable<TSource> sources) => sources.ThenBy(KeySelector);
        
    }

    
}

