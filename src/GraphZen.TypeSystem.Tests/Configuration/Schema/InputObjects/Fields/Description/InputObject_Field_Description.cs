// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using JetBrains.Annotations;
#nullable disable
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.InputObjects.Fields.Description
{
    public abstract class InputObject_Field_Description : LeafElementConfigurationFixture<IDescription, IDescription, IMutableDescription,
        string, InputFieldDefinition, InputField>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(Grandparent).Field(parentName);
        }

        public override InputField GetParent(Schema schema, string parentName) =>
            schema.GetInputObject(Grandparent).GetField(parentName);

        public override InputFieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInputObject(Grandparent).GetField(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string value)
        {
            sb.InputObject(Grandparent).Field(parentName, v => v.Description(value));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(Grandparent).Field(parentName, v => v.Description(null));
        }

        public override bool TryGetValue(InputField parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(InputFieldDefinition parent, out string value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}