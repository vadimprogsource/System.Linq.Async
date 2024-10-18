using System;

namespace System.Linq.Async.Enums
{
    public abstract class AsyncEnumerableProxy<TSource>: IAsyncEnumerable<TSource>
    {
        private readonly IAsyncEnumerable<TSource> sources;


        public AsyncEnumerableProxy(IAsyncEnumerable<TSource> sources)
        {
            this.sources = sources;
        }

        public IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default) => CreateAsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken));

        public abstract IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator);

    }


    public abstract class AsyncEnumerableProxy<TSource, TResult> : IAsyncEnumerable<TResult>
    {
        private readonly IAsyncEnumerable<TSource> sources;


        public AsyncEnumerableProxy(IAsyncEnumerable<TSource> sources)
        {
            this.sources = sources;
        }

        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default) => CreateAsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken));
     

        public abstract IAsyncEnumerator<TResult> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator);
    }
}

