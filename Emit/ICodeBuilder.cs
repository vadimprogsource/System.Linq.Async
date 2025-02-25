using System;
using System.Linq.Async.Reflection;
using System.Reflection;

namespace System.Linq.Async.Emit;


public interface ICodeBuilder : IDisposable , IAsyncDisposable
{

    IDisposable If(Action<ICodeBuilder> condition);

    ICodeBuilder This();
    ICodeBuilder Argument();
    ICodeBuilder ArgumentSecond();
    ICodeBuilder NewObject<T>();
    ICodeBuilder NewObject(Type type);
    ICodeBuilder NewObject(ConstructorInfo ctor);
    ICodeBuilder GetValue(FieldInfo field);
    ICodeBuilder GetValue(PropertyInfo property);
    ICodeBuilder GetValue(MemberInfo member);
    ICodeBuilder SetValue(FieldInfo field);
    ICodeBuilder SetValue(PropertyInfo property);
    ICodeBuilder SetValue(MemberInfo member);

    ICodeBuilder ReadField(FieldInfo field, MethodInfo read);
    ICodeBuilder ReadProperty(PropertyInfo prop, MethodInfo read);

    ICodeBuilder Call(MethodInfo method);
    ICodeBuilder Call(IMethod method ,params Type[] arguments);

    ICodeBuilder Build();

}

