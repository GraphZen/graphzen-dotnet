using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IMutableInputFieldsContainerDefinition : IInputFieldsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, InputFieldDefinition> Fields { get; }

        ConfigurationSource? FindIgnoredFieldConfigurationSource([NotNull] string fieldName);

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputFieldDefinition> GetFields();
    }
}