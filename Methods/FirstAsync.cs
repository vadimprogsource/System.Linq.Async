using System;

namespace System.Linq.Async.Methods
{
    public class FirstAsync<TSource>(
        IAsyncEnumerable<TSource> sources,
        Func<TSource, bool> predicate,
        CancellationToken cancellationToken = default)
        : FirstOrDefaultAsync<TSource>(sources, predicate, cancellationToken)
    {
        public new async Task<TSource> ExecuteAsync()
        {
            await base.ExecuteAsync();
            return Instance ?? throw new NullReferenceException();
        }
    }
}

