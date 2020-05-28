// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

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

            if (identity.Definition is IOutputTypeDefinition outputType && !(identity.Definition is IInputTypeDefinition) && declaringMember is IInputDefinition)
            {
                throw new InvalidTypeException($"Cannot create {declaringMember} on {declaringMember.GetParentMember()} with type {TypeSyntax}: {outputType} is only an output type and {declaringMember.GetDisplayOrTypeName()}s can only use input types.");
            }
        }

        public IMutableDefinition DeclaringMember { get; }

        public TypeIdentity Identity { get; private set; }

        public TypeSyntax TypeSyntax => _syntax.WithName(Identity.Name);

        public ConfigurationSource GetTypeReferenceConfigurationSource() => _configurationSource;

        TypeReference IMutableTypeReferenceDefinition.TypeReference => this;

        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => this;
        public ConfigurationSource GetConfigurationSource() => GetTypeReferenceConfigurationSource();

        public SchemaDefinition Schema => DeclaringMember.Schema;

        public string Name => Identity.Name;

        public bool Update(TypeIdentity identity, ConfigurationSource configurationSource) =>
            Update(identity, _syntax.WithName(identity.Name), configurationSource);

        public bool Update(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource)
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

        public bool Update(string type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            var syntax = Schema.Builder.Parser.ParseType(type);
            var named = syntax.GetNamedType();
            var identity = Schema.GetOrAddTypeIdentity(named.Name.Value);
            return Update(identity, syntax, configurationSource);
        }

        public override string ToString() => $"ref: {TypeSyntax} | {Identity}";
    }
}