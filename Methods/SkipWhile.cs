using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class SkipWhile<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly Func<TSource, bool> predicate;

        public SkipWhile(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate) : base(sources)
        {
            this.predicate = predicate;
        }

        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, predicate); 
    

        private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
        {
            private readonly Func<TSource, bool> predicate;
            private bool skipping = true;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, bool> predicate) : base(enumerator)
            {
                this.predicate = predicate;
            }

            public async override ValueTask<bool> MoveNextAsync()
            {

                while (skipping)
                {
                    if (await base.MoveNextAsync())
                    {
                        if (!(skipping = predicate(base.Current))) return true;
                        continue;
                    }

                    return false;
                }

                return await base.MoveNextAsync();

           
            }
        }
    }
}

