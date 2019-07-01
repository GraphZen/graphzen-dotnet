// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using GraphZen.Types.Builders;
using JetBrains.Annotations;

namespace GraphZen.Types
{
    [NoReorder]
    [UsedImplicitly]
    public class InterfaceTypeNameConfigurationTests : NameConfigurationByExplicitConfigurationTests
    {
        private interface IInterfaceNamedByConvention
        {
        }

        [GraphQLName("CustomName")]
        private interface IInterfaceNamedByDataAnnotation
        {
        }


        public override string ConventionalName { get; } = nameof(IInterfaceNamedByConvention);

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Interface<IInterfaceNamedByConvention>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Interface<IInterfaceNamedByDataAnnotation>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Interface<IInterfaceNamedByConvention>().Name(name);

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder.Interface<IInterfaceNamedByDataAnnotation>().Name(name);

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetInterface<IInterfaceNamedByConvention>();

        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            schemaBuilder.GetDefinition().GetInterface<IInterfaceNamedByDataAnnotation>();

        public override INamed GetMemberNamedByConvention(Schema schema) =>
            schema.GetInterface<IInterfaceNamedByConvention>();

        public override INamed GetMemberNamedByDataAnnotation(Schema schema) =>
            schema.GetInterface<IInterfaceNamedByDataAnnotation>();
    }
}