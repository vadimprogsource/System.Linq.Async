using System;
namespace System.Linq.Async.Commands;


public readonly struct AsyncCommand<T>(Func<T, Task> exec) : IAsyncCommand<T>
{
    public async Task<T> ExecuteAsync(T obj)
    {
        await exec(obj);
        return obj;
    }
}

