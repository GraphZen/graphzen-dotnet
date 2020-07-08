// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DisplayName("argument type")]
    public class ArgumentTypeReference : TypeReference
    {
        public ArgumentTypeReference(TypeIdentity identity, TypeSyntax syntax, IMutableDefinition declaringMember) :
            base(identity, syntax, declaringMember)
        {
        }
    }
}