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
    public interface IDirectiveAnnotationsDefinition
    {
        DirectiveLocation DirectiveLocation { get; }
        IEnumerable<IDirectiveAnnotation> GetDirectiveAnnotations();
    }

    [GraphQLIgnore]
    public interface IMutableDirectiveAnnotations : IDirectiveAnnotationsDefinition
    {
        
    }

    [GraphQLIgnore]
    public interface IDirectiveAnnotations : IDirectiveAnnotationsDefinition
    {
        IDirectiveAnnotation FindDirectiveAnnotation(string name);
    }
}