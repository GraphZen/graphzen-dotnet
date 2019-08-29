// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Objects.Fields.Description
{
    public abstract class Object_Field_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, FieldDefinition, Field>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(Grandparent).Field(parentName);
        }

        public override Field GetParent(Schema schema, string parentName) =>
            schema.GetObject(Grandparent).GetField(parentName);

        public override FieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetObject(Grandparent).GetField(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Object(Grandparent).Field(parentName, v => v.Description(value));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Object(Grandparent).Field(parentName, v => v.Description(null));
        }

        public override bool TryGetValue(Field parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(FieldDefinition parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}