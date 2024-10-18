using System;
using System.Collections.Generic;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class TakeLast<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly int taken;

        public TakeLast(IAsyncEnumerable<TSource> sources,int taken) : base(sources)
        {
            this.taken = taken;
        }

        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, taken);
       
        private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
        {
            private readonly int taken;
            private Queue<TSource>? queue;
            private TSource? current;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator,int taken) : base(enumerator)
            {
                this.taken = taken;
            }

            public override TSource Current => current ?? throw new NullReferenceException();

            public override async ValueTask<bool> MoveNextAsync()
            {
                if (queue == null)
                {
                    queue = new();

                    try
                    {
                        while (await base.MoveNextAsync())
                        {
                            if (queue.Count >= taken)
                            {
                                queue.Dequeue();
                            }

                            queue.Enqueue(base.Current);
                        }
                    }
                    finally
                    {
                        await base.DisposeAsync();
                    }
                }

                if (queue.Count > 0)
                {
                    current = queue.Dequeue();
                    return true;
                }

                return false;
            }
        }
    }
}

