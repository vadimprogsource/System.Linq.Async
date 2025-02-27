namespace System.Linq.Async.Providers;

public interface IOrderedProvider<TSource>
{
    IAsyncOrderedEnumerable<TSource> CreateThenBy<TKey>(Func<TSource, TKey> keySelector);
    IAsyncOrderedEnumerable<TSource> CreateThenByDesceling<TKey>( Func<TSource, TKey> keySelector);

}

