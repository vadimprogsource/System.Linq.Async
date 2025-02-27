using System.Linq.Async.Providers;

namespace System.Linq.Async.Methods;


public class OrderByDesceling<TSource, TKey> : OrderedProvider<TSource>
{
    public OrderByDesceling(IAsyncEnumerable<TSource> sources, Func<TSource, TKey> keySelector) : base(sources)
    {
        CreateOrderByDesceling(keySelector);
    }
}

