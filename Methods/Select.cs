using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class Select<TSource, TResult>(IAsyncEnumerable<TSource> sources, Func<TSource, TResult> selector)
        : IAsyncEnumerable<TResult>
    {
        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken), selector);
        


        private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, TResult> selector)
            : IAsyncEnumerator<TResult>
        {
            public TResult Current => selector(enumerator.Current);

            public ValueTask DisposeAsync() => enumerator.DisposeAsync();


            public ValueTask<bool> MoveNextAsync() => enumerator.MoveNextAsync();
            
        }

    }
}

