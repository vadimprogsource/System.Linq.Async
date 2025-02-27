using System;
namespace System.Linq.Async.Executors;

public abstract class AsyncEnumerableExecutor<TSource>(
    IAsyncEnumerable<TSource> sources,
    CancellationToken cancellationToken = default)
{
    private readonly IAsyncEnumerator<TSource> _handler = sources.GetAsyncEnumerator(cancellationToken);


    public async Task ExecuteAsync()
    {
        try
        {
            while (await _handler.MoveNextAsync())
            {
                if (Do(_handler.Current)) continue;
                break;
            }
                
        }
        finally
        {
            await _handler.DisposeAsync();
        }
    }



    protected abstract bool Do(TSource current);



}

