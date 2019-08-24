// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public class TypeReference : INamedTypeReference
    {
        public TypeReference([NotNull] TypeIdentity identity, [NotNull] TypeSyntax typeSyntax)
        {
            Identity = Check.NotNull(identity, nameof(identity));
            TypeSyntax = Check.NotNull(typeSyntax, nameof(typeSyntax));

            if (identity.ClrType == typeof(IEnumTypeDefinition))
            {
                throw new Exception("hullo");
            }
        }

        [NotNull]
        public TypeIdentity Identity { get; }

        [NotNull]
        public TypeSyntax TypeSyntax { get; }

        public string Name => Identity.Name;

        [NotNull]
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
                                .Where(_ =>
                                {
                                    Debug.Assert(_ != null, nameof(_) + " != null");
                                    return _.ClrType != null;
                                })
                                .Where(_ => _.ClrType.IsAssignableFrom(Identity.ClrType)).ToArray();

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

            // ReSharper disable once AssignNullToNotNullAttribute
            return GetType(TypeSyntax);
        }

        public override string ToString() => "Reference:" + Name;
    }
}