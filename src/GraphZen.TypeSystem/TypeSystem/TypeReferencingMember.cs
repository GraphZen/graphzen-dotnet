// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

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
    internal sealed class TypeReferenceConfiguration<T> : IMutableTypeReferenceDefinition where T : AnnotatableMemberDefinition, IMutableTypeReferenceDefinition
    {
        private ConfigurationSource _typeReferenceConfigurationSource;
        public TypeReferenceConfiguration(TypeIdentity typeReferenceIdentity, TypeSyntax typeReferenceSyntax, T declaringMember)
        {
            _typeReferenceConfigurationSource = declaringMember.GetConfigurationSource();
            TypeReference = new TypeReference(typeReferenceIdentity, typeReferenceSyntax, declaringMember);
        }

        public ConfigurationSource GetTypeReferenceConfigurationSource() => _typeReferenceConfigurationSource;
        public TypeReference TypeReference { get; private set; }

        public bool SetTypeReference(TypeReference type, ConfigurationSource configurationSource)
        {
            if (!configurationSource.Overrides(GetTypeReferenceConfigurationSource()))
            {
                return false;
            }

            TypeReference = type;
            _typeReferenceConfigurationSource = configurationSource;
            return true;

        }
        IGraphQLTypeReference ITypeReferenceDefinition.TypeReference => TypeReference;
    }
}