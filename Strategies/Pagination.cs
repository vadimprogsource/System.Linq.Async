using System;
using System.Collections;
using System.Linq.Async.Providers;

namespace System.Linq.Async.Strategies
{
    internal static class Pagination
    {


        private readonly struct d_page<T> : IPageResult<T>
        {
            private readonly int page_index;
            private readonly int page_size;
            private readonly int total_rec;
            private readonly IEnumerable<T> page_content;


            public d_page(int pageIndex, int pageSize, int totalRec, IEnumerable<T> recs)
            {
                page_index = pageIndex;
                page_size = pageSize;
                total_rec = totalRec;
                page_content = recs;
            }

            public int TotalCount => total_rec;

            public int Pages
            {
                get
                {
                    int pages = total_rec / page_size;
                    if (total_rec % page_size > 0) ++pages;
                    return pages;
                }
            }

            public int PageIndex => page_index;

            public int PageSize => page_size;

            public IPageResult<X> Convert<X>(Func<T, X> convertor) => new d_page<X>(page_index, page_size, total_rec, page_content.Select(convertor));
            public IEnumerator<T> GetEnumerator() => page_content.GetEnumerator();
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

