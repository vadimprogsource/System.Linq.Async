using System;
using System.Linq.Async.Providers;

namespace System.Linq.Async.Methods;

public class OrderBy<TSource,TKey> : OrderedProvider<TSource>
{
    public OrderBy(IAsyncEnumerable<TSource> sources , Func<TSource,TKey> keySelector) : base(sources)
    {
        CreateOrderBy(keySelector);
    }
}

