using System;

namespace System.Linq.Async.Methods
{
    public class LastOrDefaultAsync<TSource>(
        IAsyncEnumerable<TSource> sources,
        Func<TSource, bool> predicate,
        CancellationToken cancellationToken = default)
        : FirstOrDefaultAsync<TSource>(sources, predicate, cancellationToken)
    {
        protected override bool Do(TSource current)
        {
            if (Predicate(current)) Instance = current;
            return true;
        }
    }
}

