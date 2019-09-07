// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IDirectiveAnnotations
    {
        DirectiveLocation DirectiveLocation { get; }


        IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations { get; }


        IDirectiveAnnotation FindDirectiveAnnotation(string name);
    }
}