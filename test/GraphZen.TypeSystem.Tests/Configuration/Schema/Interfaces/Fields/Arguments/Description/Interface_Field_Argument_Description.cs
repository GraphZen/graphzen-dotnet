// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Arguments.Description
{
    // ReSharper disable once InconsistentNaming
    public abstract class Interface_Field_Argument_Description : LeafElementConfigurationFixture<IDescription,
        IDescription, IMutableDescription,
        string?, ArgumentDefinition, Argument>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(GreatGrandparent).Field(Grandparent, "String", field => field.Argument(parentName, "String"));
        }

        public override Argument GetParent(Schema schema, string parentName) => schema.GetInterface(GreatGrandparent)
            .GetField(Grandparent).GetArgument(parentName);

        public override ArgumentDefinition GetParent(SchemaBuilder sb, string parentName) => sb.GetDefinition()
            .GetInterface(GreatGrandparent).GetField(Grandparent).GetArgument(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Interface(GreatGrandparent)
                .Field(Grandparent, field => field.Argument(parentName, v => v.Description(value!)));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Interface(GreatGrandparent)
                .Field(Grandparent, field => field.Argument(parentName, v => v.RemoveDescription()));
        }

        public override bool TryGetValue(Argument parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(ArgumentDefinition parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}