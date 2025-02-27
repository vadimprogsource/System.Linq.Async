using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Async.Reflection;

public interface IMethod
{
    MethodInfo GetMethodInfo();
    MethodInfo GetBaseMethod();


    MethodInfo MakeGenericMethod(params Type[] type);


    bool Is(MethodCallExpression method);

    Expression Call<T>(Expression operand);
    Expression Call<T>(Expression left, ConstantExpression right);
    Expression Call<T>(Expression left, LambdaExpression right);
    Expression Call<T, V>(Expression left, LambdaExpression right);
    Expression Call<T, V>(Expression @this, LambdaExpression left, LambdaExpression right);

    Expression EmitCall(Expression left, LambdaExpression lambda);
    Expression EmitCall(Expression left);


}

