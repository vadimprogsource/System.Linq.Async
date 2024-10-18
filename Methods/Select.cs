using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class Select<TSource,TResult> : IAsyncEnumerable<TResult>
    {
        private readonly IAsyncEnumerable<TSource>   sources;
        private readonly Func<TSource, TResult> selector;


        public Select(IAsyncEnumerable<TSource> sources , Func<TSource,TResult> selector)
        {
            this.sources  = sources;
            this.selector = selector;
        }

        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new AsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken), selector);
        


        private class AsyncEnumerator : IAsyncEnumerator<TResult>
        {
            private readonly IAsyncEnumerator<TSource> enumerator;
            private readonly Func<TSource, TResult> selector;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, TResult> selector) 
            {
                this.enumerator = enumerator;
                this.selector = selector;
            }

            public TResult Current => selector(enumerator.Current);

            public ValueTask DisposeAsync() => enumerator.DisposeAsync();


            public ValueTask<bool> MoveNextAsync() => enumerator.MoveNextAsync();
            
        }

    }
}

