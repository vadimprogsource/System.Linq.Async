using System.Linq.Async.Enums;

namespace System.Linq.Async.Methods;

public class TakeLast<TSource>(IAsyncEnumerable<TSource> sources, int taken)
    : AsyncEnumerableProxy<TSource>(sources)
{
    protected override IAsyncEnumerator<TSource> CreateAsyncEnumerator(IAsyncEnumerator<TSource> enumerator) => new AsyncEnumerator(enumerator, taken);
   
    private class AsyncEnumerator(IAsyncEnumerator<TSource> enumerator, int taken)
        : AsyncEnumeratorProxy<TSource>(enumerator)
    {
        private Queue<TSource>? _queue;
        private TSource? _current;

        public override TSource Current => _current ?? throw new NullReferenceException();

        public override async ValueTask<bool> MoveNextAsync()
        {
            if (_queue == null)
            {
                _queue = new();

                try
                {
                    while (await base.MoveNextAsync())
                    {
                        if (_queue.Count >= taken)
                        {
                            _queue.Dequeue();
                        }

                        _queue.Enqueue(base.Current);
                    }
                }
                finally
                {
                    await base.DisposeAsync();
                }
            }

            if (_queue.Count > 0)
            {
                _current = _queue.Dequeue();
                return true;
            }

            return false;
        }
    }
}

