// Last generated: Saturday, July 27, 2019 3:49:03 PM
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
    public abstract class InterfaceType__Field__Description_Base : LeafElementConfigurationTests<IDescription, IMutableDescription, FieldDefinition, Field, String>
    {
        public override string ValueA { get; } = nameof(ValueA);
        public override string ValueB { get; } = nameof(ValueB);
        public override string ValueC { get; } = nameof(ValueC);

        public abstract string InterfaceTypeName { get; }

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Interface(InterfaceTypeName).Field(parentName, "String", f => f.Description(value));
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override FieldDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition, string parentName)
        {
            return schemaDefinition.GetInterface(InterfaceTypeName).GetField(parentName);
        }

        public override Field GetParentByName(Schema schema, string parentName) =>
            schema.GetInterface(InterfaceTypeName).GetField(parentName);
        public override bool TryGetValue(IDescription parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}
