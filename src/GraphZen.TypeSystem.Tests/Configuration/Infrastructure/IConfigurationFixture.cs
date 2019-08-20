using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen
{
    public interface IConfigurationFixture
    {
        void ConfigureParentExplicitly(SchemaBuilder sb, string parentName);
        Member GetParent(Schema schema, string parentName);
        MemberDefinition GetParent(SchemaBuilder schemBuilder, string parentName);
    }
}