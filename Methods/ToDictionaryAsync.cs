using System.Linq.Async.Executors;

namespace System.Linq.Async.Methods;

public class ToDictionaryAsync<TKey, TValue, TSource>(
    IAsyncEnumerable<TSource> sources,
    Func<TSource, TKey> keySelector,
    Func<TSource, TValue> valueSelector,
    CancellationToken cancellationToken = default)
    : AsyncEnumerableExecutor<TSource>(sources, cancellationToken)
    where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _handler = new();

    protected override bool Do(TSource current)
    {
        _handler.Add(keySelector(current), valueSelector(current));
        return true;
    }

    public new async Task<IDictionary<TKey, TValue>> ExecuteAsync()
    {
        await base.ExecuteAsync();
        return _handler;
    }
}

