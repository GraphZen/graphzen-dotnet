﻿using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.Interfaces.Fields.Arguments.Description
{
    public abstract class Interface_Field_Argument_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, ArgumentDefinition, Argument>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(GreatGrandparent).Field(Grandparent, field => field.Argument(parentName));
        }

        public override Argument GetParent(Schema schema, string parentName) =>
            schema.GetInterface(GreatGrandparent).GetField(Grandparent).GetArgument(parentName);

        public override ArgumentDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(GreatGrandparent).GetField(Grandparent).GetArgument(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Interface(GreatGrandparent)
                .Field(Grandparent, field => field.Argument(parentName, v => v.Description(value)));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Interface(GreatGrandparent)
                            .Field(Grandparent, field => field.Argument(parentName, v => v.Description(null)));

        }

        public override bool TryGetValue(Argument parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(ArgumentDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}