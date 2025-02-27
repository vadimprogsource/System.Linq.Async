using System;
namespace System.Linq.Async.Executors;

public class OrderByDescelingExecutor<TSource, TKey>(Func<TSource, TKey> keySelector)
    : OrderByExecutor<TSource, TKey>(keySelector)
{
    protected override IOrderedEnumerable<TSource> Execute(IEnumerable<TSource> sources) => sources.OrderByDescending(KeySelector);
    protected override IOrderedEnumerable<TSource> Execute(IOrderedEnumerable<TSource> sources) => sources.ThenByDescending(KeySelector);
    
}

