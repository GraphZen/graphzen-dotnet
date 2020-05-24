// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.InputObjects.Fields.Description
{
    // ReSharper disable once InconsistentNaming
    public abstract class InputObject_Field_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, InputFieldDefinition, InputField>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(Grandparent).Field(parentName, "String");
        }

        public override InputField GetParent(Schema schema, string parentName) =>
            schema.GetInputObject(Grandparent).GetField(parentName);

        public override InputFieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInputObject(Grandparent).GetField(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.InputObject(Grandparent).Field(parentName, v => v.Description(value!));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(Grandparent).Field(parentName, v => v.RemoveDescription());
        }

        public override bool TryGetValue(InputField parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(InputFieldDefinition parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}