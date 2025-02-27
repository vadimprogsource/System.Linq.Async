using System;
namespace System.Linq.Async.Filters
{
    public class WhereFilter<TSource> 
    {
        private readonly List<Func<TSource, bool>> _where = new();

        public void AndAlso(Func<TSource, bool> predicate) => _where.Add(predicate);

        public bool Is(TSource source)
        {
            foreach (Func<TSource, bool> criteria in _where)
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

