using System;
using System.Linq.Async.Executors;
using System.Linq.Expressions;

namespace System.Linq.Async.Methods
{
    public class FirstOrDefaultAsync<TSource> : AsyncEnumerableExecutor<TSource>
    {
        protected readonly Func<TSource, bool> Predicate;
        protected TSource? Instance = default; 

        public FirstOrDefaultAsync(IAsyncEnumerable<TSource> sources,Func<TSource,bool> predicate, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
        {
            Predicate = predicate;
        }

        protected override bool Do(TSource current)
        {
            if (Predicate(current))
            {
                Instance = current;
                return false;
            }
            return true;
        }

        public new async Task<TSource?> ExecuteAsync()
        {
            Instance = default;
            await base.ExecuteAsync();
            return Instance;
        }
    }
}

