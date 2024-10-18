using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Filters
{
    public class AsyncEnumerableFilter<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly Func<TSource, bool> predicate;

        public AsyncEnumerableFilter(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate) : base(sources)
        {
            this.predicate = predicate;
        }

        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new Enumerator(enumerator, predicate);
        


        private class Enumerator : AsyncEnumeratorProxy<TSource>
        {
            private readonly Func<TSource, bool> predicate;

            public Enumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, bool> predicate) : base(enumerator)
            {
                this.predicate = predicate;
            }

            public async override ValueTask<bool> MoveNextAsync()
            {
                while (await base.MoveNextAsync())
                {
                    if (predicate(Current))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}

