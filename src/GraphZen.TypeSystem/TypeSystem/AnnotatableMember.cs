// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public abstract class AnnotatableMember : Member, IDirectives
    {
        protected AnnotatableMember(IReadOnlyList<IDirectiveAnnotation>? directives)
        {
            DirectiveAnnotations = directives ?? ImmutableArray<IDirectiveAnnotation>.Empty;
        }

        [GraphQLIgnore] public abstract DirectiveLocation DirectiveLocation { get; }

        [GraphQLIgnore]
        public IDirectiveAnnotation FindDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            return DirectiveAnnotations.SingleOrDefault(_ => _.Name == name);
        }

        [GraphQLIgnore] public IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations { get; }
    }
}