using System;
using System.Linq.Async.Providers;

namespace System.Linq.Async;

public interface IAsyncOrderedEnumerable<T> : IAsyncEnumerable<T>
{
    IOrderedProvider<T> Provider { get; }
}

