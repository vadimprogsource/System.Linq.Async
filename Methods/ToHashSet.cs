using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class ToHashSet<TSource>(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default)
    : AsyncEnumerableExecutor<TSource>(sources, cancellationToken)
{

    private readonly HashSet<TSource> _handler=new();

    protected override bool Do(TSource current)
    {
        _handler.Add(current);
        return true;
    }

    public new async Task<HashSet<TSource>> ExecuteAsync()
    {
        await base.ExecuteAsync();
        return _handler;
    }
}

