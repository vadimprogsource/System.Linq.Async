using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class SkipLast<TSource>(IAsyncEnumerable<TSource> sources, int skipped)
        : AsyncEnumerableProxy<TSource>(sources)
    {
        protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, skipped);
       

        private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, int skipped)
            : AsyncEnumeratorProxy<TSource>(enumerator)
        {
            private readonly Queue<TSource> queue = new();
            private TSource? current = default;

            public override TSource Current=> current?? throw new NullReferenceException();
                

            public override async ValueTask<bool> MoveNextAsync()
            {

                while (await base.MoveNextAsync())
                {
                    queue.Enqueue(base.Current);

                    if (queue.Count > skipped)
                    {
                        current = queue.Dequeue();
                        return true;
                    }
                }

                return false;
            }
        }

    }
}

