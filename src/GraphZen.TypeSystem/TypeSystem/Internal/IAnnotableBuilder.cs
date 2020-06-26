// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Internal
{
    internal interface IAnnotableBuilder<out TBuilder>
    {
        TBuilder AddDirectiveAnnotation(string name, object? value = null);
        TBuilder AddOrUpdateDirectiveAnnotation(string name, object? value = null);
        TBuilder RemoveDirectiveAnnotations(string name);
        TBuilder RemoveDirectiveAnnotations();
    }
}