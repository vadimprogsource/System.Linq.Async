using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class SkipWhile<TSource>(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate)
    : AsyncEnumerableProxy<TSource>(sources)
{
    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, predicate); 


    private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, bool> predicate)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {
        private bool skipping = true;

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

