// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.Language
{
    public interface IFieldsNode : INamedSyntax
    {
        [NotNull]
        [ItemNotNull]
        IReadOnlyList<FieldDefinitionSyntax> Fields { get; }
    }
}