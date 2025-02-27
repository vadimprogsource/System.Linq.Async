using System;

namespace System.Linq.Async.Methods
{
    public class ElementAtOrDefaultAsync<TSource>(
        IAsyncEnumerable<TSource> sources,
        int index,
        CancellationToken cancellationToken = default)
        : LastOrDefaultAsync<TSource>(sources, x => true, cancellationToken)
    {
        private  int _index = index;

        protected override bool Do(TSource current)
        {
            base.Do(current);
            return _index-- > 0;
        }


    }
}

