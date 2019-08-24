using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface IConfigurationFixture 

    {
        Type ParentMemberType { get; }
        Type ParentMemberDefinitionType { get; }
        void ConfigureParentExplicitly([NotNull]SchemaBuilder sb, [NotNull] string parentName);
        Member GetParent([NotNull]Schema schema, [NotNull] string parentName);
        MemberDefinition GetParent([NotNull]SchemaBuilder sb, [NotNull]string parentName);
    }
}