// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class TypeReference : INamedTypeReference
    {
        private readonly TypeSyntax _seedSyntax;


        public TypeReference(TypeIdentity identity, TypeSyntax typeSyntax, IMutableDefinition declaringMember)
        {
            Identity = identity;
            DeclaringMember = declaringMember;
            _seedSyntax = typeSyntax;
        }

        public IMutableDefinition DeclaringMember { get; }


        public bool SetIdentity(TypeIdentity identity, ConfigurationSource configurationSource)
        {

            var def = identity.Definition;
            if (def != null)
            {
                if (def.IsInputType() && def.IsOutputType())
                {
                }
                else if (def.IsInputType() && DeclaringMember is IOutputDefinition)
                {
                    throw new InvalidTypeException("tbd");
                }
                else if (def.IsOutputType() && DeclaringMember is IInputDefinition)
                {
                    throw new InvalidTypeException("tbd");
                }
            }

            Identity = identity;
            return true;
        }

        public TypeIdentity Identity { get; private set; }

        public TypeSyntax TypeSyntax
        {
            get
            {
                TypeSyntax GetType(TypeSyntax node) =>
                    node switch
                    {
                        ListTypeSyntax list => SyntaxFactory.ListType(GetType(list.OfType)),
                        NonNullTypeSyntax nn => SyntaxFactory.NonNullType((NullableTypeSyntax)GetType(nn.OfType)),
                        NamedTypeSyntax _ => SyntaxFactory.NamedType(SyntaxFactory.Name(Name)),
                        _ => throw new NotImplementedException()
                    };

                return GetType(_seedSyntax);
            }
        }

        public string Name => Identity.Name;


        public IGraphQLType ToType(Schema schema)
        {
            IGraphQLType GetType(TypeSyntax node)
            {
                switch (node)
                {
                    case ListTypeSyntax list:
                        return ListType.Of(GetType(list.OfType));
                    case NonNullTypeSyntax nn:
                        return NonNullType.Of((INullableType)GetType(nn.OfType));
                    case NamedTypeSyntax _:
                        var nameMatch = schema.FindType(Identity.Name);
                        if (nameMatch != null)
                        {
                            return nameMatch;
                        }

                        if (Identity.ClrType != null)
                        {
                            var typeMatches = schema.Types.Values
                                .Where(_ => _.ClrType != null && _.ClrType.IsAssignableFrom(Identity.ClrType))
                                .ToArray();

                            if (typeMatches.Length == 1)
                            {
                                return typeMatches[0];
                            }

                            if (typeMatches.Length > 1)
                            {
                                throw new Exception(
                                    $"More than one type in the schema matched type reference  \"{Identity.Name}\" with CLR type {Identity.ClrType}");
                            }

                            throw new Exception(
                                $"Unable to find output type for type reference named \"{Identity.Name}\" with CLR type {Identity.ClrType}");
                        }

                        throw new Exception(
                            $"Unable to find output type for type reference named \"{Identity.Name}\"");
                }

                throw new Exception($"Unable to create type reference from type node: {node?.GetType()}");
            }


            return GetType(TypeSyntax);
        }

        public override string ToString() => $"ref: {TypeSyntax} | {Identity}";
    }
}