using System;

namespace System.Linq.Async.Enums
{
    public abstract class AsyncEnumerableProxy<TSource>(IAsyncEnumerable<TSource> sources) : IAsyncEnumerable<TSource>
    {
        public IAsyncEnumerator<TSource> GetAsyncEnumerator(CancellationToken cancellationToken = default) => CreateAsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken));

        protected abstract IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator);

    }


    public abstract class AsyncEnumerableProxy<TSource, TResult>(IAsyncEnumerable<TSource> sources)
        : IAsyncEnumerable<TResult>
    {
        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default) => CreateAsyncEnumerator(sources.GetAsyncEnumerator(cancellationToken));


        protected abstract IAsyncEnumerator<TResult> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator);
    }
}

