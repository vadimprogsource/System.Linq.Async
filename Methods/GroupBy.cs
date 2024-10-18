using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class GroupBy<TSource,TKey> : IAsyncEnumerable<IGrouping<TKey,TSource>>
{
    private readonly IAsyncEnumerable<TSource> sources;
    private readonly Func<TSource, TKey> selector;

    public GroupBy(IAsyncEnumerable<TSource> sources,Func<TSource,TKey> selector)
    {
        this.sources = sources;
        this.selector = selector;
    }

    public IAsyncEnumerator<IGrouping<TKey, TSource>> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken), selector);
    

    private class AsyncEnumerator : AsyncEnumeratorExecutor<TSource, IGrouping<TKey, TSource>>
    {
        private readonly Func<TSource, TKey> selector;

        public AsyncEnumerator(IAsyncEnumerator<TSource> source, Func<TSource, TKey> selector) : base(source)
        {
            this.selector = selector;
        }

        protected override IEnumerator<IGrouping<TKey, TSource>> Execute(IEnumerable<TSource> sources) => sources.GroupBy(selector).GetEnumerator();
        
    }
}

