using System;
using System.Collections.Generic;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Providers;

public class OrderedProvider<T>(IAsyncEnumerable<T> chain) : IOrderedProvider<T>
{
    private readonly IAsyncEnumerable<T>     _chain = chain;
    private readonly List<OrderExecutor<T>> _sorter = new();

    protected IAsyncOrderedEnumerable<T> CreateOrderBy<TKey>(Func<T, TKey> keySelector)
    {
        _sorter.Add(new OrderByExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    protected IAsyncOrderedEnumerable<T> CreateOrderByDesceling<TKey>(Func<T, TKey> keySelector)
    {
        _sorter.Add(new OrderByDescelingExecutor<T, TKey>(keySelector));
        return new Enumerable(this);

    }

    public IAsyncOrderedEnumerable<T> CreateThenBy<TKey>(Func<T, TKey> keySelector)
    {
        _sorter.Add(new OrderByExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    public IAsyncOrderedEnumerable<T> CreateThenByDesceling<TKey>(Func<T, TKey> keySelector)
    {
        _sorter.Add(new OrderByDescelingExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    private IOrderedEnumerable<T> ExecuteOrder(IEnumerable<T> enumerable)
    {
        return OrderExecutor<T>.ExecuteWith(enumerable, _sorter);
    }


    private readonly struct Enumerable(OrderedProvider<T> provider) : IAsyncOrderedEnumerable<T>
    {
        public IOrderedProvider<T> Provider => provider;

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new Enumerator(provider, provider._chain.GetAsyncEnumerator(cancellationToken));
        
    }

    private class Enumerator(OrderedProvider<T> provider, IAsyncEnumerator<T> enumerator)
        : AsyncEnumeratorExecutor<T, T>(enumerator)
    {
        protected override IEnumerator<T> Execute(IEnumerable<T> sources)=> provider.ExecuteOrder(sources).GetEnumerator();

     
       
    }

    public IAsyncOrderedEnumerable<T> AsOrderedEnumerable() => new Enumerable(this);
}

