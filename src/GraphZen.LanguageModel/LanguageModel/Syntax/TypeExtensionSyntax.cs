// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;


namespace GraphZen.LanguageModel
{
    /// <summary>
    ///     Type extension
    ///     http://facebook.github.io/graphql/June2018/#TypeExtension
    /// </summary>
    public abstract class TypeExtensionSyntax : TypeSystemExtensionSyntax, INamedSyntax
    {
        protected TypeExtensionSyntax(SyntaxLocation location) : base(location)
        {
        }

        public abstract NameSyntax Name { get; }
    }
}