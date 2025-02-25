using System;
using System.Linq.Async.Reflection;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Async.Emit;

public class CodeBuilder : ICodeBuilder
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ILGenerator _generator;
    protected CodeBuilder() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


    public CodeBuilder(ILGenerator generator)
    {
        _generator = generator;
    }

    protected void Generate(ILGenerator generator)
    {
        _generator = generator;
    }



    public ICodeBuilder This()
    {
        _generator.Emit(OpCodes.Dup);
        return this;
    }


    public ICodeBuilder Argument()
    {
        _generator.Emit(OpCodes.Ldarg_0);
        return this;
    }


    public ICodeBuilder ArgumentSecond()
    {
        _generator.Emit(OpCodes.Ldarg_1);
        return this;
    }





    public ICodeBuilder NewObject<T>() => NewObject(typeof(T));


    public ICodeBuilder NewObject(Type type) => NewObject(type.GetConstructor(Array.Empty<Type>()) ?? type.GetConstructors().First());


    public ICodeBuilder NewObject(ConstructorInfo ctor)
    {
        _generator.Emit(OpCodes.Newobj, ctor);
        return this;
    }


    private class IfBlock : IDisposable
    {
        private readonly ILGenerator generator;
        private readonly Label label;

        public IfBlock(ILGenerator gen)
        {
            generator = gen;
            label = gen.DefineLabel();
            gen.Emit(OpCodes.Brfalse, label);
        }

        public void Dispose()
        {
            generator.MarkLabel(label);
        }
    }

    public IDisposable If(Action<ICodeBuilder> condition)
    {
        condition(this);
        return new IfBlock(_generator);
    }



    public ICodeBuilder GetValue(FieldInfo field)
    {
        _generator.Emit(OpCodes.Ldfld, field);
        return this;
    }


    public ICodeBuilder SetValue(FieldInfo field)
    {
        _generator.Emit(OpCodes.Stfld, field);
        return this;
    }


    private ICodeBuilder EmitCallMethod(MethodInfo method)
    {
        _generator.Emit(method.IsVirtual || (method.DeclaringType !=null && method.DeclaringType.IsInterface) ? OpCodes.Callvirt : OpCodes.Call, method);
        return this;
    }

    public ICodeBuilder GetValue(PropertyInfo property) => EmitCallMethod(property.GetGetMethod() ?? throw new NullReferenceException());

    public ICodeBuilder SetValue(PropertyInfo property) => EmitCallMethod(property.GetSetMethod() ?? throw new NullReferenceException());



    public ICodeBuilder GetValue(MemberInfo member)
    {

        if (member.MemberType == MemberTypes.Field && member is FieldInfo field)
        {
            return GetValue(field);
        }

        if (member.MemberType == MemberTypes.Property && member is PropertyInfo property)
        {
            return GetValue(property);
        }

        throw new NotSupportedException();
    }

    public ICodeBuilder SetValue(MemberInfo member)
    {
        if (member.MemberType == MemberTypes.Field && member is FieldInfo field)
        {
            return SetValue(field);
        }

        if (member.MemberType == MemberTypes.Property && member is PropertyInfo property)
        {
            return SetValue(property);
        }

        throw new NotSupportedException();
    }



    
    public ICodeBuilder Call(MethodInfo method) => EmitCallMethod(method);


    public ICodeBuilder Call(IMethod method, params Type[] type)
    {
        if (type == null || type.Length < 1)
        {
            return EmitCallMethod(method.GetMethodInfo());
        }

        return EmitCallMethod(method.MakeGenericMethod(type));
    }



    public void Dispose()
    {
        Build();
    }

    public ICodeBuilder ReadField(FieldInfo field, MethodInfo read) => Argument().ArgumentSecond().Call(read).SetValue(field);
    public ICodeBuilder ReadProperty(PropertyInfo prop, MethodInfo read) => Argument().ArgumentSecond().Call(read).SetValue(prop);

    public virtual ICodeBuilder Build()
    {
        _generator.Emit(OpCodes.Ret);
        return this;
    }

    public ValueTask DisposeAsync()
    {
        Dispose();
        return ValueTask.CompletedTask;
    }

}

