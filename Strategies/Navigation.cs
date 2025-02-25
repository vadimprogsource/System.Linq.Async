using System;
using System.Linq.Async.Commands;
using System.Linq.Async.Providers;
using System.Linq.Async.Reflection;
using System.Linq.Expressions;

namespace System.Linq.Async.Strategies;

public static class Navigation
{
    private readonly static  IMethod  s_first_predicate = new Method<IQueryable<object>>(x => x.First(y => true));
    private readonly static  IMethod  s_first = new Method<IQueryable<object>>(x => x.First());
    private readonly static  IMethod  s_first_predicate_default = new Method<IQueryable<object>>(x => x.FirstOrDefault(y => true));
    private readonly static  IMethod  s_first_default = new Method<IQueryable<object>>(x => x.FirstOrDefault());
    private readonly static  IMethod  s_single_predicate = new Method<IQueryable<object>>(x => x.Single(y => true));
    private readonly static  IMethod  s_single = new Method<IQueryable<object>>(x => x.Single());
    private readonly static  IMethod  s_sigle_predicate_default = new Method<IQueryable<object>>(x => x.SingleOrDefault(y => true));
    private readonly static  IMethod  s_single_default = new Method<IQueryable<object>>(x => x.SingleOrDefault());
    private readonly static  IMethod  s_last_predicate = new Method<IQueryable<object>>(x => x.Last(y => true));
    private readonly static  IMethod  s_last = new Method<IQueryable<object>>(x => x.Last());
    private readonly static  IMethod s_last_predicate_default = new Method<IQueryable<object>>(x => x.LastOrDefault(y => true));
    private readonly static  IMethod s_last_default = new Method<IQueryable<object>>(x => x.LastOrDefault());
    private readonly static  IMethod s_element_at = new Method<IQueryable<object>>(x => x.ElementAt(1));
    private readonly static  IMethod s_element_at_default = new Method<IQueryable<object>>(x => x.ElementAtOrDefault(1));



    internal static Task<T> AsNotNull<T>(this Task<T?> @this) => @this.ContinueWith(x=>x.Result?? throw new NullReferenceException()) ;

    internal static Task<T?> AsSingle<T>(this Task<T[]> @this) => @this.ContinueWith
    (
        x=>
        {
              if (x.Result.Length > 1)
            {
                throw new NotSupportedException();
            }

            if (x.Result.Length == 1) return x.Result[0];
            return default(T);
        }
    );
    

    internal static IAsyncCommand GetFirst<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_first.Call<T>( @this.Expression));
    internal static IAsyncCommand GetFirst<T>(this IQueryable<T> @this,LambdaExpression predicate) => @this.Provider.GetCommand(s_first_predicate.Call<T>(@this.Expression,predicate));
    internal static IAsyncCommand GetFirstOrDefault<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_first_default.Call<T>(@this.Expression));
    internal static IAsyncCommand GetFirstOrDefault<T>(this IQueryable<T> @this, LambdaExpression predicate) => @this.Provider.GetCommand(s_first_predicate_default.Call<T>(@this.Expression, predicate));


    internal static IAsyncCommand GetSigle<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_single.Call<T>(@this.Expression));
    internal static IAsyncCommand GetSigle<T>(this IQueryable<T> @this, LambdaExpression predicate) => @this.Provider.GetCommand(s_single_predicate.Call<T>(@this.Expression, predicate));


    internal static IAsyncCommand GetSigleOrDefault<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_single_default.Call<T>(@this.Expression));
    internal static IAsyncCommand GetSigleOrDefault<T>(this IQueryable<T> @this, LambdaExpression predicate) => @this.Provider.GetCommand(s_sigle_predicate_default.Call<T>(@this.Expression, predicate));

    internal static IAsyncCommand GetLast<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_last.Call<T>(@this.Expression));
    internal static IAsyncCommand GetLast<T>(this IQueryable<T> @this, LambdaExpression predicate) => @this.Provider.GetCommand(s_last_predicate.Call<T>(@this.Expression, predicate));

    internal static IAsyncCommand GetLastOrDefault<T>(this IQueryable<T> @this) => @this.Provider.GetCommand(s_last_default.Call<T>(@this.Expression));
    internal static IAsyncCommand GetLastOrDefault<T>(this IQueryable<T> @this, LambdaExpression predicate) => @this.Provider.GetCommand(s_last_predicate_default.Call<T>(@this.Expression, predicate));


    internal static IAsyncCommand GetElement<T>(this IQueryable<T> @this,int index) => @this.Provider.GetCommand(s_element_at.Call<T>(@this.Expression,Expression.Constant(index)));
    internal static IAsyncCommand GetElementOrDefault<T>(this IQueryable<T> @this, int index) => @this.Provider.GetCommand(s_element_at_default.Call<T>(@this.Expression, Expression.Constant(index)));


}

