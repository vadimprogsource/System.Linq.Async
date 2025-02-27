using System;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Async.Reflection;

public class Method : IMethod
{

    public static IEnumerable<Method> GetMethodsByName(Type type, string methodName)
    {
        return type.GetMember(methodName).Where(x => x.MemberType == MemberTypes.Method).OfType<MethodInfo>().Select(x => new Method(x));
    }


    public static MethodInfo GetKeyMethod(MethodInfo m)
    {
        if (m.IsGenericMethod)
        {
            return m.GetGenericMethodDefinition();
        }

        return m;
    }

    public MethodInfo GetMethodInfo()
    {
        return _methodInfo;
    }


    private MethodInfo _methodInfo;

    public Method(MethodInfo methodInfo)
    {
        _methodInfo = GetKeyMethod(methodInfo);
    }


    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is Method m)
        {
            return m._methodInfo == _methodInfo;
        }

        if (obj is MethodCallExpression expr)
        {
            return _methodInfo == GetKeyMethod((expr).Method);
        }

        if (obj is MethodInfo mi)
        {
            return _methodInfo == GetKeyMethod(mi);
        }

        return false;
    }

    public bool Is(MethodCallExpression methodCall)
    {

        if (_methodInfo.IsGenericMethodDefinition)
        {
            return _methodInfo == GetKeyMethod(methodCall.Method);
        }

        return _methodInfo == methodCall.Method || _methodInfo.Name == methodCall.Method.Name;

    }



    public override int GetHashCode()
    {
        return typeof(Method).GetHashCode() ^ _methodInfo.GetHashCode();
    }


    public Expression Call<T>(Expression operand)
    {
        return Expression.Call(_methodInfo.MakeGenericMethod(typeof(T)), operand);
    }

    public Expression Call<T>(Expression left, LambdaExpression right)
    {
        return Expression.Call(_methodInfo.MakeGenericMethod(typeof(T)), left, Expression.Quote(right));
    }

    public Expression Call<T>(Expression left, ConstantExpression right)
    {
        return Expression.Call(_methodInfo.MakeGenericMethod(typeof(T)), left, right);
    }

    public Expression Call<T, V>(Expression left, LambdaExpression right)
    {
        return Expression.Call(_methodInfo.MakeGenericMethod(typeof(T), typeof(V)), left, Expression.Quote(right));
    }


    public Expression Call<T, V>(Expression @this, LambdaExpression left, LambdaExpression right)
    {
        return Expression.Call(_methodInfo.MakeGenericMethod(typeof(T), typeof(V)), @this, Expression.Quote(left), Expression.Quote(right));
    }


    public Expression EmitCall(Expression left, LambdaExpression right)
    {
        if (_methodInfo.IsGenericMethodDefinition)
        {
            return Expression.Call(_methodInfo.MakeGenericMethod(left.Type, right.ReturnType), left, Expression.Quote(right));
        }

        return Expression.Call(_methodInfo, left, Expression.Quote(right));
    }


    public Expression EmitCall(Expression operand)
    {
        if (_methodInfo.IsGenericMethodDefinition)
        {
            return Expression.Call(_methodInfo.MakeGenericMethod(operand.Type), operand);
        }

        return Expression.Call(_methodInfo, operand);
    }

    public MethodInfo GetBaseMethod()
    {
        return GetKeyMethod(_methodInfo);
    }

    public MethodInfo MakeGenericMethod(params Type[] type) => _methodInfo.MakeGenericMethod(type);

}

public class Method<T>(Expression<Action<T>> methodCall) : Method((methodCall.Body is MethodCallExpression call)
    ? call.Method
    : throw new NotSupportedException());

