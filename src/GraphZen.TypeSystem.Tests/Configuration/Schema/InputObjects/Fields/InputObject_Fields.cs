// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;
#nullable disable

// ReSharper disable PossibleNullReferenceException

namespace GraphZen.InputObjects.Fields
{
    public abstract class InputObject_Fields :
        CollectionConfigurationFixture<IInputFieldsContainer,
            IInputFieldsContainerDefinition, IMutableInputFieldsContainerDefinition, InputFieldDefinition, InputField,
            InputObjectTypeDefinition,
            InputObjectType>
    {
        public override void ConfigureParentExplicitly(SchemaBuilder sb, string parentName)
        {
            sb.InputObject(parentName);
        }

        public override InputObjectType GetParent(Schema schema, string parentName)
        {
            return schema.GetInputObject(parentName);
        }

        public override InputObjectTypeDefinition GetParent(SchemaBuilder sb, string parentName)
        {
            return sb.GetDefinition().GetInputObject(parentName);
        }

        public override NamedCollection<InputFieldDefinition> GetCollection(InputObjectTypeDefinition parent)
        {
            return parent.Fields.ToNamedCollection();
        }

        public override NamedCollection<InputField> GetCollection(InputObjectType parent)
        {
            return parent.Fields.ToNamedCollection();
        }

        public override ConfigurationSource? FindIgnoredItemConfigurationSource(InputObjectTypeDefinition parent,
            string name)
        {
            return parent.FindIgnoredFieldConfigurationSource(name);
        }

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.InputObject(parentName).Field(name, "String");
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.InputObject(parentName).IgnoreField(name);
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            sb.InputObject(parentName).UnignoreField(name);
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            sb.InputObject(parentName).Field(name, f => f.Name(newName));
        }
    }
}