using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class Take<TSource> : AsyncEnumerableProxy<TSource>
{
    private readonly int taken;

    public Take(IAsyncEnumerable<TSource> sources,int taken) : base(sources)
    {
        this.taken = taken;
    }

    public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, taken);
    

    private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
    {
        private int taken;

        public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator,int taken) : base(enumerator)
        {
            this.taken = taken;
        }

        public override async ValueTask<bool> MoveNextAsync()
        {
            if (taken > 0)
            {
                --taken;
                return await base.MoveNextAsync();
            }

            return false;
        }
    }
}

