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
    public class InterfaceFieldViaClrMethodNameConfigurationTests : NamedContainerItemConfigurationTests
    {
        private interface IFooInterface
        {
            string ConventionallyNamedField();

            [GraphQLName("CustomName")]
            string CustomNamedField();
        }

        public override string ConventionalName { get; } =
            nameof(IFooInterface.ConventionallyNamedField).FirstCharToLower();

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
            schemaBuilder.Interface<IFooInterface>().Field<string>(ConventionalName, _ => _.Name(name));
        }

        public override void SetNameOnMemberNamedByDataAnnotation(SchemaBuilder schemaBuilder, string name)
        {
            schemaBuilder.Interface<IFooInterface>().Field<string>("CustomName", _ => _.Name(name));
        }

        public override IMutableNamed GetMemberDefinitionNamedByConvention(SchemaBuilder schemaBuilder)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                schemaBuilder.GetDefinition().GetInterface<IFooInterface>(),
                nameof(IFooInterface.ConventionallyNamedField).FirstCharToLower());
        }


        public override IMutableNamed GetMemberDefinitionWithCustomNameDataAnnotation(SchemaBuilder schemaBuilder)
        {
            return MutableFieldsContainerDefinitionFieldAccessorExtensions.GetField(
                schemaBuilder.GetDefinition().GetInterface<IFooInterface>(), "CustomName");
        }

        public override INamed GetMemberNamedByConvention(Schema schema)
        {
            return schema.GetInterface<IFooInterface>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(IFooInterface).GetMethod(nameof(IFooInterface.ConventionallyNamedField)));
        }

        public override INamed GetMemberNamedByDataAnnotation(Schema schema)
        {
            return schema.GetInterface<IFooInterface>()
                .GetFields().SingleOrDefault(_ =>
                    _.ClrInfo == typeof(IFooInterface).GetMethod(nameof(IFooInterface.CustomNamedField)));
        }
    }
}