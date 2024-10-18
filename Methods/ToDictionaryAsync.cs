using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods
{
    public class ToDictionaryAsync<TKey, TValue,TSource> : AsyncEnumerableExecutor<TSource> where TKey:notnull
    {
        private readonly Func<TSource, TKey> keySelector;
        private readonly Func<TSource, TValue> valueSelector;
        private readonly Dictionary<TKey, TValue> handler = new();

        public ToDictionaryAsync(IAsyncEnumerable<TSource> sources, Func<TSource,TKey> keySelector , Func<TSource,TValue> valueSelector, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
        {
            this.keySelector = keySelector;
            this.valueSelector = valueSelector;
        }

        protected override bool Do(TSource current)
        {
            handler.Add(keySelector(current), valueSelector(current));
            return true;
        }

        public new async Task<IDictionary<TKey, TValue>> ExecuteAsync()
        {
            await base.ExecuteAsync();
            return handler;
        }
    }
}

