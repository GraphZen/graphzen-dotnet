// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using GraphZen.Infrastructure;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

// ReSharper disable AssignNullToNotNullAttribute

namespace GraphZen.MetaModel
{
    public static class GraphQLMetaModel
    {
        [NotNull]
        public static LeafElement Name { get; } = LeafElement.Create<INamed, IMutableNamed>(nameof(Name),
            nameof(INamed.Name),
            new ConfigurationScenarios
            {
                Define = new[]
                    {ConfigurationSource.Convention, ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit}
            });

        [NotNull]
        public static LeafElement Description { get; } = LeafElement.Create<IDescription, IMutableDescription>(
            nameof(Description),
            nameof(IDescription.Description),
            new ConfigurationScenarios
            {
                Define = new[] { ConfigurationSource.DataAnnotation, ConfigurationSource.Explicit },
                Remove = new[] { ConfigurationSource.Explicit }
            }, true);

        [NotNull]
        public static Vector Directive { get; } = new Vector(nameof(Directive), nameof(TypeSystem.Directive))
        {
            Name, Description, ArgumentsViaClrProperties, ArgumentsViaClrFields, ArgumentsViaClrConstructorParameters
        };

        [NotNull]
        public static Collection DirectiveAnnotations { get; } =
            new Collection(nameof(DirectiveAnnotations), nameof(DirectiveAnnotations), Directive);


        public static Vector ScalarType { get; } = new Vector(nameof(ScalarType), nameof(TypeSystem.ScalarType))
        {
            Name, Description, DirectiveAnnotations
            // new LeafElement("ValueParser", null),
            // new LeafElement("LiteralParser", null),
            // new LeafElement("ClrType", null)
        };

        public static Vector FieldViaClrProperty => Field(nameof(FieldViaClrProperty));

        public static Vector FieldViaClrMethod =>
            Field(nameof(FieldViaClrProperty)).Add(ArgumentsViaClrMethodParameters);

        public static Collection FieldsVialClrProperties { get; } =
            new Collection(nameof(FieldsVialClrProperties), nameof(IFieldsContainer.Fields), FieldViaClrProperty);

        public static Collection FieldsVialClrMethods { get; } =
            new Collection(nameof(FieldsVialClrMethods), nameof(IFieldsContainer.Fields), FieldViaClrProperty);

        public static Collection ExplicitFields =>
            new Collection(nameof(ExplicitFields), nameof(IFieldsContainer.Fields), ExplicitField);

        public static Vector ExplicitField => Field(nameof(ExplicitField)).Add(ExplicitArguments);


        [NotNull]
        public static Vector InterfaceType { get; } =
            new Vector(nameof(InterfaceType), nameof(TypeSystem.InterfaceType))
            {
                Name, Description, DirectiveAnnotations, FieldsVialClrProperties
            };

        [NotNull]
        public static Vector ObjectType { get; } = new Vector(nameof(ObjectType), nameof(TypeSystem.ObjectType))
        {
            Name, Description, DirectiveAnnotations, FieldsVialClrProperties, FieldsVialClrMethods, ExplicitFields
        };


        [NotNull]
        public static Vector UnionType { get; } = new Vector(nameof(UnionType), nameof(TypeSystem.UnionType))
        {
            Name, Description, DirectiveAnnotations, // new LeafElement("ClrType", null),
            new Collection("MemberTypes", nameof(TypeSystem.UnionType.MemberTypes), ObjectType)
        };

        [NotNull]
        public static Vector InputObjectType { get; } =
            new Vector(nameof(InputObjectType), nameof(TypeSystem.InputObjectType))
            {
                Name, Description, DirectiveAnnotations,
                new Collection("InputFields", nameof(TypeSystem.InputObjectType.Fields), InputField())
            };

        [NotNull]
        public static Vector EnumValue { get; } = new Vector(nameof(EnumValue), nameof(TypeSystem.EnumValue))
        {
            Name, Description, DirectiveAnnotations // new LeafElement("CustomValue", null) { Optional = true }
        };

        [NotNull]
        public static Vector EnumType { get; } = new Vector(nameof(EnumType), nameof(TypeSystem.EnumType))
        {
            Name, Description, DirectiveAnnotations, new Collection("Values", nameof(IEnumType.Values), EnumValue)
        };


        [NotNull]
        public static Vector Schema { get; } = new Vector(nameof(Schema), nameof(TypeSystem.Schema))
        {
            DirectiveAnnotations,
            new Collection("Directives", nameof(TypeSystem.Schema.Directives), Directive),
            new Collection("Objects", nameof(TypeSystem.Schema.Types), ObjectType),
            new Collection("Interfaces", nameof(TypeSystem.Schema.Types), InterfaceType),
            new Collection("Unions", nameof(TypeSystem.Schema.Types), UnionType),
            new Collection("Scalars", nameof(TypeSystem.Schema.Types), ScalarType),
            new Collection("Enums", nameof(TypeSystem.Schema.Types), EnumType),
            new Collection("InputObjects", nameof(TypeSystem.Schema.Types), InputObjectType)
            // new LeafElement("QueryType", null),
            // new LeafElement("MutationType", null),
            // new LeafElement("SubscriptionType", null)
        };

        [NotNull]
        [ItemNotNull]
        public static IReadOnlyList<Element> Elements { get; } =
            ImmutableArray.Create(Schema, Directive, ExplicitArgument(), EnumValue, EnumType, ScalarType, UnionType,
                InputObjectType, ObjectType,
                InterfaceType, InputField());

        private static Collection ExplicitArguments => new Collection(nameof(ExplicitArguments),
            nameof(TypeSystem.Argument), ExplicitArgument());

        public static Collection ArgumentsViaClrMethodParameters =>
            new Collection(nameof(ArgumentsViaClrMethodParameters), nameof(IArgumentsContainer.Arguments),
                ArgumentViaClrMethodParameter);

        public static Vector ArgumentViaClrMethodParameter => Argument(nameof(ArgumentViaClrMethodParameter));

        public static Collection ArgumentsViaClrConstructorParameters =>
            new Collection(nameof(ArgumentsViaClrConstructorParameters), nameof(IArgumentsContainer.Arguments),
                ArgumentViaClrConstructorParameter);

        public static Vector ArgumentViaClrConstructorParameter => Argument(nameof(ArgumentViaClrConstructorParameter));

        public static Collection ArgumentsViaClrProperties =>
            new Collection(nameof(ArgumentsViaClrProperties), nameof(IArgumentsContainer.Arguments),
                ArgumentViaClrProperty);

        public static Vector ArgumentViaClrProperty => Argument(nameof(ArgumentViaClrProperty));

        public static Collection ArgumentsViaClrFields =>
            new Collection(nameof(ArgumentsViaClrFields), nameof(IArgumentsContainer.Arguments), ArgumentViaClrField);

        public static Vector ArgumentViaClrField => Argument(nameof(ArgumentViaClrField));

        [NotNull]
        [ItemNotNull]
        public static IEnumerable<Element> ElementsDeep()
        {
            var all = new List<Element>();


            void AddElements(IEnumerable<Element> elements)
            {
                // ReSharper disable once PossibleNullReferenceException
                foreach (var element in elements)
                {
                    all.Add(element);
                    AddElements(element);
                }
            }

            AddElements(Elements);

            return all.ToImmutableArray();
        }


        [NotNull]
        public static Vector Field(string name) => new Vector(name, nameof(TypeSystem.Field))
        {
            Name, Description, DirectiveAnnotations
        };

        [NotNull]
        private static Vector Argument(string name) => InputValue(name, nameof(TypeSystem.Argument));

        private static Vector ExplicitArgument() => Argument(nameof(ExplicitArgument));


        public static Vector InputField() => InputValue(nameof(InputField), nameof(TypeSystem.InputField));

        [NotNull]
        public static Vector InputValue([NotNull] string name, string member) => new Vector(name, member)
        {
            Name, Description, DirectiveAnnotations
            /*new LeafElement("DefaultValue", null)
            {
                Optional = true
            }*/
        };
    }
}