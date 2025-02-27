using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class TakeWhile<TSource> : AsyncEnumerableProxy<TSource>
{
    private readonly IPredicateExecutor _executor;

    public TakeWhile(IAsyncEnumerable<TSource> sources,Func<TSource,bool> predicate) : base(sources)
    {
        _executor = new PredicateExecutor(predicate);
    }

    public TakeWhile(IAsyncEnumerable<TSource> sources, Func<TSource,int, bool> predicate) : base(sources)
    {
        _executor = new PredicateExecutorWithIndex(predicate);

    }

    private interface IPredicateExecutor
    {
        bool Execute(TSource source,int index);
    }

    private readonly struct PredicateExecutor(Func<TSource, bool> predicate) : IPredicateExecutor
    {
        public bool Execute(TSource source, int index) => predicate(source);
        
    }

    private readonly struct PredicateExecutorWithIndex(Func<TSource, int, bool> predicate) : IPredicateExecutor
    {
        public bool Execute(TSource source, int index) => predicate(source, index);
        
    }


    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, _executor);
    

    private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, IPredicateExecutor executor)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {
        private int _index = 0;

        public override async ValueTask<bool> MoveNextAsync()
        {
            if(await base.MoveNextAsync())
            {
                return executor.Execute(Current,_index++);
            }
            return false;
        }
    }

}

