// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public class TypeReference : INamedTypeReference, IMutableTypeReferenceDefinition
    {
        private ConfigurationSource _configurationSource;
        private TypeSyntax _syntax;

        public TypeReference(TypeIdentity identity, TypeSyntax typeSyntax, IMutableDefinition declaringMember)
        {
            Identity = identity;
            DeclaringMember = declaringMember;
            _syntax = typeSyntax;
            _configurationSource = declaringMember.GetConfigurationSource();
        }

        public IMutableDefinition DeclaringMember { get; }

        public TypeIdentity Identity { get; private set; }

        public TypeSyntax TypeSyntax
        {
            get
            {
                TypeSyntax GetType(TypeSyntax node) =>
                    node switch
                    {
                        ListTypeSyntax list => SyntaxFactory.ListType(GetType(list.OfType)),
                        NonNullTypeSyntax nn => SyntaxFactory.NonNullType((NullableTypeSyntax) GetType(nn.OfType)),
                        NamedTypeSyntax _ => SyntaxFactory.NamedType(SyntaxFactory.Name(Name)),
                        _ => throw new NotImplementedException()
                    };

                return GetType(_syntax);
            }
        }

        public ConfigurationSource GetTypeReferenceConfigurationSource() => _configurationSource;

        TypeReference IMutableTypeReferenceDefinition.TypeReference => this;

        public bool SetTypeReference(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            _syntax = syntax;
            Identity = identity;
            _configurationSource = configurationSource;
            return true;
        }

        public bool SetTypeReference(string type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            var syntax = Schema.Builder.Parser.ParseType(type);
            var named = syntax.GetNamedType();
            var identity = Schema.GetOrAddTypeIdentity(named.Name.Value);
            return SetTypeReference(identity, syntax, configurationSource);
        }

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => this;
        public ConfigurationSource GetConfigurationSource() => GetTypeReferenceConfigurationSource();

        public SchemaDefinition Schema => DeclaringMember.Schema;

        public string Name => Identity.Name;

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

        public override string ToString() => $"ref: {TypeSyntax} | {Identity}";
    }
}