using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class Skip<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly int skipped;

        public Skip(IAsyncEnumerable<TSource> sources,int skipped) : base(sources)
        {
            this.skipped = skipped;
        }

        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, skipped);
        

        private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
        {

            private  int skipped;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator,int skipped) : base(enumerator)
            {
                this.skipped = skipped;
            }


            public async override ValueTask<bool> MoveNextAsync()
            {
                while (skipped > 0)
                {
                    if (await base.MoveNextAsync())
                    {
                        --skipped;
                        continue;
                    }

                    return false;
                }
                return await base.MoveNextAsync();

            }

        }
    }
}

