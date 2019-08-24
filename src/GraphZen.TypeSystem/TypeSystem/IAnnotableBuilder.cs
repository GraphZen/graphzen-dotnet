// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using GraphZen.Infrastructure;

namespace GraphZen.TypeSystem
{
    public interface IAnnotableBuilder<out TBuilder>
    {
        TBuilder DirectiveAnnotation(string name);
        TBuilder DirectiveAnnotation(string name, object value);
        TBuilder RemoveDirectiveAnnotation(string name);
    }
}