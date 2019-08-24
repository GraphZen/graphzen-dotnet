using System.Collections.Generic;
using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Taxonomy
{
    public interface IInputFieldsContainer : IInputFieldsContainerDefinition
    {
        [NotNull]
        IReadOnlyDictionary<string, InputField> Fields { get; }

        [NotNull]
        [ItemNotNull]
        new IEnumerable<InputField> GetFields();
    }
}