// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    /// <summary>
    ///     Executable defintion
    ///     http://facebook.github.io/graphql/June2018/#ExecutableDefinition
    /// </summary>
    public abstract class ExecutableDefinitionSyntax : DefinitionSyntax
    {
        protected ExecutableDefinitionSyntax(SyntaxLocation location) : base(location)
        {
        }
    }
}