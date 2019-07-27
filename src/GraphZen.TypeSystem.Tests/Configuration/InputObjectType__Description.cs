// Last generated: Saturday, July 27, 2019 3:49:03 PM
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
    public class InputObjectType__Description : InputObjectType__Description_Cases
    {
        public override string ValueA { get; } = nameof(ValueA);
        public override string ValueB { get; } = nameof(ValueB);
        public override string ValueC { get; } = nameof(ValueC);

        public override void ConfigureParentExplicitlyByName(SchemaBuilder sb, string name)
        {
            sb.InputObject(name);
        }

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.InputObject(parentName).Description(value);
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override InputObjectTypeDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition,
            string parentName) => schemaDefinition.GetInputObject(parentName);

        public override InputObjectType GetParentByName(Schema schema, string parentName) =>
            schema.GetInputObject(parentName);

        public override bool TryGetValue(IDescription parent, out object value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}