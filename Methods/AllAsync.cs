using System;

namespace System.Linq.Async.Methods
{
    public class AllAsync<TSource>(
        IAsyncEnumerable<TSource> sources,
        Func<TSource, bool> predicate,
        CancellationToken cancellationToken = default)
        : AnyAsync<TSource>(sources, predicate, cancellationToken)
    {
        protected override bool Do(TSource current) => HasComplete = Predicate(current);

    }
}

