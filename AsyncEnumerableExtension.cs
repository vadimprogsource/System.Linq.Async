using System.Linq.Async.Methods;

namespace System.Linq.Async
{
    public static class AsyncEnumerableExtension
    {



        public static IAsyncEnumerable<TResult> Select<TSource,TResult>(this IAsyncEnumerable<TSource> @this, Func<TSource, TResult> selector) => new Select<TSource,TResult>(@this, selector);


        public static IAsyncEnumerable<T> Where<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate)=>new Where<T>(@this,predicate);

        public static IAsyncOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IAsyncEnumerable<TSource> @this, Func<TSource, TKey> keySelector) => new OrderBy<TSource,TKey>(@this,keySelector).AsOrderedEnumerable();
        public static IAsyncOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> @this, Func<TSource, TKey> keySelector) => @this.Provider.CreateThenBy(keySelector);
        public static IAsyncOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IAsyncEnumerable<TSource> @this, Func<TSource, TKey> keySelector) => new OrderByDescending<TSource, TKey>(@this, keySelector).AsOrderedEnumerable();
        public static IAsyncOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IAsyncOrderedEnumerable<TSource> @this, Func<TSource, TKey> keySelector) => @this.Provider.CreateThenByDescending(keySelector);


        public static IAsyncEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IAsyncEnumerable<TSource> @this, Func<TSource, TKey> keySelector) => new GroupBy<TSource,TKey>(@this, keySelector);

        public static IAsyncEnumerable<T> Skip<T>(this IAsyncEnumerable<T> @this, int skipped) => new Skip<T>(@this, skipped);
        public static IAsyncEnumerable<T> SkipWhile<T>(this IAsyncEnumerable<T> @this, Func<T,bool> predicate) => new SkipWhile<T>(@this,predicate);
        public static IAsyncEnumerable<T> SkipLast<T>(this IAsyncEnumerable<T> @this, int skipped) => new SkipLast<T>(@this, skipped);

        public static IAsyncEnumerable<T> Take<T>(this IAsyncEnumerable<T> @this, int taken) => new Take<T>(@this, taken);
        public static IAsyncEnumerable<T> TakeLast<T>(this IAsyncEnumerable<T> @this, int taken) => new TakeLast<T>(@this, taken);
        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> @this, Func<T,bool> predicate) => new TakeWhile<T>(@this, predicate);
        public static IAsyncEnumerable<T> TakeWhile<T>(this IAsyncEnumerable<T> @this, Func<T, int,bool> predicate) => new TakeWhile<T>(@this, predicate);

        public static async Task<bool> AllAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new AllAsync<T>(@this, predicate,cancellationToken).ExecuteAsync();
       


        public static async Task<bool> AnyAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new AnyAsync<T>(@this, predicate,cancellationToken).ExecuteAsync();
       

        public static async Task<bool> AnyAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new AnyAsync<T>(@this, cancellationToken).ExecuteAsync();


        public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => (int)await new CountAsync<T>(@this, cancellationToken).ExecuteAsync();
        

        public static async Task<int> CountAsync<T>(this IAsyncEnumerable<T> @this,Func<T,bool> predicate  ,  CancellationToken cancellationToken = default) => (int)await new CountAsync<T>(@this,predicate, cancellationToken).ExecuteAsync();
 

        public static async Task<long> LongCountAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default)=> await new CountAsync<T>(@this, cancellationToken).ExecuteAsync();


        public static async Task<T?> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new FirstOrDefaultAsync<T>(@this, predicate,cancellationToken).ExecuteAsync();
     
        public static async Task<T?> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new FirstOrDefaultAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();
     
        public static async Task<T?> FirstAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new FirstAsync<T>(@this, predicate, cancellationToken).ExecuteAsync();
     
        public static async Task<T?> FirstAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new FirstAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();

        public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new SingleOrDefaultAsync<T>(@this, predicate, cancellationToken).ExecuteAsync();
      
        public static async Task<T?> SingleOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new SingleOrDefaultAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();
        

        public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new SingleAsync<T>(@this, predicate, cancellationToken).ExecuteAsync();
 
        public static async Task<T> SingleAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new SingleAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();
  
        public static async Task<T> ElementAtAsync<T>(this IAsyncEnumerable<T> @this, int index, CancellationToken cancellationToken = default) => await new ElementAtAsync<T>(@this, index, cancellationToken).ExecuteAsync();
        public static async Task<T?> ElementAtOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, int index, CancellationToken cancellationToken = default) => await new ElementAtOrDefaultAsync<T>(@this, index, cancellationToken).ExecuteAsync();

        public static async Task<T?> LastOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new LastOrDefaultAsync<T>(@this, predicate, cancellationToken).ExecuteAsync();
    
        public static async Task<T?> LastOrDefaultAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new LastOrDefaultAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();
        
        public static async Task<T?> LastAsync<T>(this IAsyncEnumerable<T> @this, Func<T, bool> predicate, CancellationToken cancellationToken = default) => await new LastAsync<T>(@this, predicate, cancellationToken).ExecuteAsync();
        
        public static async Task<T?> LastAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new LastAsync<T>(@this, x=>true, cancellationToken).ExecuteAsync();
    
        public static async Task<IEnumerable<T>> AsEnumerable<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default)=> await new ToListAsync<T>(@this, cancellationToken).ExecuteAsync();
        
        public static async Task<IList<T>> ToListAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new ToListAsync<T>(@this, cancellationToken).ExecuteAsync();
        public static async Task<T[]> ToArrayAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new ToArrayAsync<T>(@this, cancellationToken).ExecuteAsync();


        public static async Task<IDictionary<TKey, TValue>> ToDictionaryAsync<TKey, TValue, TElement>(this IAsyncEnumerable<TElement> @this, Func<TElement, TKey> keySelector, Func<TElement, TValue> valueSelector, CancellationToken cancellationToken = default) where TKey:notnull
        => await new ToDictionaryAsync<TKey, TValue, TElement>(@this, keySelector, valueSelector, cancellationToken).ExecuteAsync();

        public static async Task<IDictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(this IAsyncEnumerable<TElement> @this, Func<TElement, TKey> keySelector, CancellationToken cancellationToken = default) where TKey : notnull
        => await new ToDictionaryAsync<TKey,TElement,TElement>(@this, keySelector, x => x, cancellationToken).ExecuteAsync();


        public static async Task<HashSet<T>> ToHashSetAsync<T>(this IAsyncEnumerable<T> @this, CancellationToken cancellationToken = default) => await new ToHashSet<T>(@this, cancellationToken).ExecuteAsync();
       

        public static async Task<ILookup<TKey, TElement>> ToLookupAsync<TKey, TElement>(this IAsyncEnumerable<TElement> @this, Func<TElement, TKey> keySelector, CancellationToken cancellationToken = default)
            => await new ToLookupAsync<TKey, TElement>(@this,keySelector, cancellationToken).ExecuteAsync();
        

    }
}

