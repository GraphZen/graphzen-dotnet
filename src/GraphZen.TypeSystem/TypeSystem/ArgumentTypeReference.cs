// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DisplayName("argument type")]
    public class ArgumentTypeReference : TypeReference
    {
        public ArgumentTypeReference(TypeIdentity identity, TypeSyntax typeSyntax, IMutableDefinition declaringMember) :
            base(identity, typeSyntax, declaringMember)
        {
        }
    }
}