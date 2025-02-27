using System;
using System.Linq.Async.Providers;
using System.Linq.Expressions;

namespace System.Linq.Async.Commands;

internal readonly struct CommandProxy(IQueryProvider provider, Expression expression) : IAsyncCommand
{
    public int Execute() => provider.Execute<int>(expression);
    public Task<TElement[]> ExecuteArrayAsync<TElement>(CancellationToken cancellationToken = default) => Task.FromResult(provider.Execute<TElement[]>(expression));
    public Task<int> ExecuteAsync(CancellationToken cancellationToken = default) => Task.FromResult(provider.Execute<int>(expression));
    public Task<bool> ExecuteExistsAsync(CancellationToken cancellationToken = default) => Task.FromResult(provider.Execute<bool>(expression));
    public Task<TObject> ExecuteObjectAsync<TObject>(CancellationToken cancellationToken = default) => Task.FromResult(provider.Execute<TObject>(expression));
    public Task<TValue> ExecuteValueAsync<TValue>(CancellationToken cancellationToken = default) => Task.FromResult(provider.Execute<TValue>(expression));
}
