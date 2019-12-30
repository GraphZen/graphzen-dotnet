// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;




namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Type system definition
    ///     http://facebook.github.io/graphql/June2018/#TypeSystemDefinition
    /// </summary>
    public abstract class TypeSystemDefinitionSyntax : DefinitionSyntax
    {
        protected TypeSystemDefinitionSyntax(SyntaxLocation? location) : base(location)
        {
        }
    }
}