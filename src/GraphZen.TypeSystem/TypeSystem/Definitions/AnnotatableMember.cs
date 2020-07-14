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
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    [GraphQLIgnore]
    public abstract class AnnotatableMember : Member, IDirectives
    {
        private readonly ImmutableArray<IDirective> _annotations;

        protected AnnotatableMember(IEnumerable<IDirective>? directives, ISchema schema) : base(schema)
        {
            _annotations = directives?.ToImmutableArray() ?? ImmutableArray<IDirective>.Empty;
        }

        [GraphQLIgnore]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public abstract DirectiveLocation DirectiveLocation { get; }


        [GraphQLIgnore]
        public IEnumerable<IDirective> FindDirectiveAnnotations(string name)
        {
            Check.NotNull(name, nameof(name));
            return _annotations.Where(_ => _.Name == name);
        }

        [GraphQLIgnore]
        public bool HasDirectiveAnnotation(string name) => FindDirectiveAnnotations(name).Any();

        public IEnumerable<IDirective> FindDirectiveAnnotations(Func<IDirective, bool> predicate) =>
            _annotations.Where(predicate);

        public IReadOnlyList<IDirective> DirectiveAnnotations => _annotations;
        public IEnumerable<IChildMember> Children() => GetChildren();

        protected virtual IEnumerable<IChildMember> GetChildren() => DirectiveAnnotations;
    }
}