using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IMaybeDeprecatedBuilder
    {
        IMaybeDeprecatedBuilder Deprecated(bool deprecated);
        IMaybeDeprecatedBuilder Deprecated(string reason);

    }

    public interface IMaybeDeprecatedBuilder<out TBuilder> : IMaybeDeprecatedBuilder
    {
        new TBuilder Deprecated(bool deprecated);
        new TBuilder Deprecated(string reason);
    }
}