using System;

namespace System.Linq.Async.Methods
{
    public class SingleAsync<TSource> : SingleOrDefaultAsync<TSource>
    {
        public SingleAsync(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate, CancellationToken cancellationToken = default) : base(sources, predicate, cancellationToken)
        {
        }

        public new async Task<TSource> ExecuteAsync()
        {
            await base.ExecuteAsync();
            return Instance ?? throw new NullReferenceException();
        }
    }
}

