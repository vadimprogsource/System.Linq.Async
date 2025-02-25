using System;
using System.Collections;

namespace System.Linq.Async.Methods
{
    public class OfType<TSource,TResult> : IAsyncEnumerable<TResult>
    {
        private readonly IAsyncEnumerable<TSource> _sources;

        public OfType(IAsyncEnumerable<TSource> sources)
        {
            _sources = sources;
        }

        public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {

            return new d_enumerator(_sources.GetAsyncEnumerator(cancellationToken));
        }


        readonly struct d_enumerator : IAsyncEnumerator<TResult>
        {

            public d_enumerator(IAsyncEnumerator<TSource> sources) => _sources = sources;

            private readonly IAsyncEnumerator<TSource> _sources;

            public TResult Current => _sources.Current is TResult res ? res : throw new NotSupportedException();

            public ValueTask<bool> MoveNextAsync() => _sources.MoveNextAsync();
            public ValueTask DisposeAsync() => _sources.DisposeAsync();
            
        }

    }
}

