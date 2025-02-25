using System;
namespace System.Linq.Async.Commands;


public readonly struct AsyncCommand<T> : IAsyncCommand<T>
{
    private readonly Func<T, Task> command_action;


    public AsyncCommand(Func<T, Task> exec)
    {
        command_action = exec;
    }

    public async Task<T> ExecuteAsync(T obj)
    {
        await command_action(obj);
        return obj;
    }
}

