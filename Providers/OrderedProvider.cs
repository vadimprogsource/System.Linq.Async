using System;
using System.Collections.Generic;
using System.Linq.Async.Executors;

namespace System.Linq.Async.Providers;

public class OrderedProvider<T> : IOrderedProvider<T>
{
    private readonly IAsyncEnumerable<T>     chain;
    private readonly List<OrderExecutor<T>> sorter;

    public OrderedProvider(IAsyncEnumerable<T> chain)
    {
        this.chain = chain;
        sorter     = new();
    }

    public IAsyncOrderedEnumerable<T> CreateOrderBy<TKey>(Func<T, TKey> keySelector)
    {
        sorter.Add(new OrderByExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    public IAsyncOrderedEnumerable<T> CreateOrderByDesceling<TKey>(Func<T, TKey> keySelector)
    {
        sorter.Add(new OrderByDescelingExecutor<T, TKey>(keySelector));
        return new Enumerable(this);

    }

    public IAsyncOrderedEnumerable<T> CreateThenBy<TKey>(Func<T, TKey> keySelector)
    {
        sorter.Add(new OrderByExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    public IAsyncOrderedEnumerable<T> CreateThenByDesceling<TKey>(Func<T, TKey> keySelector)
    {
        sorter.Add(new OrderByDescelingExecutor<T, TKey>(keySelector));
        return new Enumerable(this);
    }

    private IOrderedEnumerable<T> ExecuteOrder(IEnumerable<T> enumerable)
    {
        return OrderExecutor<T>.ExecuteWith(enumerable, sorter);
    }


    private readonly struct Enumerable : IAsyncOrderedEnumerable<T>
    {
        private readonly OrderedProvider<T> provider;

        public Enumerable(OrderedProvider<T> provider)
        {
            this.provider = provider;
        }

        public IOrderedProvider<T> Provider => provider;

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new Enumerator(provider, provider.chain.GetAsyncEnumerator(cancellationToken));
        
    }

    private class Enumerator : AsyncEnumeratorExecutor<T,T>
    {
        private readonly OrderedProvider<T> provider;

        public Enumerator(OrderedProvider<T> provider,IAsyncEnumerator<T> enumerator) : base(enumerator)
        {
            this.provider = provider;
        }

        protected override IEnumerator<T> Execute(IEnumerable<T> sources)=> provider.ExecuteOrder(sources).GetEnumerator();

     
       
    }

    public IAsyncOrderedEnumerable<T> AsOrderedEnumerable() => new Enumerable(this);
}

