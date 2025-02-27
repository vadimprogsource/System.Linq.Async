using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;
public  class CodeDelegateBuilder :CodeBuilder  ,  IMethodCodeBuilder
{
 
    private readonly DynamicMethod _method;


    protected CodeDelegateBuilder(Type? ownerType, Type returnType, params Type[] argumentTypes)  : base()
    {
        ownerType ??= typeof(void);
        _method = new($"{ownerType.Name}.{Guid.NewGuid():n}", returnType, argumentTypes ??[], ownerType);
        Generate(_method.GetILGenerator());
    }


    public virtual Delegate BuildDelegate(Type methodType)
    {
        Build();
        return _method.CreateDelegate(methodType);
    }

    public TDelegate BuildDelegate<TDelegate>() => BuildDelegate(typeof(TDelegate)) is TDelegate d ? d : throw new NotSupportedException();

}

public class CodeDelegateBuilder<TArgument, TResult>(Type? ownerType = null)
    : CodeDelegateBuilder(ownerType, typeof(TResult), typeof(TArgument)), IMethodCodeBuilder<TArgument, TResult>
{
    public Func<TArgument, TResult> BuildFunc() => BuildDelegate<Func<TArgument, TResult>>();
}



