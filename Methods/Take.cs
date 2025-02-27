using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class Take<TSource>(IAsyncEnumerable<TSource> sources, int taken) : AsyncEnumerableProxy<TSource>(sources)
{
    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, taken);
    

    private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, int taken)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {
        private int _taken = taken;

        public override async ValueTask<bool> MoveNextAsync()
        {
            if (_taken > 0)
            {
                --_taken;
                return await base.MoveNextAsync();
            }

            return false;
        }
    }
}

