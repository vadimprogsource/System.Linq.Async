using System;

namespace System.Linq.Async.Methods
{
    public class ElementAtOrDefaultAsync<TSource> : LastOrDefaultAsync<TSource>
    {
        private  int index;

        public ElementAtOrDefaultAsync(IAsyncEnumerable<TSource> sources, int index, CancellationToken cancellationToken = default) : base(sources, x=>true, cancellationToken)
        {
            this.index = index;
        }

        protected override bool Do(TSource current)
        {
            base.Do(current);
            return index-- > 0;
        }


    }
}

