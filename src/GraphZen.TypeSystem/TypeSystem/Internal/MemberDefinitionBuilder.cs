// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class MemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder
        where TDefinition : MemberDefinition
    {
        protected MemberDefinitionBuilder(TDefinition definition,
            InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }


        public new TDefinition Definition => (TDefinition)base.Definition;
    }


    public abstract class MemberDefinitionBuilder
    {
        protected MemberDefinitionBuilder(MemberDefinition definition,
            InternalSchemaBuilder schemaBuilder)
        {
            Definition = definition;
            SchemaBuilder = schemaBuilder;
        }


        public MemberDefinition Definition { get; }


        public virtual InternalSchemaBuilder SchemaBuilder { get; }


        public SchemaDefinition Schema => SchemaBuilder.Definition;
    }
}