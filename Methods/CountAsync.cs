using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods
{
    public class CountAsync<TSource> : AsyncEnumerableExecutor<TSource>
    {
        private readonly Func<TSource, bool> predicate;
        private long total;

        public CountAsync(IAsyncEnumerable<TSource> sources,Func<TSource,bool> predicate ,  CancellationToken cancellationToken = default) : base(sources, cancellationToken)
        {
            this.predicate = predicate;
            total = 0;
        }

        public CountAsync(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) :this(sources,x=>true,cancellationToken)
        { }

        protected override bool Do(TSource current)
        {
            if(predicate(current)) ++total;
            return true;
        }

        public new async Task<long> ExecuteAsync()
        {
            total = 0;
            await base.ExecuteAsync();
            return total;
        }
    }
}

