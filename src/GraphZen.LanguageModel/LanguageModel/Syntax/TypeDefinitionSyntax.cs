// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Type definition
    ///     http://facebook.github.io/graphql/June2018/#TypeDefinition
    /// </summary>
    public abstract class TypeDefinitionSyntax : TypeSystemDefinitionSyntax, INamedSyntax, IDescribedSyntax
    {
        protected TypeDefinitionSyntax(SyntaxLocation? location) : base(location)
        {
        }

        public abstract bool IsInputType { get; }
        public abstract bool IsOutputType { get; }

        public abstract StringValueSyntax? Description { get; }
        public abstract NameSyntax Name { get; }

        public string GetDisplayValue() => Name.Value;
    }
}