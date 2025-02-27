namespace System.Linq.Async.Methods;

public class SingleOrDefaultAsync<TSource>(
    IAsyncEnumerable<TSource> sources,
    Func<TSource, bool> predicate,
    CancellationToken cancellationToken = default)
    : FirstOrDefaultAsync<TSource>(sources, predicate, cancellationToken)
{
    protected override bool Do(TSource current)
    {
        if (Predicate(current))
        {
            if (Instance == null)
            {
                Instance = current;
                return true;
            }

            throw new Exception("Not Single");
        }
        return true;
    }
}

