// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.Internal;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [NoReorder]
    public static class Introspection
    {
        public static Schema Schema { get; } = Schema.Create(sb =>
        {
            sb.Object<IGraphQLType>()
                .Description("The fundamental unit of any GraphQL Schema is the type. There are " +
                             "many kinds of types in GraphQL as represented by the `__TypeKind` enum." +
                             "\n\nDepending on the kind of a type, certain fields describe " +
                             "information about that type. Scalar types provide no information " +
                             "beyond a name and description, while Enum types provide their values. " +
                             "Object and Interface types provide the fields they describe. Abstract " +
                             "types, Union and Interface, provide the Object types possible " +
                             "at runtime. List and NonNull types compose other types.', ")
                .Field(_ => _.Kind)
                .Field("name", "String", _ => { _.Resolve(type => type is NamedType named ? named.Name : null); }
                )
                .Field("description", "String", _ => _
                    .Resolve(type => type is NamedType named ? named.Description : null))
                .Field("fields", "[__Field!]", _ =>
                {
                    _.Argument("includeDeprecated", "Boolean", a => a.DefaultValue(false))
                        .Resolve((type, args) =>
                        {
                            if (type is IFields fieldsType)
                            {
                                var includeDeprecated = args.includeDeprecated == true;
                                return fieldsType.Fields.Values.Where(field =>
                                    includeDeprecated || !field.IsDeprecated);
                            }

                            return null;
                        });
                })
                .Field("interfaces", "[__Type!]", _ =>
                {
                    _.Resolve(type =>
                        type is ObjectType obj ? obj.Interfaces.Cast<IGraphQLTypeReference>().ToList() : null);
                })
                .Field("possibleTypes", "[__Type!]", _ => _.Resolve(
                    (source, args, context, info) => source is IAbstractType abstractType
                        ? info.Schema.GetPossibleTypes(abstractType).Cast<IGraphQLTypeReference>().ToList()
                        : null))
                .Field("enumValues", "[__EnumValue!]", _ => _
                    .Argument("includeDeprecated", "Boolean", a => a.DefaultValue(false))
                    .Resolve((type, args) =>
                    {
                        if (type is EnumType enumType)
                            return enumType.GetValues()
                                .Where(f => args.includeDeprecated || !f.IsDeprecated).ToList();

                        return null;
                    }))
                .Field("inputFields", "[__InputValue!]", _ => _
                    .Resolve(type =>
                        type is InputObjectType inputObject
                            ? inputObject.Fields.Cast<InputValue>().ToList()
                            : null))
                .Field("ofType", "__Type", _ => _
                    .Resolve(source => source is IWrappingType wrapping ? wrapping.OfType : null));


            sb.Object<Schema>()
                .Field<List<IGraphQLType>>("types",
                    _ =>
                    {
                        _.Resolve(s => s.Types.Values.Cast<IGraphQLType>().ToList())
                            .Description("A list of all types supported by this server.");
                    });

            sb.Object<Directive>()
                .Field<IEnumerable<InputValue>>("args", f => f.Resolve(d => d.GetArguments()));
            sb.Enum<DirectiveLocation>();

            sb.Object<Field>()
                .Field<IEnumerable<InputValue>>("args", f => f.Resolve(d => d.GetArguments()));

            sb.Object<InputValue>()
                .Field("defaultValue", "String", _ => _
                    .Resolve((input, args, context, info) =>
                    {
                        if (input.HasDefaultValue)
                        {
                            var ast = AstFromValue.Get(Maybe.Some(input.DefaultValue!), input.InputType);
                            return ast?.ToSyntaxString();
                        }

                        return null;
                    }));

            sb.Object<EnumValue>();

            sb.Enum<TypeKind>();
        });


        public static Field SchemaMetaFieldDef { get; } = new Field("__schema",
            "Access the current type schema of this server.", null,
            NonNullType.Of(Schema.GetObject("__Schema")), null,
            (source, args, context, info) => info.Schema, null);


        public static Field TypeMetaFieldDef { get; } = new Field("__type",
            "Request the type information of a single type.", null, Schema.GetObject("__Type"),
            new[]
            {
                new Argument("name", null, NonNullType.Of(SpecScalars.String), null, null, false)
            },
            (source, args, context, info) => info.Schema.GetType(args.name), null);


        public static Field TypeNameMetaFieldDef { get; } = new Field("__typename",
            "The name of the current Object type at runtime.", null, NonNullType.Of(SpecScalars.String), null,
            (source, args, context, info) => info.ParentType.Name, null);


        public static readonly IReadOnlyList<NamedType> IntrospectionTypes =
            Schema.GetTypes()
                .Where(_ => SpecScalars.All.All(ss => ss.Name != _.Name))
                .ToImmutableList();
    }
}