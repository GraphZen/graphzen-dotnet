// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Objects
{
    public abstract class Object_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, ObjectTypeDefinition, ObjectType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName);
        }

        public override ObjectType GetParent(Schema schema, string parentName) => schema.GetObject(parentName);

        public override ObjectTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetObject(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Object(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName).Description(null);
        }

        public override bool TryGetValue(ObjectType parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(ObjectTypeDefinition parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}