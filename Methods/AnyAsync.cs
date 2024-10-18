using System;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;


public class AnyAsync<TSource> : AsyncEnumerableExecutor<TSource>
{
    protected readonly  Func<TSource,bool> Predicate;
    protected  bool HasComplete ;

    public AnyAsync(IAsyncEnumerable<TSource> sources, Func<TSource,bool> predicate, CancellationToken cancellationToken = default) : base(sources, cancellationToken)
    {
        Predicate = predicate;
        HasComplete = false;
    }


    public AnyAsync(IAsyncEnumerable<TSource> sources, CancellationToken cancellationToken = default) : this(sources, x => true, cancellationToken)
    { }

    protected override bool Do(TSource current)
    {
        if (Predicate(current))
        {
            HasComplete = true;
            return false;
        }
        return true;
    }

    public new async Task<bool> ExecuteAsync()
    {
        HasComplete = false;
        await base.ExecuteAsync();
        return HasComplete;
    }
}

