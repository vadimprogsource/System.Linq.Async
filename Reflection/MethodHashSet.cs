using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq.Async.Reflection;



public class MethodHashSet : HashSet<MethodInfo>
{

    internal readonly struct d_comparer : IEqualityComparer<MethodInfo>
    {
        public bool Equals(MethodInfo? x, MethodInfo? y) => x != null && y != null && ReferenceEquals(x, y);
        public int GetHashCode([DisallowNull] MethodInfo obj) => obj.Name.GetHashCode();
    }

    public MethodHashSet(): base(new d_comparer()) { }

    public void Add(IMethod method) => base.Add(method.GetMethodInfo());

    public bool Contains(MethodCallExpression caller) => base.Contains(caller.Method);


 }

