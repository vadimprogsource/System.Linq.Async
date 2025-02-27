using System;
using System.Collections;
using System.Linq.Async.Reflection;
using System.Linq.Expressions;

namespace System.Linq.Async.Strategies;

public static class Orderable
{

    private static readonly  IMethod s_order_asc   = new Method<IQueryable<object>>(x => x.OrderBy(y => true));
    private static readonly  IMethod s_order_desc  = new Method<IQueryable<object>>(x => x.OrderByDescending(y => true));
    private static readonly  IMethod s_then_asc    = new Method<IOrderedQueryable<object>>(x =>x.ThenBy(y => true));
    private static readonly  IMethod s_then_desc   = new Method<IOrderedQueryable<object>>(x => x.ThenByDescending(y => true));


    private struct d_query<T>(IQueryProvider provider, Expression expression) : IOrderedQueryable<T>
    {
        public Type ElementType => typeof(T);

        public Expression Expression => expression;

        public IQueryProvider Provider => provider;

        public IEnumerator<T> GetEnumerator() => provider.Execute<IEnumerable<T>>(expression).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


    private static LambdaExpression MakeKeySelector(Type type , string key)
    {
        ParameterExpression x = Expression.Parameter(type, "x");
        return Expression.Lambda(Expression.PropertyOrField(x, key), x);
    }


    internal static IOrderedQueryable<T> CreateOrderByAsc<T>(this IQueryable<T> query, string name      ) => new d_query<T>(query.Provider, s_order_asc.Call<T>(query.Expression  , MakeKeySelector(typeof(T),name)));
    internal static IOrderedQueryable<T> CreateOrderByDesc<T>(this IQueryable<T> query, string name     ) => new d_query<T>(query.Provider, s_order_desc.Call<T>(query.Expression, MakeKeySelector(typeof(T), name)));
    internal static IOrderedQueryable<T> CreateThenByAsc<T>(this IOrderedQueryable<T> query, string name) => new d_query<T>(query.Provider, s_then_asc.Call<T>(query.Expression, MakeKeySelector(typeof(T), name)));
    internal static IOrderedQueryable<T> CreateThenByDesc<T>(this IQueryable<T> query, string name      ) => new d_query<T>(query.Provider, s_then_desc.Call<T>(query.Expression, MakeKeySelector(typeof(T), name)));


}

