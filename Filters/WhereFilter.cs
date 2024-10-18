using System;
namespace System.Linq.Async.Filters
{
    public class WhereFilter<TSource> 
    {
        private readonly List<Func<TSource, bool>> where = new();

        public void AndAlso(Func<TSource, bool> predicate) => where.Add(predicate);

        public bool Is(TSource source)
        {
            foreach (Func<TSource, bool> criteria in where)
            {
                if (criteria(source)) continue;
                return false;
            }

            return true;
        }

        public IEnumerable<TSource> Apply(IEnumerable<TSource> sources)
        {
            foreach (TSource source in sources)
            {
                if (Is(source)) yield return source;
            }
        }


        public async IAsyncEnumerable<TSource> Apply(IAsyncEnumerable<TSource> sources)
        {
            await foreach (TSource source in sources)
            {
                if (Is(source)) yield return source;
            }
        }
    }
}

