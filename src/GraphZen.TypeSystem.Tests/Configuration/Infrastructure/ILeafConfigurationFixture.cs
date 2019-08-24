#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;

namespace GraphZen
{
    public interface ILeafConfigurationFixture : IConfigurationFixture
    {
        ConfigurationSource GetElementConfigurationSource([NotNull]MemberDefinition parent);
        bool TryGetValue([NotNull]MemberDefinition parent, out object value);
        bool TryGetValue([NotNull]Member parent, out object value);
        void ConfigureExplicitly([NotNull]SchemaBuilder sb, [NotNull]string parentName, object value);
        void RemoveValue([NotNull]SchemaBuilder sb, [NotNull] string parentName);

        object ValueA { get; }
        object ValueB { get; }
    }
}