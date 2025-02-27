using System;

namespace System.Linq.Async.Methods
{
    public class ToArrayAsync<TSource>(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default)
        : ToListAsync<TSource>(sources, cancellationToken)
    {
        public new  async Task<TSource[]> ExecuteAsync() => (await base.ExecuteAsync()).ToArray();
       
    }
}

