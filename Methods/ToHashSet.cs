using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class ToHashSet<TSource> : AsyncEnumerableExecutor<TSource>
{

    private readonly HashSet<TSource> handler=new();

    public ToHashSet(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
    {
        
    }

    protected override bool Do(TSource current)
    {
        handler.Add(current);
        return true;
    }

    public new async Task<HashSet<TSource>> ExecuteAsync()
    {
        await base.ExecuteAsync();
        return handler;
    }
}

