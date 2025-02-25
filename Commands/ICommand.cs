using System;
namespace System.Linq.Async.Commands;

public interface ICommand
{
    int Execute();
}

public interface IAsyncCommand<T>
{
    Task<T> ExecuteAsync(T obj);
}