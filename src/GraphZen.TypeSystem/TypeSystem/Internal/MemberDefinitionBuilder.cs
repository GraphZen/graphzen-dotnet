// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem.Internal
{
    public abstract class MemberDefinitionBuilder<TDefinition> : MemberDefinitionBuilder
        where TDefinition : MemberDefinition
    {
        protected MemberDefinitionBuilder([NotNull] TDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder) : base(
            definition, schemaBuilder)
        {
        }

        [NotNull]
        public new TDefinition Definition => (TDefinition)base.Definition;
    }


    public abstract class MemberDefinitionBuilder
    {
        protected MemberDefinitionBuilder([NotNull] MemberDefinition definition,
            [NotNull] InternalSchemaBuilder schemaBuilder)
        {
            Definition = definition;
            SchemaBuilder = schemaBuilder;
        }

        [NotNull]
        public MemberDefinition Definition { get; }

        [NotNull]
        public virtual InternalSchemaBuilder SchemaBuilder { get; }

        [NotNull]
        public SchemaDefinition Schema => SchemaBuilder.Definition;
    }
}