// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;



using System.Collections.Generic;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;

namespace GraphZen.TypeSystem.Taxonomy
{
    [GraphQLIgnore]
    public interface IDirectives
    {
        DirectiveLocation DirectiveLocation { get; }

        
        
        IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations { get; }

        
        IDirectiveAnnotation FindDirectiveAnnotation( string name);
    }
}