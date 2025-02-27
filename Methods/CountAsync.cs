using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class CountAsync<TSource>(
    IAsyncEnumerable<TSource> sources,
    Func<TSource, bool> predicate,
    CancellationToken cancellationToken = default)
    : AsyncEnumerableExecutor<TSource>(sources, cancellationToken)
{
    private long _total = 0;

    public CountAsync(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) :this(sources,x=>true,cancellationToken)
    { }

    protected override bool Do(TSource current)
    {
        if(predicate(current)) ++_total;
        return true;
    }

    public new async Task<long> ExecuteAsync()
    {
        _total = 0;
        await base.ExecuteAsync();
        return _total;
    }
}

