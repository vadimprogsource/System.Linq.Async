using System;

namespace System.Linq.Async.Methods
{
    public class ToArrayAsync<TSource> : ToListAsync<TSource>
    {
        public ToArrayAsync(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
        {
        }

        public new  async Task<TSource[]> ExecuteAsync() => (await base.ExecuteAsync()).ToArray();
       
    }
}

