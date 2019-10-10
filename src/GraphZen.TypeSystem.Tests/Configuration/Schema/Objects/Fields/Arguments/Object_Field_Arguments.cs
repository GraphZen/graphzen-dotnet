// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Configuration.Infrastructure;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.Configuration.Objects.Fields.Arguments
{
    public abstract class Object_Field_Arguments : NamedCollectionConfigurationFixture<IArguments,
        IArgumentsDefinition, IMutableArgumentsDefinition, ArgumentDefinition, Argument,
        FieldDefinition, Field>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.Object(Grandparent).Field(parentName, "String");
        }

        public override Field GetParent(Schema schema, string parentName) =>
            schema.GetObject(Grandparent).GetField(parentName);

        public override FieldDefinition GetParent(SchemaBuilder sb, string parentName) =>
            sb.GetDefinition().GetObject(Grandparent).GetField(parentName);

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(Grandparent).Field(parentName, f => f.Argument(name, "String"));
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(Grandparent).Field(parentName, f => f.IgnoreArgument(name));
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.Object(Grandparent).Field(parentName, f => f.UnignoreArgument(name));
        }

        public override NamedCollection<ArgumentDefinition> GetCollection(FieldDefinition parent) =>
            parent.Arguments.ToNamedCollection();

        public override NamedCollection<Argument> GetCollection(Field parent) => parent.Arguments.ToNamedCollection();

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(FieldDefinition parent, string name) =>
            parent.FindIgnoredArgumentConfigurationSource(name);

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.Object(Grandparent).Field(parentName, f => f.Argument(name, arg => arg.Name(newName)));
        }
    }
}