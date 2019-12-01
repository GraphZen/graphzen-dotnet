// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.InputObjects.Description
{
    public abstract class InputObject_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, InputObjectTypeDefinition, InputObjectType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(parentName);
        }

        public override InputObjectType GetParent(Schema schema, string parentName) =>
            schema.GetInputObject(parentName);

        public override InputObjectTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetInputObject(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.InputObject(parentName).Description(value);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(parentName).Description(null);
        }

        public override bool TryGetValue(InputObjectType parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(InputObjectTypeDefinition parent, out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}