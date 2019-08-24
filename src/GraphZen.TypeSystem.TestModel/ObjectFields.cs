// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System;
using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen
{
    public abstract class ObjectFields : CollectionElementConfigurationFixture<IFieldsContainer,
        IFieldsContainerDefinition, IMutableFieldsContainerDefinition, FieldDefinition, Field, ObjectTypeDefinition, ObjectType>
    {
        public override void DefineParent(SchemaBuilder sb, string parentName) => sb.Object(parentName);

        public override ObjectType GetParent(Schema schema, string parentName) => throw new NotImplementedException();

        public override ObjectTypeDefinition GetParent(SchemaBuilder schemaBuilder, string parentName) => throw new NotImplementedException();

        public override IReadOnlyDictionary<string, FieldDefinition> GetCollection(ObjectTypeDefinition parent) => throw new NotImplementedException();

        public override IReadOnlyDictionary<string, Field> GetCollection(ObjectType parent) => throw new NotImplementedException();

        public override ConfigurationSource? FindItemIgnoredConfigurationSource(ObjectTypeDefinition parent, string name) => throw new NotImplementedException();

        public override void AddItem(SchemaBuilder sb, string parentName, string name)
        {
            throw new NotImplementedException();
        }

        public override void IgnoreItem(SchemaBuilder sb, string parentName, string name)
        {
            throw new NotImplementedException();
        }

        public override void UnignoreItem(SchemaBuilder sb, string parentName, string name)
        {
            throw new NotImplementedException();
        }

        public override void RenameItem(SchemaBuilder sb, string parentName, string name, string newName)
        {
            throw new NotImplementedException();
        }
    }
}