// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using JetBrains.Annotations;

namespace GraphZen.TypeSystem
{
    public abstract class MutableAnnotatableMember : MutableMember, IMutableDirectives
    {
        private readonly List<IDirective> _directiveAnnotations = new List<IDirective>();

        protected MutableAnnotatableMember(ConfigurationSource configurationSource) : base(configurationSource)
        {
        }

        internal new AnnotatableMemberDefinitionBuilder InternalBuilder =>
            (AnnotatableMemberDefinitionBuilder)base.InternalBuilder;

        public IReadOnlyList<IDirective> DirectiveAnnotations => _directiveAnnotations;

        public abstract DirectiveLocation DirectiveLocation { get; }
        public IEnumerable<IDirective> GetDirectiveAnnotations() => _directiveAnnotations;

        public IEnumerable<IDirective> FindDirectiveAnnotations(string name) =>
            _directiveAnnotations.Where(_ => _.Name == name);

        public bool HasDirectiveAnnotation(string name) => FindDirectiveAnnotations(name).Any();

        public IEnumerable<IDirective> FindDirectiveAnnotations(Func<IDirective, bool> predicate) =>
            _directiveAnnotations.Where(predicate);

        public bool AddDirective(IDirective directive, ConfigurationSource configurationSource)
        {
            _directiveAnnotations.Add(directive);
            return true;
        }

        public bool RemoveDirective(IDirective directive, ConfigurationSource configurationSource)
        {
            _directiveAnnotations.Remove(directive);
            return true;
        }
    }
}