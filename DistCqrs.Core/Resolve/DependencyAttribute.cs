using System;

namespace DistCqrs.Core.Resolve
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependencyAttribute : Attribute
    {
    }
}