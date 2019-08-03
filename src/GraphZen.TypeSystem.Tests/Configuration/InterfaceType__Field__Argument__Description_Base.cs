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
    public class InterfaceType__Field__Argument__Description_Base : LeafElementConfigurationTests<IDescription, IMutableDescription, ArgumentDefinition, Argument, String>
    {
        public override string ValueA { get; } = nameof(ValueA);
        public override string ValueB { get; } = nameof(ValueB);
        public override string ValueC { get; } = nameof(ValueC);
        public override void DefineParentExplicitly(SchemaBuilder sb, out string parentName)
        {
            var argName = "ExplicitArgument";
            parentName = argName;
            sb.Interface(GreatGrandparentName)
                .Field(GrandparentName, "String", fb => fb.Argument(argName, "String"));
        }

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Interface(GreatGrandparentName)
                .Field(GrandparentName, fb => fb.Argument(parentName, ab => ab.Description(value)));
        }

        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription definition) =>
            definition.GetDescriptionConfigurationSource();

        public override ArgumentDefinition GetParentDefinitionByName(SchemaDefinition schemaDefinition, string parentName) => schemaDefinition
            .GetInterface(GreatGrandparentName).GetField(GrandparentName).GetArgument(parentName);

        public override Argument GetParentByName(Schema schema, string parentName) => schema.GetInterface(GreatGrandparentName)
            .GetField(GrandparentName).GetArgument(parentName);

        public override bool TryGetValue(IDescription parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override void RemoveExplicitly(SchemaBuilder sb, string parentName)
        {
        }
    }
}
