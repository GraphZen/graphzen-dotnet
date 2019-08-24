// Copyright (c) GraphZen LLC. All rights reserved.
// Licensed under the GraphZen Community License. See the LICENSE file in the project root for license information.
using JetBrains.Annotations;
#nullable disable


using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GraphZen.Infrastructure;
using GraphZen.LanguageModel;
using GraphZen.TypeSystem.Internal;
using GraphZen.TypeSystem.Taxonomy;

namespace GraphZen.TypeSystem
{
    public abstract class AnnotatableMemberDefinition : MemberDefinition, IMutableAnnotatableDefinition
    {
        
        
        private readonly List<IDirectiveAnnotation> _directives = new List<IDirectiveAnnotation>();

        protected AnnotatableMemberDefinition(ConfigurationSource configurationSource) : base(configurationSource)
        {
        }

        public abstract DirectiveLocation DirectiveLocation { get; }

        public IDirectiveAnnotation FindDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            return _directives.SingleOrDefault(_ => _.Name == name);
        }

        public IReadOnlyList<IDirectiveAnnotation> DirectiveAnnotations => _directives;

        public IDirectiveAnnotation GetOrAddDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            return FindDirectiveAnnotation(name) ?? AddDirectiveAnnotation(name, value);
        }

        public IDirectiveAnnotation AddDirectiveAnnotation(string name, object value)
        {
            var directive = CreateDirective(Check.NotNull(name, nameof(name)), value);
            _directives.Add(directive);
            return directive;
        }

        public IDirectiveAnnotation UpdateDirectiveAnnotation(string name, object value)
        {
            Check.NotNull(name, nameof(name));
            RemoveDirectiveAnnotation(name);
            return AddDirectiveAnnotation(name, value);
        }

        public void RemoveDirectiveAnnotation(string name)
        {
            Check.NotNull(name, nameof(name));
            _directives.RemoveAll(_ =>
            {
                Debug.Assert(_ != null, nameof(_) + " != null");
                return _.Name == name;
            });
        }


        private DirectiveAnnotation CreateDirective( string name, object value) =>
            new DirectiveAnnotation(name, value);
    }
}