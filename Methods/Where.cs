using System.Linq.Async.Filters;

namespace System.Linq.Async.Methods;

public class Where<TSource>(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate)
        : AsyncEnumerableFilter<TSource>(sources, predicate);


