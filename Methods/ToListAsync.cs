using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class ToListAsync<TSource>(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default)
    : AsyncEnumerableExecutor<TSource>(sources, cancellationToken)
{
    private readonly List<TSource> _handler = new();


    protected override bool Do(TSource current)
    {
        _handler.Add(current);
        return true;
    }


    public new async Task<List<TSource>> ExecuteAsync()
    {
        await base.ExecuteAsync();
        return _handler;
    }
}

