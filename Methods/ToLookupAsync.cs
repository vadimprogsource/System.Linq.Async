using System;

namespace System.Linq.Async.Methods
{
    public class ToLookupAsync<TKey, TSource>(
        IAsyncEnumerable<TSource> sources,
        Func<TSource, TKey> keySelector,
        CancellationToken cancellationToken = default)
        : ToListAsync<TSource>(sources, cancellationToken)
    {
        public new async Task<ILookup<TKey, TSource>> ExecuteAsync() => (await base.ExecuteAsync()).ToLookup(keySelector); 
    }
}

