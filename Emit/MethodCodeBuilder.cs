using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;
public  class CodeDelegateBuilder :CodeBuilder  ,  IMethodCodeBuilder
{
 
    private readonly DynamicMethod method;


    public CodeDelegateBuilder(Type? ownerType, Type returnType, params Type[] argumentTypes)  : base()
    {
        ownerType ??= typeof(void);
        method = new($"{ownerType.Name}.{Guid.NewGuid():n}", returnType, argumentTypes ?? Array.Empty<Type>(), ownerType);
        Generate(method.GetILGenerator());
    }


    public virtual Delegate BuildDelegate(Type methodType)
    {
        Build();
        return method.CreateDelegate(methodType);
    }

    public TDelegate BuildDelegate<TDelegate>() => BuildDelegate(typeof(TDelegate)) is TDelegate d ? d : throw new NotSupportedException();

}

public class CodeDelegateBuilder<TArgument, TResult> : CodeDelegateBuilder, IMethodCodeBuilder<TArgument, TResult>
{
    public CodeDelegateBuilder(Type? ownerType = null) : base(ownerType, typeof(TResult), typeof(TArgument))
    {

    }

    public Func<TArgument, TResult> BuildFunc() => BuildDelegate<Func<TArgument, TResult>>();
}



