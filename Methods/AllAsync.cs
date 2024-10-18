using System;

namespace System.Linq.Async.Methods
{
    public class AllAsync<TSource> : AnyAsync<TSource>
    {
        public AllAsync(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate, CancellationToken cancellationToken = default) : base(sources, predicate, cancellationToken)
        {
        }

        protected override bool Do(TSource current)
        {
            return HasComplete = Predicate(current);
        }
    }
}

