using System;
namespace System.Linq.Async.Commands;


public interface IAsyncCommand : ICommand
{
    Task<int> ExecuteAsync(CancellationToken cancellationToken = default);
    Task<TObject> ExecuteObjectAsync<TObject>(CancellationToken cancellationToken = default);
    Task<TElement[]> ExecuteArrayAsync<TElement>(CancellationToken cancellationToken=default);

    Task<bool> ExecuteExistsAsync(CancellationToken cancellationToken = default);
    Task<TValue> ExecuteValueAsync<TValue>(CancellationToken cancellationToken = default);
}


