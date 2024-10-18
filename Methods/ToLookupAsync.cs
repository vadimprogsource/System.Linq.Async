using System;

namespace System.Linq.Async.Methods
{
    public class ToLookupAsync<TKey,TSource> : ToListAsync<TSource>
    {
        private readonly Func<TSource, TKey> keySelector;

        public ToLookupAsync(IAsyncEnumerable<TSource> sources,Func<TSource,TKey> keySelector, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
        {
            this.keySelector = keySelector;
        }

        public new async Task<ILookup<TKey, TSource>> ExecuteAsync() => (await base.ExecuteAsync()).ToLookup(keySelector); 
    }
}

