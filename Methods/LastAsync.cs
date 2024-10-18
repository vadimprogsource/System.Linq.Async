using System;

namespace System.Linq.Async.Methods
{
    public class LastAsync<TSource> : LastOrDefaultAsync<TSource>
    {
        public LastAsync(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate, CancellationToken cancellationToken = default) : base(sources, predicate, cancellationToken)
        {
        }


        public new async Task<TSource> ExecuteAsync()
        {
            await base.ExecuteAsync();
            return Instance ?? throw new NullReferenceException();
        }
    }
}

