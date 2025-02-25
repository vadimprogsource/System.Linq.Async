using System;
using System.Linq.Async.Providers;
using System.Linq.Expressions;

namespace System.Linq.Async.Commands;

internal readonly struct CommandProxy : IAsyncCommand
{
    private readonly IQueryProvider _provider;
    private readonly Expression    _expression;

    public CommandProxy(IQueryProvider provider, Expression expression)
    {
        _provider = provider;
        _expression = expression;
    }

    public int Execute() => _provider.Execute<int>(_expression);
    public Task<TElement[]> ExecuteArrayAsync<TElement>(CancellationToken cancellationToken = default) => Task.FromResult(_provider.Execute<TElement[]>(_expression));
    public Task<int> ExecuteAsync(CancellationToken cancellationToken = default) => Task.FromResult(_provider.Execute<int>(_expression));
    public Task<bool> ExecuteExistsAsync(CancellationToken cancellationToken = default) => Task.FromResult(_provider.Execute<bool>(_expression));
    public Task<TObject> ExecuteObjectAsync<TObject>(CancellationToken cancellationToken = default) => Task.FromResult(_provider.Execute<TObject>(_expression));
    public Task<TValue> ExecuteValueAsync<TValue>(CancellationToken cancellationToken = default) => Task.FromResult(_provider.Execute<TValue>(_expression));
}
