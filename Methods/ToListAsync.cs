using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class ToListAsync<TSource> : AsyncEnumerableExecutor<TSource>
{
    private readonly List<TSource> handler = new(); 


    public ToListAsync(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
    {
    }

    protected override bool Do(TSource current)
    {
        handler.Add(current);
        return true;
    }


    public new async Task<List<TSource>> ExecuteAsync()
    {
        await base.ExecuteAsync();
        return handler;
    }
}

