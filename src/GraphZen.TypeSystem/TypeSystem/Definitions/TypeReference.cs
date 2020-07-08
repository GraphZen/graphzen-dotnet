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


        protected TypeReference(TypeIdentity identity, TypeSyntax syntax, IMutableDefinition declaringMember)
        {
            ThrowIfInvalid(identity, syntax, declaringMember);
            Identity = identity;
            DeclaringMember = declaringMember;
            _syntax = syntax;
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


        private void ThrowIfInvalid(TypeIdentity identity, TypeSyntax syntax, IMutableDefinition declaringMember,
            bool isUpdate = false)
        {
            if (identity.Definition is IOutputTypeDefinition outputType)
            {
                if (!(identity.Definition is IInputTypeDefinition))
                {
                    if (declaringMember is IInputDefinition)
                    {
                        var detail =
                            $"{declaringMember?.GetParentMember()?.GetTypeDisplayName()?.FirstCharToUpper()} {declaringMember.GetTypeDisplayName()}s can only use input types. {outputType.ToString()?.FirstCharToUpper()} is only an output type.";
                        if (isUpdate)
                        {
                            throw new InvalidTypeException(
                                $"Cannot set {this.GetTypeDisplayName()} to '{syntax.WithName(identity.Name)}' on {DeclaringMember}. {detail}");
                        }

                        throw new InvalidTypeException(
                            $"Cannot create {declaringMember} with {this.GetTypeDisplayName()} '{syntax.WithName(identity.Name)}'. {detail}");
                    }
                }
            }
            else if (identity.Definition is IInputTypeDefinition inputType)
            {
                if (declaringMember is IOutputDefinition)
                {
                    var detail =
                        $"{declaringMember?.GetParentMember()?.GetTypeDisplayName()?.FirstCharToUpper()} {declaringMember.GetTypeDisplayName()}s can only use output types. {inputType.ToString()?.FirstCharToUpper()} is only an input type.";
                    if (isUpdate)
                    {
                        throw new InvalidTypeException(
                            $"Cannot set {this.GetTypeDisplayName()} to '{syntax.WithName(identity.Name)}' on {DeclaringMember}. {detail}");
                    }

                    throw new InvalidTypeException(
                        $"Cannot create {declaringMember} with {this.GetTypeDisplayName()} '{syntax.WithName(identity.Name)}'. {detail}");
                }
            }
        }

        public bool Update(TypeIdentity identity, TypeSyntax syntax, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            ThrowIfInvalid(identity, syntax, DeclaringMember, true);
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

            var syntax = Schema.InternalBuilder.Parser.ParseType(type);
            var named = syntax.GetNamedType();
            var identity = Schema.GetOrAddTypeIdentity(named.Name.Value);
            return Update(identity, syntax, configurationSource);
        }

        public override string ToString() => $"ref: {TypeSyntax} | {Identity}";
    }
}