using System;
using System.Linq.Async.Providers;
using System.Linq.Async.Strategies;
using System.Linq.Expressions;

namespace System.Linq.Async;

public static class AsyncQueryableExtension
{

    public static Task<IAsyncScope> AsyncScope<TEntity>(this IQueryable<TEntity> query) => query.GetScopeAsync();


    public static IOrderedQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, string propertyOrField) => query.CreateOrderByAsc(propertyOrField);
    public static IOrderedQueryable<TEntity> OrderByDesceling<TEntity>(this IQueryable<TEntity> query, string propertyOrField) => query.CreateOrderByDesc(propertyOrField);
    public static IOrderedQueryable<TEntity> ThenBy<TEntity>(this IOrderedQueryable<TEntity> query, string propertyOrField) => query.CreateThenByAsc(propertyOrField);
    public static IOrderedQueryable<TEntity> ThenByDesceling<TEntity>(this IOrderedQueryable<TEntity> query, string propertyOrField) => query.CreateThenByDesc(propertyOrField);


    public static int Insert<TEntity, TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression) => query.InsertNew(expression).Execute();
    public static Task<int> InsertAsync<TEntity, TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression) => query.InsertNew(expression).ExecuteAsync();
    public static int Update<TEntity, TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression) => query.ApplyUpdate(expression).Execute();
    public static Task<int> UpdateAsync<TEntity, TResult>(this IQueryable<TEntity> query, Expression<Func<TEntity, TResult>> expression) => query.ApplyUpdate(expression).ExecuteAsync();
    public static int Delete<TEntity>(this IQueryable<TEntity> query) => query.ExecuteDelete().Execute();
    public static Task<int> DeleteAsync<TEntity>(this IQueryable<TEntity> query) => query.ExecuteDelete().ExecuteAsync();



    public  static IAsyncEnumerable<T> AsEnumerableAsync<T>(this IQueryable<T> query)
    {


        if (query.Provider is IAsyncQueryProvider provider)
        {
            return provider.GetAsyncEnumerable<T>(query.Expression);
        }

        return query.Provider.Execute<IAsyncEnumerable<T>>(query.Expression);
    }

    public static IPageResult<TEntity> SinglePage<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize) => query.GetPage(pageIndex, pageSize);
 
    public static  Task<IPageResult<TEntity>> SinglePageAsync<TEntity>(this IQueryable<TEntity> query, int pageIndex, int pageSize,CancellationToken cancellationToken =default) => query.GetPageAsync(pageIndex, pageSize,cancellationToken);


    public static Task<TEntity>  FirstAsync<TEntity>(this IQueryable<TEntity> query,CancellationToken cancellationToken = default) => query.GetFirst().ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();
    public static Task<TEntity>  FirstAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetFirst(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();

    public static Task<TEntity?>  FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetFirstOrDefault().ExecuteObjectAsync<TEntity?>(cancellationToken);
    public static Task<TEntity?>  FirstOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetFirstOrDefault(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken);

    public static Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetSigle().ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();
    public static Task<TEntity> SingleAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetSigle(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();

    public static Task<TEntity?> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetSigleOrDefault().ExecuteObjectAsync<TEntity?>(cancellationToken);
    public static Task<TEntity?> SingleOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetSigleOrDefault(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken);

    public static Task<TEntity>  LastAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetLast().ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();
    public static Task<TEntity>  LastAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetLast(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken).AsNotNull();

    public static Task<TEntity?> LastOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetLastOrDefault().ExecuteObjectAsync<TEntity?>(cancellationToken);
    public static Task<TEntity?> LastOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetLastOrDefault(predicate).ExecuteObjectAsync<TEntity?>(cancellationToken);

    public static Task<TEntity?> ElementAtOrDefaultAsync<TEntity>(this IQueryable<TEntity> query, int index,CancellationToken cancellationToken =default) => query.GetElementOrDefault(index).ExecuteObjectAsync<TEntity?>(cancellationToken);


    public static Task<bool> AllAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.IsAll(predicate).ExecuteExistsAsync(cancellationToken);
    public static Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.IsAny(predicate).ExecuteExistsAsync(cancellationToken);
    public static Task<bool> AnyAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.IsAny().ExecuteExistsAsync(cancellationToken);

    public static Task<int>  CountAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetCount().ExecuteValueAsync<int>(cancellationToken);
    public static Task<int>  CountAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity,bool>> predicate, CancellationToken cancellationToken = default) => query.GetCount(predicate).ExecuteValueAsync<int>(cancellationToken);
    public static Task<long> LongCountAsync<TEntity>(this IQueryable<TEntity> query, CancellationToken cancellationToken = default) => query.GetLongCount().ExecuteValueAsync<long>(cancellationToken);
    public static Task<long> LongCountAsync<TEntity>(this IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default) => query.GetLongCount(predicate).ExecuteValueAsync<long>(cancellationToken);


    public static Task<TValue> MinAsync<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, CancellationToken cancellationToken = default) => query.GetMin(selector).ExecuteValueAsync<TValue>(cancellationToken);
    public static Task<TValue> MaxAsync<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, CancellationToken cancellationToken = default) => query.GetMax(selector).ExecuteValueAsync<TValue>(cancellationToken);
    public static Task<TValue> SumAsync<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, CancellationToken cancellationToken = default) => query.GetSum(selector).ExecuteValueAsync<TValue>(cancellationToken);
    public static Task<TValue> AverageAsync<TEntity, TValue>(this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, CancellationToken cancellationToken = default) => query.GetAverage(selector).ExecuteValueAsync<TValue>(cancellationToken);



    public static async Task<T[]> ToArrayAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default) => await query.Provider.GetCommand(query.Expression).ExecuteArrayAsync<T>(cancellationToken);
    public static async Task<IList<T>> ToListAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default) => (await query.Provider.GetCommand(query.Expression).ExecuteArrayAsync<T>(cancellationToken)).ToList();
    public static async Task<HashSet<T>> ToHashSetAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken = default) => (await query.Provider.GetCommand(query.Expression).ExecuteArrayAsync<T>(cancellationToken)).ToHashSet();

    public static async Task<IDictionary<TKey, TEntity>> ToDictionaryAsyc<TKey, TEntity>(this IQueryable<TEntity> query, Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default) where TKey :notnull
    {
        return await query.AsEnumerableAsync().ToDictionaryAsync(keySelector, cancellationToken);
    }

    public static async Task<ILookup<TKey, TEntity>> ToLookupAsync<TKey, TEntity>(this IQueryable<TEntity> query, Func<TEntity, TKey> keySelector, CancellationToken cancellationToken = default) => await query.AsEnumerableAsync().ToLookupAsync(keySelector, cancellationToken);



}

