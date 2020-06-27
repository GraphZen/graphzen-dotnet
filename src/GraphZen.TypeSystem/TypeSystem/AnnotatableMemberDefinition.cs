// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class AnnotatableMemberDefinition : MemberDefinition, IMutableAnnotatableDefinition
    {
        private readonly List<IDirectiveAnnotation> _directiveAnnotations = new List<IDirectiveAnnotation>();

        protected AnnotatableMemberDefinition(ConfigurationSource configurationSource) : base(configurationSource)
        {
        }

        internal new AnnotatableMemberDefinitionBuilder Builder => (AnnotatableMemberDefinitionBuilder)base.Builder;

        public IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations => _directiveAnnotations;

        public abstract DirectiveLocation DirectiveLocation { get; }
        public IEnumerable<IDirectiveAnnotation> GetDirectiveAnnotations() => _directiveAnnotations;

        public IEnumerable<IDirectiveAnnotation> FindDirectiveAnnotations(string name) =>
            _directiveAnnotations.Where(_ => _.Name == name);

        public bool HasAnyDirectiveAnnotation(string name) => FindDirectiveAnnotations(name).Any();

        public IEnumerable<IDirectiveAnnotation> FindDirectiveAnnotations(Func<IDirectiveAnnotation, bool> predicate) => _directiveAnnotations.Where(predicate);

        public bool AddDirectiveAnnotation(IDirectiveAnnotation directive, ConfigurationSource configurationSource)
        {
            _directiveAnnotations.Add(directive);
            return true;
        }

        public bool RemoveDirectiveAnnotation(IDirectiveAnnotation directive, ConfigurationSource configurationSource)
        {
            _directiveAnnotations.Remove(directive);
            return true;
        }

    }
}