// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class ObjectFieldArgumentNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private class FooObject
        {
            public string FieldWithNamedArgs(
                [UsedImplicitly] string namedByConvention,
                [GraphQLName("CustomName")] string nameSetWithDataAnnotation) => "";
        }

        public override string ConventionalName { get; } =
            "namedByConvention";

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder) =>
            schemaBuilder.Object<FooObject>();

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
            => schemaBuilder.Object<FooObject>();

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder
                .Object<FooObject>()
                .Field<string>(nameof(FooObject.FieldWithNamedArgs).FirstCharToLower(),
                    f => f.Argument<string>(ConventionalName, a => a.Name(name)));

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name) =>
            schemaBuilder
                .Object<FooObject>()
                .Field<string>(nameof(FooObject.FieldWithNamedArgs).FirstCharToLower(),
                    f => f.Argument<string>("CustomName", a => a.Name(name)));


        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetObject<FooObject>(),
                    nameof(FooObject.FieldWithNamedArgs).FirstCharToLower()), ConventionalName);


        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder) =>
            MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetObject<FooObject>(),
                    nameof(FooObject.FieldWithNamedArgs).FirstCharToLower()), "CustomName");

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            var parameterInfo = typeof(FooObject).GetMethod(nameof(FooObject.FieldWithNamedArgs)).GetParameters()
                .Single(p => p.Name == ConventionalName);
            return schema.GetObject<FooObject>()
                .GetField(nameof(FooObject.FieldWithNamedArgs).FirstCharToLower())
                .GetArguments().Single(_ => _.ClrInfo == parameterInfo);
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            var parameterInfo = typeof(FooObject).GetMethod(nameof(FooObject.FieldWithNamedArgs)).GetParameters()
                .Single(p => p.Name == "nameSetWithDataAnnotation");

            return schema.GetObject<FooObject>()
                .GetField(nameof(FooObject.FieldWithNamedArgs).FirstCharToLower())
                .GetArguments().Single(_ => _.ClrInfo == parameterInfo);
        }
    }
}