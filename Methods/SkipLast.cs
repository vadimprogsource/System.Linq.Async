using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class SkipLast<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly int skipped;

        public SkipLast(IAsyncEnumerable<TSource> sources,int skipped) : base(sources)
        {
            this.skipped = skipped;
        }

        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, skipped);
       

        private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
        {
            private readonly int skipped;
            private readonly Queue<TSource> queue = new();
            private TSource? current = default;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator,int skipped) : base(enumerator)
            {
                this.skipped = skipped;
            }

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

