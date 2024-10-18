using System;

namespace System.Linq.Async.Enums
{
    public  class AsyncEnumeratorProxy<TSource> : IAsyncEnumerator<TSource>
    {
        private readonly IAsyncEnumerator<TSource> async_enumerator;

        public AsyncEnumeratorProxy(IAsyncEnumerator<TSource> enumerator)
        {
            async_enumerator = enumerator;
        }

        public virtual TSource Current => async_enumerator.Current;

        public virtual ValueTask DisposeAsync() => async_enumerator.DisposeAsync();

        public virtual ValueTask<bool> MoveNextAsync() => async_enumerator.MoveNextAsync();
        
    }




    public abstract class AsyncEnumeratorProxy<TSource,TResult> : IAsyncEnumerator<TResult>
    {
        private readonly IAsyncEnumerator<TSource> async_enumerator;

        public AsyncEnumeratorProxy(IAsyncEnumerator<TSource> enumerator)
        {
            async_enumerator = enumerator;
        }

        protected abstract TResult Convert(TSource source);


        public virtual TResult Current => Convert(async_enumerator.Current);

        public virtual ValueTask DisposeAsync() => async_enumerator.DisposeAsync();

        public virtual ValueTask<bool> MoveNextAsync() => async_enumerator.MoveNextAsync();

    }


}

