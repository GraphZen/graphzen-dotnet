using GraphZen.Infrastructure;
using GraphZen.TypeSystem;

namespace GraphZen.MetaModel
{
    public interface IElementConfigurationFixture
    {
        void DefineParent(SchemaBuilder sb, string parentName);
        Member GetParent(Schema schema, string parentName);
        MemberDefinition GetParent(SchemaBuilder schemBuilder, string parentName);
    }
}