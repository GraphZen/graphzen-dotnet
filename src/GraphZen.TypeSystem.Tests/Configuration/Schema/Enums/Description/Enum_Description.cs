// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.Enums.Description
{
    public abstract class Enum_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, EnumTypeDefinition, EnumType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName);
        }

        public override EnumType GetParent(Schema schema, string parentName) => schema.GetEnum(parentName);

        public override EnumTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetEnum(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.Enum(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Enum(parentName).Description(null);
        }

        public override bool TryGetValue(EnumType parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(EnumTypeDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}