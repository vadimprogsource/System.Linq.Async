using System;
using System.Collections;
using System.Linq.Async.Providers;

namespace System.Linq.Async.Strategies
{
    internal static class Pagination
    {


        private readonly struct d_page<T>(int pageIndex, int pageSize, int totalRec, IEnumerable<T> recs)
            : IPageResult<T>
        {
            public int TotalCount => totalRec;

            public int Pages
            {
                get
                {
                    int pages = totalRec / pageSize;
                    if (totalRec % pageSize > 0) ++pages;
                    return pages;
                }
            }

            public int PageIndex => pageIndex;

            public int PageSize => pageSize;

            public IPageResult<X> Convert<X>(Func<T, X> convertor) => new d_page<X>(pageIndex, pageSize, totalRec, recs.Select(convertor));
            public IEnumerator<T> GetEnumerator() => recs.GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }



        internal static IQueryable<T> TakePage<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
        {
            int skipped = pageIndex * pageSize;

            if (skipped > 0)
            {
                queryable = queryable.Skip(skipped);
            }

            return queryable.Take(pageSize);
        }

        internal static IPageResult<T> GetPage<T>(this IQueryable<T> query, int pageIndex, int pageSize) => new d_page<T>(pageIndex, pageSize, query.Count(), query.TakePage(pageIndex, pageSize).ToArray());



        internal static async Task<IPageResult<T>> GetPageAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            if (query.Provider is IAsyncQueryProvider provider)
            {
                int recs = await provider.GetCommand(Aggregation.CountOf<T>(query.Expression)).ExecuteValueAsync<int>(cancellationToken);

                if (recs > 0)
                {
                    return new d_page<T>(pageIndex, pageSize, recs, await provider.GetCommand(query.TakePage(pageIndex, pageSize).Expression).ExecuteArrayAsync<T>(cancellationToken));
                }
                return new d_page<T>(pageIndex, pageSize, recs, Array.Empty<T>());
            }

            return query.GetPage(pageIndex,pageSize);
        }




    }
}

