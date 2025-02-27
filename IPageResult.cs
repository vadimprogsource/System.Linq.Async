using System.Collections;

namespace System.Linq.Async;

public interface IPageResult : IEnumerable
{

    int TotalCount { get; }
    int Pages { get; }

    int PageIndex { get; }
    int PageSize { get; }

    //IPageResult<T> OfType<T>();
    
}

public interface IPageResult<T> : IPageResult, IEnumerable<T>
{
    IPageResult<X> Convert<X>(Func<T, X> convertor);
}

