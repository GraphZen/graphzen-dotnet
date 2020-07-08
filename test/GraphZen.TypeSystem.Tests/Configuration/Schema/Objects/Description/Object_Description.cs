// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Objects.Description
{
    // ReSharper disable once InconsistentNaming
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
            sb.Object(parentName).Description(value!);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Object(parentName).RemoveDescription();
        }

        public override bool TryGetValue(ObjectType parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(ObjectTypeDefinition parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}