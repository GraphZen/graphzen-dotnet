// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public abstract class AnnotatableMember : Member, IDirectiveAnnotations
    {
        private readonly ImmutableArray<IDirectiveAnnotation> _annotations;

        protected AnnotatableMember(IEnumerable<IDirectiveAnnotation>? directives, Schema schema) : base(schema)
        {
            _annotations = directives?.ToImmutableArray() ?? ImmutableArray<IDirectiveAnnotation>.Empty;
        }

        [GraphQLIgnore] 
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract DirectiveLocation DirectiveLocation { get; }

        public IEnumerable<IDirectiveAnnotation> GetDirectiveAnnotations() => _annotations;


        [GraphQLIgnore]
        public IEnumerable<IDirectiveAnnotation> FindDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            return _annotations.Where(_ => _.Name == name);
        }

        [GraphQLIgnore]
        public bool HasAnyDirectiveAnnotation(string name) => FindDirectiveAnnotations(name).Any();

        public IEnumerable<IDirectiveAnnotation> FindDirectiveAnnotations(Func<IDirectiveAnnotation, bool> predicate) =>
            _annotations.Where(predicate);

        public IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations => _annotations;
    }
}