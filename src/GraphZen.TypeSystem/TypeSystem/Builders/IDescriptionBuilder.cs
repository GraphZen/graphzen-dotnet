// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IDescriptionBuilder
    {
        IDescriptionBuilder Description(string description);
        IDescriptionBuilder RemoveDescription();
    }
    public interface IDescriptionBuilder<out TBuilder> : IDescriptionBuilder
    {
        new TBuilder Description(string description);
        new TBuilder RemoveDescription();
    }
}