using System;
namespace System.Linq.Async.Commands;

public enum CommandBehavior : byte { InsertNew = 1, ApplyUpdate = 2, ExecuteDelete = 3 }

public interface ICommandStategy
{
    IAsyncCommand<T> CreateCommand<T>(CommandBehavior commandBehavior);
}

public interface ICommandStrategy<T>
{
    IAsyncCommand<T> CreateCommand(CommandBehavior commandBehavior);
}


