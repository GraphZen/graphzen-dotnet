using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface INamedTypeBuilder :
        INamedBuilder,
        IDescriptionBuilder,
        IClrTypeBuilder,
        IAnnotableBuilder
    {
    }

    public interface INamedTypeBuilder<TBuilder, TUntypedBuilder> :
        INamedBuilder<TBuilder>,
        IDescriptionBuilder<TBuilder>,
        IClrTypeBuilder<TUntypedBuilder>,
        IAnnotableBuilder<TBuilder>
    {
    }
}