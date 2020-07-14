using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface INameBuilder
    {
        INameBuilder Name(string name);
    }
    public interface INameBuilder<out TBuilder> : INameBuilder
    {
        new TBuilder Name(string name);
    }

}