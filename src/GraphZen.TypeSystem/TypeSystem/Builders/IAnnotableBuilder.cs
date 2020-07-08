// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public interface IAnnotableBuilder
    {
        IAnnotableBuilder AddDirectiveAnnotation(string name, object value);
        IAnnotableBuilder AddDirectiveAnnotation(string name);
        IAnnotableBuilder RemoveDirectiveAnnotations(string name);
        IAnnotableBuilder ClearDirectiveAnnotations();
    }
    public interface IAnnotableBuilder<out TBuilder> : IAnnotableBuilder
    {
        new TBuilder AddDirectiveAnnotation(string name, object value);
        new TBuilder AddDirectiveAnnotation(string name);
        new TBuilder RemoveDirectiveAnnotations(string name);
        new TBuilder ClearDirectiveAnnotations();
    }
}