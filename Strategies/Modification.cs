using System;
using System.Linq.Async.Commands;
using System.Linq.Async.Reflection;
using System.Linq.Async.Strategies;
using System.Linq.Expressions;

namespace System.Linq.Async.Providers;

public static class Modification
{

    private readonly static IMethod s_insert = new Method<IQueryable<object>>(x => x.Insert(x => true));
    private readonly static IMethod s_update = new Method<IQueryable<object>>(x => x.Update(x => true));
    private readonly static IMethod s_delete = new Method<IQueryable<object>>(x => x.Delete());



    public static IMethod GetInsertInfo() => s_insert;
    public static IMethod GetUpdateInfo() => s_update;
    public static IMethod GetDeleteInfo() => s_delete;


    public static bool IsCallInsert(this MethodCallExpression @this) => s_insert.Is(@this);
    public static bool IsCallUpdate(this MethodCallExpression @this) => s_update.Is(@this);
    public static bool IsCallDelete(this MethodCallExpression @this) => s_delete.Is(@this);



    internal static IAsyncCommand InsertNew<TEntity, TResult>    (this IQueryable<TEntity> @this, Expression<Func<TEntity, TResult>> lambda   ) => @this.Provider.GetCommand(s_insert.Call<TEntity, TResult>(@this.Expression, lambda));
    internal static IAsyncCommand ApplyUpdate<TEntity, TResult>  (this IQueryable<TEntity> @this, Expression<Func<TEntity, TResult>> lambda  ) => @this.Provider.GetCommand(s_update.Call<TEntity, TResult>(@this.Expression, lambda));
    internal static IAsyncCommand ExecuteDelete<TEntity>(this IQueryable<TEntity> @this) => @this.Provider.GetCommand(s_delete.Call<TEntity>(@this.Expression));



    private readonly struct d_sync_scope : IAsyncScope
    {
        public void Cancel() { }
        public Task CancelAsync() => Task.CompletedTask;

        public void Dispose() { }
        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }



    internal static async Task<IAsyncScope> GetScopeAsync<T>(this IQueryable<T> query)
    {
        if (query.Provider is IAsyncQueryProvider provider)
        {
            return await provider.GetAsyncScope();
        }

        return new d_sync_scope();
    }


}

