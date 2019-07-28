// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective
using System;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;
namespace GraphZen.Configuration
{
    public class EnumType__EnumValue__Description_Base : LeafElementConfigurationTests<IDescription, IMutableDescription, EnumValueDefinition, EnumValue, String>
    {
        public override string ValueA { get; } = nameof(ValueA);
        public override string ValueB { get; } = nameof(ValueB);
        public override string ValueC { get; } = nameof(ValueC);

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Enum(Grandparent).Value(parentName, v => v.Description(value));
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(Grandparent).Value(parentName, v => v.Description(null));
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override EnumValueDefinition
            GetParentDefinitionByName(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetEnum(Grandparent).GetValue(parentName);

        public override EnumValue GetParentByName(Schema schema, string parentName) =>
            schema.GetEnum(Grandparent).GetValue(parentName);

        public override bool TryGetValue(IDescription parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}
