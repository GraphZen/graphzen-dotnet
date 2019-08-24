// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.LanguageModel
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