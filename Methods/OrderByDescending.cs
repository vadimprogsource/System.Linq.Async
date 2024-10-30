using System;
using System.Linq.Async.Providers;

namespace System.Linq.Async.Methods;


public class OrderByDescending<TSource, TKey> : OrderedProvider<TSource>
{
    public OrderByDescending(IAsyncEnumerable<TSource> sources, Func<TSource, TKey> keySelector) : base(sources)
    {
        CreateOrderByDescending(keySelector);
    }
}

