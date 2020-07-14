// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Interfaces.Fields.Description
{
    // ReSharper disable once InconsistentNaming
    public abstract class Interface_Field_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, MutableField, Field>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Interface(Grandparent).Field(parentName, "String");
        }

        public override Field GetParent(Schema schema, string parentName) =>
            schema.GetInterface(Grandparent).GetField(parentName);

        public override MutableField GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInterface(Grandparent).GetField(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Interface(Grandparent).Field(parentName, v => v.Description(value!));
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Interface(Grandparent).Field(parentName, v => v.RemoveDescription());
        }

        public override bool TryGetValue(Field parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(MutableField parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}