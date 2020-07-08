// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [DisplayName("field type")]
    public class FieldTypeReference : TypeReference
    {
        public FieldTypeReference(TypeIdentity identity, TypeSyntax syntax,
            [JetBrains.Annotations.NotNull] IMutableDefinition declaringMember) : base(identity, syntax,
            declaringMember)
        {
        }
    }
}