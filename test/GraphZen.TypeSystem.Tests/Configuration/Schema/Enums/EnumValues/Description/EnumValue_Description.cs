// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Enums.EnumValues.Description
{
    // ReSharper disable once InconsistentNaming
    public abstract class EnumValue_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, EnumValueDefinition, EnumValue>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(Grandparent).Value(parentName);
        }

        public override EnumValue GetParent(Schema schema, string parentName) =>
            schema.GetEnum(Grandparent).GetValue(parentName);

        public override EnumValueDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetEnum(Grandparent).GetValue(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Enum(Grandparent).Value(parentName, v => v.Description(value!));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Enum(Grandparent).Value(parentName, v => v.RemoveDescription());
        }

        public override bool TryGetValue(EnumValue parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(EnumValueDefinition parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}