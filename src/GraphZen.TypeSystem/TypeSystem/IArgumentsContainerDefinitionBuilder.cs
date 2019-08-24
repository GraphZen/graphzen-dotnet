#nullable disable
using System;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IArgumentsContainerDefinitionBuilder<out TBuilder>
    {
        [NotNull]
        TBuilder Argument(string name, Action<InputValueBuilder> configurator = null);

        [NotNull]
        TBuilder Argument(string name, string type, Action<InputValueBuilder> configurator = null);

        [NotNull]
        TBuilder Argument<TArgument>(string name, Action<InputValueBuilder> configurator = null);


        [NotNull]
        TBuilder IgnoreArgument(string name);

        [NotNull]
        TBuilder UnignoreArgument(string name);
    }
}