// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

#nullable disable


namespace GraphZen.TypeSystem
{
    [NoReorder]
    [UsedImplicitly]
    public class InterfaceFieldArgumentNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private interface IFooInterface
        {
            string FieldWithNamedArgs(string namedByConvention,
                [GraphQLName("CustomName")] string nameSetWithDataAnnotation);
        }

        public override string ConventionalName { get; } =
            "namedByConvention";

        public override void CreateMemberNamedByConvention(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Interface<IFooInterface>();
        }

        public override void CreateMemberWithCustomNameAttribute(SchemaBuilder schemaBuilder)
        {
            schemaBuilder.Interface<IFooInterface>();
        }

        public override void SetNameOnMemberNamedByConvention(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder
                .Interface<IFooInterface>()
                .Field<string>(nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower(),
                    f => f.Argument<string>(ConventionalName, a => a.Name(name)));
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder
                .Interface<IFooInterface>()
                .Field<string>(nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower(),
                    f => f.Argument<string>("CustomName", a => a.Name(name)));
        }


        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                    nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower()), ConventionalName);
        }


        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return MutableArgumentsContainerDefinitionArgumentAccessorExtensions.GetArgument(
                MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                    schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                    nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower()), "CustomName");
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            // ReSharper disable once PossibleNullReferenceException
            var parameterInfo = typeof(IFooInterface).GetMethod(nameof(IFooInterface.FieldWithNamedArgs))
                .GetParameters()
                .Single(p => p.Name == ConventionalName);
            return schema.GetInterface<IFooInterface>()
                .GetField(nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower())
                .GetArguments().Single(_ => _.ClrInfo == parameterInfo);
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            // ReSharper disable once PossibleNullReferenceException
            var parameterInfo = typeof(IFooInterface).GetMethod(nameof(IFooInterface.FieldWithNamedArgs))
                .GetParameters()
                .Single(p => p.Name == "nameSetWithDataAnnotation");

            return schema.GetInterface<IFooInterface>()
                .GetField(nameof(IFooInterface.FieldWithNamedArgs).FirstCharToLower())
                .GetArguments().Single(_ => _.ClrInfo == parameterInfo);
        }
    }
}