using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Executors;

public abstract class AsyncEnumeratorExecutor<TSource,TResult> : IAsyncEnumerator<TResult>
{
    private readonly IAsyncEnumerator<TSource> source; 
    private IEnumerator<TResult>? executed = default;


    public AsyncEnumeratorExecutor(IAsyncEnumerator<TSource> source) 
    {
        this.source = source;
    }

    public  TResult Current=>(executed ?? throw new NullReferenceException()).Current;
        

    public  ValueTask DisposeAsync()
    {
        if (executed is IDisposable disp) disp.Dispose();
        return ValueTask.CompletedTask;
    }

    protected abstract  IEnumerator<TResult> Execute(IEnumerable<TSource> sources);


    public async  ValueTask<bool> MoveNextAsync()
    {
        if (executed == null)
        {
            List<TSource> list = new();
            try
            {
                while (await source.MoveNextAsync()) list.Add(source.Current);
            }
            finally
            {
                await source.DisposeAsync();
            }

            executed = Execute(list);
        }

        return executed.MoveNext();

    }
}

