// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class TypeReference : INamedTypeReference, IMutableTypeReferenceDefinition
    {
        private ConfigurationSource _configurationSource;
        private TypeSyntax _syntax;


        protected TypeReference(TypeIdentity identity, TypeSyntax typeSyntax, IMutableDefinition declaringMember)
        {
            if (identity.Definition is IOutputTypeDefinition outputType)
            {
                if (!(identity.Definition is IInputTypeDefinition))
                {
                    if (declaringMember is IInputDefinition)
                    {
                        throw new InvalidTypeException(
                            $"Cannot create {declaringMember} with {this.GetTypeDisplayName()} '{typeSyntax.WithName(identity.Name)}'. {declaringMember?.GetParentMember()?.GetTypeDisplayName()?.FirstCharToUpper()} {declaringMember.GetTypeDisplayName()}s can only use input types. {outputType.ToString()?.FirstCharToUpper()} is only an output type.");
                    }
                }
            }

            Identity = identity;
            DeclaringMember = declaringMember;
            _syntax = typeSyntax;
            _configurationSource = declaringMember.GetConfigurationSource();
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

            if (identity.Definition is IOutputTypeDefinition outputType &&
                !(identity.Definition is IInputTypeDefinition) && DeclaringMember is IInputDefinition)
            {
                throw new InvalidTypeException(
                    $"Cannot set {this.GetTypeDisplayName()} to '{syntax.WithName(identity.Name)}' on {DeclaringMember}. {DeclaringMember?.GetParentMember()?.GetTypeDisplayName()?.FirstCharToUpper()} {DeclaringMember.GetTypeDisplayName()}s can only use input types. {outputType.ToString()?.FirstCharToUpper()} is only an output type.");
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
        ISchemaDefinition IMemberDefinition.Schema => Schema;
    }
}