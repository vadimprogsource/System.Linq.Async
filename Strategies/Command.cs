using System;
using System.Linq.Async.Commands;
using System.Linq.Async.Providers;
using System.Linq.Expressions;

namespace System.Linq.Async.Strategies;

public static class Command
{
    public static IAsyncCommand GetCommand(this IQueryProvider @this, Expression expression)
    {
        if (@this is IAsyncQueryProvider provider)
        {
            return provider.GetCommand(expression);
        }

        return new CommandProxy(@this, expression);
    }
}

