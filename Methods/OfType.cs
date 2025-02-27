namespace System.Linq.Async.Methods;

public class OfType<TSource, TResult>(IAsyncEnumerable<TSource> sources) : IAsyncEnumerable<TResult>
{
    public IAsyncEnumerator<TResult> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {

        return new d_enumerator(sources.GetAsyncEnumerator(cancellationToken));
    }


    readonly struct d_enumerator(IAsyncEnumerator<TSource> sources) : IAsyncEnumerator<TResult>
    {
        public TResult Current => sources.Current is TResult res ? res : throw new NotSupportedException();

        public ValueTask<bool> MoveNextAsync() => sources.MoveNextAsync();
        public ValueTask DisposeAsync() => sources.DisposeAsync();
        
    }

}

