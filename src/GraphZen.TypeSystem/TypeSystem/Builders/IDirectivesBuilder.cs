using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IDirectivesBuilder
    {
        IDirectivesBuilder AddDirectiveAnnotation(string name, object value);
        IDirectivesBuilder AddDirectiveAnnotation(string name);
        IDirectivesBuilder RemoveDirectiveAnnotations(string name);
        IDirectivesBuilder ClearDirectiveAnnotations();
    }

    public interface IDirectivesBuilder<out TBuilder> : IDirectivesBuilder
    {
        new TBuilder AddDirectiveAnnotation(string name, object value);
        new TBuilder AddDirectiveAnnotation(string name);
        new TBuilder RemoveDirectiveAnnotations(string name);
        new TBuilder ClearDirectiveAnnotations();
    }
}