using System;
using System.Linq.Async.Commands;
using System.Linq.Expressions;

namespace System.Linq.Async.Providers;



public interface IAsyncQueryProvider
{
    IQueryable<T>       GetQueryable<T>();
    IAsyncEnumerable<T> GetAsyncEnumerable<T>(Expression expression);
    Task<IAsyncScope>   GetAsyncScope();
    IAsyncCommand       GetCommand(Expression expression);
}




