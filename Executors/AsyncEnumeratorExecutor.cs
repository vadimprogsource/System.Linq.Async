using System;
using System.Linq.Async.Enums;

namespace System.Linq.Async.Executors;

public abstract class AsyncEnumeratorExecutor<TSource, TResult>(IAsyncEnumerator<TSource> source)
    : IAsyncEnumerator<TResult>
{
    private IEnumerator<TResult>? _executed = null;


    public  TResult Current=>(_executed ?? throw new NullReferenceException()).Current;
        

    public  ValueTask DisposeAsync()
    {
        if (_executed is IDisposable disp) disp.Dispose();
        return ValueTask.CompletedTask;
    }

    protected abstract  IEnumerator<TResult> Execute(IEnumerable<TSource> sources);


    public async  ValueTask<bool> MoveNextAsync()
    {
        if (_executed == null)
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

            _executed = Execute(list);
        }

        return _executed.MoveNext();

    }
}

