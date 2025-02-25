using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;

public interface IMethodCodeBuilder : ICodeBuilder
{

    Delegate BuildDelegate(Type methodType);
    TDelegate BuildDelegate<TDelegate>();

}


public interface IMethodCodeBuilder<TArgument, TResult> : IMethodCodeBuilder
{
    Func<TArgument, TResult> BuildFunc();
}

