using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class Skip<TSource>(IAsyncEnumerable<TSource> sources, int skipped) : AsyncEnumerableProxy<TSource>(sources)
{
    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, skipped);
    

    private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, int skipped)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {

        private  int skipped = skipped;


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

