using System;
namespace System.Linq.Async.Commands
{
    public interface IAsyncCommandStrategy<T>
    {
        IAsyncCommand<T> CreateAsyncCommand(CommandBehavior commandBehavior);
    }
}

