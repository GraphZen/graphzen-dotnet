// ReSharper disable PossibleNullReferenceException
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable InconsistentNaming
// ReSharper disable RedundantUsingDirective

using System;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using Xunit;

namespace GraphZen.Configuration
{
    public abstract class InputObjectType__InputField__Description_Base : LeafElementConfigurationTests<IDescription,
        IMutableDescription, InputFieldDefinition, InputField, string>
    {
        public override string ValueA { get; } = nameof(ValueA);
        public override string ValueB { get; } = nameof(ValueB);
        public override string ValueC { get; } = nameof(ValueC);

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.InputObject(GrandparentName).Field(parentName, fb => fb.Description(value));
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override InputFieldDefinition
            GetParentDefinitionByName(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetInputObject(GrandparentName).GetField(parentName);

        public override InputField GetParentByName(Schema schema, string parentName) =>
            schema.GetInputObject(GrandparentName).GetField(parentName);

        public override bool TryGetValue(IDescription parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(GrandparentName).Field(parentName, fb => fb.Description(null));
        }
    }
}