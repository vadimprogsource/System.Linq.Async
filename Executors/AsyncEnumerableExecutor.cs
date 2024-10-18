using System;
namespace System.Linq.Async.Executors
{
    public abstract class AsyncEnumerableExecutor<TSource> 
    {
        private readonly IAsyncEnumerator<TSource> handler;


        public AsyncEnumerableExecutor(IAsyncEnumerable<TSource> sources,CancellationToken cancellationToken =default )
        {
            handler = sources.GetAsyncEnumerator(cancellationToken);
        }

        public async Task ExecuteAsync()
        {
            try
            {
                while (await handler.MoveNextAsync())
                {
                    if (Do(handler.Current)) continue;
                    break;
                }
                    
            }
            finally
            {
                await handler.DisposeAsync();
            }
        }



        protected abstract bool Do(TSource current);



    }
}

