using System;
using System.Linq.Async.Commands;
using System.Linq.Async.Providers;
using System.Linq.Async.Reflection;
using System.Linq.Expressions;

namespace System.Linq.Async.Strategies;

public static class Aggregation
{

    private static readonly IMethod s_all_info              = new Method<IQueryable<object>>(x => x.All(y => true));
    private static readonly IMethod s_any_predicate         = new Method<IQueryable<object>>(x => x.Any(y => true));
    private static readonly IMethod s_any                  = new Method<IQueryable<object>>(x => x.Any());
    private static readonly IMethod s_count                = new Method<IQueryable<object>>(x => x.Count());
    private static readonly IMethod s_count_predicate      = new Method<IQueryable<object>>(x => x.Count(y=>true));
    private static readonly IMethod s_long_count           = new Method<IQueryable<object>>(x => x.LongCount());
    private static readonly IMethod s_long_count_predicate = new Method<IQueryable<object>>(x => x.LongCount(y => true));
    private static readonly IMethod s_min          = new Method<IQueryable<object>>(x => x.Min(y => 1));
    private static readonly IMethod s_max          = new Method<IQueryable<object>>(x => x.Max(y => 1));



    private static readonly MethodHashSet s_sum_hash = new ();
    private static readonly MethodHashSet s_avg_hash = new();


    private class Aggregate<T>
    {
        internal static IMethod? s_sum;
        internal static IMethod? s_avg;

    }


    private static void RegisterSum<T>(Expression<Action<IQueryable<T>>> expression)
    {
        IMethod m ;
        Aggregate<T>.s_sum = m = new Method<IQueryable<T>>(expression);
        s_sum_hash.Add(m);
    }

    private static void RegisterAvg<T>(Expression<Action<IQueryable<T>>> expression)
    {
        IMethod m;
        Aggregate<T>.s_avg = m = new Method<IQueryable<T>>(expression);
        s_avg_hash.Add(m);
    }


    public static bool IsSumCall(MethodCallExpression caller) => s_sum_hash.Contains(caller);
    public static bool IsAverageCall(MethodCallExpression caller) => s_avg_hash.Contains(caller);

    static Aggregation()
    {

        RegisterSum<int>(x => x.Sum(y => int.MinValue));
        RegisterSum<long>(x => x.Sum(y => long.MinValue));
        RegisterSum<decimal>(x => x.Sum(y => decimal.MinValue));
        RegisterSum<float>(x => x.Sum(y => float.MinValue));
        RegisterSum<double>(x => x.Sum(y => double.MinValue));
        RegisterSum<short>(x => x.Sum(y => short.MinValue));
        RegisterSum<byte>(x => x.Sum(y => byte.MinValue));


        RegisterAvg<int>(x => x.Average(y => int.MinValue));
        RegisterAvg<long>(x => x.Average(y => long.MinValue));
        RegisterAvg<decimal>(x => x.Average(y => decimal.MinValue));
        RegisterAvg<float>(x => x.Average(y => float.MinValue));
        RegisterAvg<double>(x => x.Average(y => double.MinValue));
        RegisterAvg<short>(x => x.Average(y => short.MinValue));
        RegisterAvg<byte>(x => x.Average(y => byte.MinValue));

    }


    internal static Expression CountOf<T>(Expression expression) => s_count.Call<T>(expression);


    internal static  Task<bool> GetFlagAsync<T>(this IQueryable<T> query, Expression expression)=> query.Provider.GetCommand(expression).ExecuteExistsAsync();


    internal static IAsyncCommand IsAll<T>(this IQueryable<T> query, LambdaExpression predicate) => query.Provider.GetCommand(s_all_info.Call<T>(query.Expression, predicate));
    internal static IAsyncCommand IsAny<T>(this IQueryable<T> query, LambdaExpression predicate) => query.Provider.GetCommand(s_any_predicate.Call<T>(query.Expression, predicate));
    internal static IAsyncCommand IsAny<T>(this IQueryable<T> query) => query.Provider.GetCommand(s_any.Call<T>(query.Expression));



    internal static IAsyncCommand GetCount<T>(this IQueryable<T> query, LambdaExpression predicate) => query.Provider.GetCommand(s_count_predicate.Call<T>(query.Expression, predicate));
    internal static IAsyncCommand GetCount<T>(this IQueryable<T> query) => query.Provider.GetCommand(s_count.Call<T>(query.Expression));
    internal static IAsyncCommand GetLongCount<T>(this IQueryable<T> query, LambdaExpression predicate) => query.Provider.GetCommand(s_long_count_predicate.Call<T>(query.Expression, predicate));
    internal static IAsyncCommand GetLongCount<T>(this IQueryable<T> query) => query.Provider.GetCommand(s_long_count.Call<T>(query.Expression));


    internal static IAsyncCommand GetMin<T>(this IQueryable<T> query, LambdaExpression selector) => query.Provider.GetCommand(s_min.Call<T>(query.Expression, selector));
    internal static IAsyncCommand GetMax<T>(this IQueryable<T> query, LambdaExpression selector) => query.Provider.GetCommand(s_max.Call<T>(query.Expression, selector));

    internal static IAsyncCommand GetSum<TSource,TResult>(this IQueryable<TSource> query, Expression<Func<TSource,TResult>> selector) => query.Provider.GetCommand((Aggregate<TResult>.s_sum??throw new NullReferenceException()).Call<TSource>(query.Expression, selector));
    internal static IAsyncCommand GetAverage<TSource, TResult>(this IQueryable<TSource> query, Expression<Func<TSource, TResult>> selector) => query.Provider.GetCommand((Aggregate<TResult>.s_avg ?? throw new NullReferenceException()).Call<TSource>(query.Expression, selector));

}

