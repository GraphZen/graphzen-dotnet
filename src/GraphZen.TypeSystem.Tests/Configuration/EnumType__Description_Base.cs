// Last generated: Saturday, July 20, 2019 4:20:50 PM
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
    public class EnumType__Description_Base : LeafElementConfigurationTests<IDescription, IMutableDescription,
        EnumTypeDefinition, EnumType, string>
    {
        public override string ValueA { get; } = "A: Enum explicit description";
        //public override string ValueA  => throw new NotImplementedException();
        public override string ValueB { get; } = "B: Enum explicit description";
        public override string ValueC { get; } = "C: Enum explicit description";

        public override void ConfigureParentExplicitlyByName(SchemaBuilder sb, string name)
        {
            sb.Enum(name);
        }

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Enum(parentName).Description(value);
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override EnumTypeDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition, string parentName) =>
            schemaDefinition.GetEnum(parentName);

        public override EnumType GetParentByName(Schema schema, string parentName) => schema.GetEnum(parentName);

        public override bool TryGetValue(IDescription parent, out object value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}