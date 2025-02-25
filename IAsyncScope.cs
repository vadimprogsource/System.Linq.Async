using System;
namespace System.Linq.Async
{
    public interface IScope : IDisposable
    {
        void Cancel();
    }

    public interface IAsyncScope : IScope, IAsyncDisposable
    {
        Task CancelAsync();
    }
}

