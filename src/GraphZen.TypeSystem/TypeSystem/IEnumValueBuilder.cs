// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IEnumValueBuilder : IAnnotableBuilder<IEnumValueBuilder>
    {

        IEnumValueBuilder Name(string name);

        IEnumValueBuilder Description(string? description);

        IEnumValueBuilder CustomValue(object? value);

        IEnumValueBuilder Deprecated(bool deprecated = true);
        IEnumValueBuilder Deprecated(string? reason);
    }
}