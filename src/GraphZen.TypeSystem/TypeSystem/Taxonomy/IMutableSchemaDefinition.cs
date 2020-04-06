// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    internal static class Specs
    {
        internal static class Creation
        {
        }

        internal static class Discovery
        {
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    internal class SpecSubjectAttribute : Attribute
    {
        public SpecSubjectAttribute(string subject)
        {
            Subject = subject;
        }

        public string Subject { get; }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    internal class SpecAttribute : Attribute
    {
        public SpecAttribute(string spec)
        {
            Spec = spec;
        }

        public string Spec { get; }
    }

    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = true)]
    internal class SpecCollectionAttribute : Attribute
    {
        public SpecCollectionAttribute(string specCollection)
        {
            SpecCollection = specCollection;
        }

        public string SpecCollection { get; }
    }

    [GraphQLIgnore]
    [SpecSubject("Schema")]
    public interface IMutableSchemaDefinition :
        ISchemaDefinition,
        IMutableDescription,
        IMutableQueryTypeDefinition,
        IMutableSubscriptionTypeDefinition,
        IMutableMutationTypeDefinition,
        IMutableDirectivesDefinition,
        IMutableObjectTypesDefinition,
        IMutableInterfaceTypesDefinition,
        IMutableUnionTypesDefinition,
        IMutableScalarTypesDefinition,
        IMutableEnumTypesDefinition,
        IMutableInputObjectTypesDefinition
    {
    }
}