using System;

namespace System.Linq.Async.Methods
{
    public class ElementAtAsync<TSource>(
        IAsyncEnumerable<TSource> sources,
        int index,
        CancellationToken cancellationToken = default)
        : ElementAtOrDefaultAsync<TSource>(sources, index, cancellationToken)
    {
        public new async Task<TSource> ExecuteAsync()
        {
            await base.ExecuteAsync();
            return Instance ?? throw new NullReferenceException();
        }
    }
}

