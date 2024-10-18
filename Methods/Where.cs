using System;
using System.Linq.Async.Filters;

namespace System.Linq.Async.Methods
{
    public class Where<TSource> : AsyncEnumerableFilter<TSource>
    {
        public Where(IAsyncEnumerable<TSource> sources, Func<TSource, bool> predicate) : base(sources, predicate)
        {
        }
    }
}

