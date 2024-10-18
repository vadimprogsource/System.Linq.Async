using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods
{
    public class TakeWhile<TSource> : AsyncEnumerableProxy<TSource>
    {
        private readonly IPredicateExecutor executor;

        public TakeWhile(IAsyncEnumerable<TSource> sources,Func<TSource,bool> predicate) : base(sources)
        {
            executor = new PredicateExecutor(predicate);
        }

        public TakeWhile(IAsyncEnumerable<TSource> sources, Func<TSource,int, bool> predicate) : base(sources)
        {
            executor = new PredicateExecutorWithIndex(predicate);

        }

        private interface IPredicateExecutor
        {
            bool Execute(TSource source,int index);
        }

        private readonly struct PredicateExecutor : IPredicateExecutor
        {
            private readonly Func<TSource, bool> predicate;

            public PredicateExecutor(Func<TSource, bool> predicate)
            {
                this.predicate = predicate;
            }

            public bool Execute(TSource source, int index) => predicate(source);
            
        }

        private readonly struct PredicateExecutorWithIndex : IPredicateExecutor
        {
            private readonly Func<TSource,int, bool> predicate;

            public PredicateExecutorWithIndex(Func<TSource,int, bool> predicate)
            {
                this.predicate = predicate;
            }

            public bool Execute(TSource source, int index) => predicate(source, index);
            
        }



        public override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, executor);
        

        private class AsyncEnumerator : AsyncEnumeratorProxy<TSource>
        {
            private readonly IPredicateExecutor executor;
            private int index;

            public AsyncEnumerator(IAsyncEnumerator<TSource> enumerator,IPredicateExecutor executor) : base(enumerator)
            {
                this.executor = executor;
                index = 0;
            }

            public async override ValueTask<bool> MoveNextAsync()
            {
                if(await base.MoveNextAsync())
                {
                    return executor.Execute(Current,index++);
                }
                return false;
            }
        }

    }
}

