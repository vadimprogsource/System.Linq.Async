using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class GroupBy<TSource, TKey>(IAsyncEnumerable<TSource> sources, Func<TSource, TKey> selector)
    : IAsyncEnumerable<IGrouping<TKey, TSource>>
{
    public IAsyncEnumerator<IGrouping<TKey, TSource>> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken), selector);
    

    private class AsyncEnumerator(IAsyncEnumerator<TSource> source, Func<TSource, TKey> selector)
        : AsyncEnumeratorExecutor<TSource, IGrouping<TKey, TSource>>(source)
    {
        protected override IEnumerator<IGrouping<TKey, TSource>> Execute(IEnumerable<TSource> sources) => sources.GroupBy(selector).GetEnumerator();
        
    }
}

