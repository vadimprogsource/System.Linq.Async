using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Filters;

public class AsyncEnumerableFilter<TSource>(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate)
    : AsyncEnumerableProxy<TSource>(sources)
{
    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new Enumerator(enumerator, predicate);
    


    private class Enumerator(IAsyncEnumerator<TSource> enumerator, Func<TSource, bool> predicate)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {
        public override async ValueTask<bool> MoveNextAsync()
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

