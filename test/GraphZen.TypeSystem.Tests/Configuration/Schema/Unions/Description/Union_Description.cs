// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Tests.Configuration.Unions.Description
{
    // ReSharper disable once InconsistentNaming
    public abstract class Union_Description : LeafElementConfigurationFixture<IDescription, IDescription,
        IMutableDescription,
        string?, UnionTypeDefinition, UnionType>
    {
        public override string ValueA { get; } = "description a";
        public override string ValueB { get; } = "description b";

        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Union(parentName);
        }

        public override UnionType GetParent(Schema schema, string parentName) => schema.GetUnion(parentName);

        public override UnionTypeDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetUnion(parentName);


        public override ConfigurationSource GetElementConfigurationSource(IMutableDescription parent) =>
            parent.GetDescriptionConfigurationSource();

        public override void ConfigureExplicitly(SchemaBuilder sb, string parentName, string? value)
        {
            sb.Union(parentName).Description(value!);
        }

        public override void RemoveValue(SchemaBuilder sb, string parentName)
        {
            sb.Union(parentName).RemoveDescription();
        }

        public override bool TryGetValue(UnionType parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }

        public override bool TryGetValue(UnionTypeDefinition parent, [NotNullWhen(true)] out string? value)
        {
            value = parent.Description;
            return value != null;
        }
    }
}