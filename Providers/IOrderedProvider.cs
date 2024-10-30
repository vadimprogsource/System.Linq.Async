using System;
namespace System.Linq.Async.Providers
{
    public interface IOrderedProvider<TSource>
    {
        IAsyncOrderedEnumerable<TSource> CreateThenBy<TKey>(Func<TSource, TKey> keySelector);
        IAsyncOrderedEnumerable<TSource> CreateThenByDescending<TKey>( Func<TSource, TKey> keySelector);

    }
}

